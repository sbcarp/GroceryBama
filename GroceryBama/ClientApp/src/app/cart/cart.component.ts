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
    items: object[];
    total: number;
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get<any>(baseUrl + 'stores/getcartitems').subscribe(result => {
            console.log(result);
            this.items = result;
            this.total = this.calculateTotal(result);
        }, error => console.error(error));
    }
    calculateTotal(items) {
        var t = 0;
        for (var i = 0; i < this.items.length; i++) {
            t += items[i].quantity * items[i].listedPrice;
        }
        return t;
    }
}
