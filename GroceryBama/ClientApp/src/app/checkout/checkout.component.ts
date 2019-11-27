import { Component, ViewChild, Inject } from '@angular/core';
import { PaymentMethodsComponent } from 'src/app/payment-methods/payment-methods.component';
import { Authenticator } from 'src/app/_utilities/authenticator'
import { HttpClient } from '@angular/common/http'
import { Router } from '@angular/router';
@Component({
    selector: 'app-checkout',
    templateUrl: './checkout.component.html',
    styleUrls: ['./checkout.component.css']
})
/** checkout component*/
export class CheckoutComponent {
/** checkout ctor */
    deliveryTime: string = 'ASAP';
    deliveryInstruction: string = '';
    @ViewChild('paymentMethods', {static: false}) paymentMethods: PaymentMethodsComponent;
    constructor(private http: HttpClient,@Inject('BASE_URL') private baseUrl: string,
                private authenticator: Authenticator,private router: Router) {
        
    }
    onCheckOut() {
        var postData = { GroceryId: this.authenticator.groceryIdValue, DeliveryInstructions: this.deliveryInstruction, RequestDeliveryTime: this.deliveryTime, PaymentMethodId: this.paymentMethods.defaultPaymentMethodId}
        this.http.post<any>(this.baseUrl + 'stores/checkout', postData).subscribe(result => {
            this.router.navigateByUrl('/myorders/orderdetails', { queryParams: { receiptmode: true, orderid: result.data.id }});
        }, error => console.error(error));
    }
}
