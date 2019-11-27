import { Component, Input, Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
    selector: 'app-payment-methods',
    templateUrl: './payment-methods.component.html',
    styleUrls: ['./payment-methods.component.css']
})
/** payment-methods component*/
export class PaymentMethodsComponent {
/** payment-methods ctor */
    @Input() selectMode: boolean = false;
    @Input() paymentMethods = [];
    defaultPaymentMethodId: number = -1;
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
        http.get<any>(baseUrl + 'users/getUserPaymentMethods').subscribe(result => {
            this.paymentMethods = result.data.paymentMethods;
            this.defaultPaymentMethodId = result.data.defaultPaymentMethodId;
        }, error => console.error(error));
    }
    addNewCard() {
        if (this.paymentMethods == null) this.paymentMethods = [];
        var lastPaymentMethod = this.paymentMethods.length == 0 ? null : this.paymentMethods[this.paymentMethods.length - 1];
        if (lastPaymentMethod && lastPaymentMethod.id == -1) {
            lastPaymentMethod.isExpanded = true;
        }
        else {
            this.paymentMethods.push({ id: -1, name: "", accountNumber: "", routineNumber: "", isExpanded: true });
            this.defaultPaymentMethodId = -1;
        }
        //this.paymentPanels.last.open();
    }
    onSavePaymentMethod(id) {
        var apiName: string;
        if (id == -1) apiName = "AddPaymentMethod"
        else apiName = "UpdatePaymentMethod"
        //console.log(this.paymentMethods.find(element => { return element.id == id }));
        this.http.post<any>(this.baseUrl + 'users/' + apiName, this.paymentMethods.find(element => { return element.id == id })).subscribe(result => {
            this.paymentMethods = result.data.paymentMethods;
            this.defaultPaymentMethodId = result.data.defaultPaymentMethodId;

        }, error => console.error(error));
    }
    onDeletePaymentMethod(id) {
        this.http.post<any>(this.baseUrl + 'users/deletePaymentMethod', id).subscribe(result => {
            this.paymentMethods = result.data.paymentMethods;
            this.defaultPaymentMethodId = result.data.defaultPaymentMethodId;
        }, error => console.error(error));
    }
    
}
