import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.css']
})
/** cart component*/
@Injectable({ providedIn: 'root' })
export class CartComponent {
    
    total: number;
    constructor() {
        this.total = 499.7;
        
    }
    calculateTotal(items) {
        var t = 0;
        //for (var i = 0; i < this.items.length; i++) {
        //    t += items[i].quantity * items[i].listedPrice;
        //}
        return t;
    }
}
