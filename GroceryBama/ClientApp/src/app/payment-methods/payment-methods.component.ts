import { Component } from '@angular/core';

@Component({
    selector: 'app-payment-methods',
    templateUrl: './payment-methods.component.html',
    styleUrls: ['./payment-methods.component.css']
})
/** payment-methods component*/
export class PaymentMethodsComponent {
/** payment-methods ctor */
    selectMode = false;
    test = [1, 2, 3];
    constructor() {

    }
}
