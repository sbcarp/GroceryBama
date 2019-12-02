import { Component, ViewChild, Inject } from '@angular/core';
import { PaymentMethodsComponent } from 'src/app/payment-methods/payment-methods.component';
import { Authenticator } from 'src/app/_services/authenticator'
import { HttpClient, HttpParams } from '@angular/common/http'
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
    selector: 'app-checkout',
    templateUrl: './checkout.component.html',
    styleUrls: ['./checkout.component.css']
})
/** checkout component*/
export class CheckoutComponent {
/** checkout ctor */
    deliveryTime: number = 0;
    deliveryInstruction: string = '';
    totalPrice: number;
    totalNumItems: number;
    groceryId: number;
    @ViewChild('paymentMethods', {static: false}) paymentMethods: PaymentMethodsComponent;
    constructor(private http: HttpClient,@Inject('BASE_URL') private baseUrl: string,
        private authenticator: Authenticator, private router: Router,
        private snackBar: MatSnackBar) {
        this.groceryId = authenticator.groceryIdValue;
        var params = new HttpParams().append('groceryId', this.groceryId.toString());
        this.http.get<any>(this.baseUrl + 'stores/getcartitems', { params }).subscribe(result => {
            if (result.success) {
                this.udpateTotal(result.data);
            }
        }, error => console.error(error));
    }
    udpateTotal(items) {
        var price = 0, quantity = 0;
        for (var i = 0; i < items.length; i++) {
            price += items[i].quantity * items[i].listedPrice;
            quantity += items[i].quantity;
        }
        this.totalPrice = price;
        this.totalNumItems = quantity;
    }
    onCheckOut() {
        var dateTimeObj: Date = new Date();
        var postData = { GroceryId: this.groceryId, DeliveryInstructions: this.deliveryInstruction, RequestDeliveryTimeOffsetInHours: this.deliveryTime, PaymentMethodId: this.paymentMethods.defaultPaymentMethodId }
        this.http.post<any>(this.baseUrl + 'stores/checkout', postData).subscribe(result => {
            // Check this if navigateByUrl is having problem https://github.com/angular/angular/issues/18798
            console.log(result.data);
            if (result.success) {
                this.router.navigateByUrl('/myorders/orderdetails?receiptmode=true&orderId=' + result.data.orderId, { queryParams: { receiptmode: true, orderId: result.data.orderId } });
                return;
            }
            if (result.errorCode == 5025 || result.errorCode == 5024 || result.errorCode == 5023) {
                this.snackBar.open(result.message, null, { duration: 4000, verticalPosition: "bottom", panelClass: "mat-stack-bar-success" });
            }
            else {
                this.snackBar.open("Sever error", null, { duration: 4000, verticalPosition: "bottom", panelClass: "mat-stack-bar-success" });
            }
        }, error => this.snackBar.open("Network error", null, { duration: 4000, verticalPosition: "bottom", panelClass: "mat-stack-bar-success" }));
    }
}
