import { Component, Inject, Injectable, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Authenticator } from 'src/app/_utilities/authenticator'

@Component({
    selector: 'app-items-list',
    templateUrl: './items-list.component.html',
    styleUrls: ['./items-list.component.css'],
})
/** items-list component*/
@Injectable({ providedIn: 'root' })
export class ItemsListComponent {
/** items-list ctor */
    items: object[];
    @Input() isCartMode: boolean;
    @Input() orderId: number;
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private authenticator: Authenticator ) {
        http.get<any>(baseUrl + 'stores/getcartitems').subscribe(result => {
            console.log(result);
            this.items = result.data;
        }, error => console.error(error));
    }
    removeItemFromCart(itemId) {
        var groceryId = this.authenticator.currentUser.groceryId;
        this.http.post<any>(this.baseUrl + 'stores/RemoveItemFromCart', { groceryId: groceryId, itemId: itemId}).subscribe(result => {
            this.items = result.data.items;
        }, error => console.error(error));
    }
    updateCartItemQuantity(itemId, quantity) {
        var groceryId = this.authenticator.currentUser.groceryId;
        this.http.post<any>(this.baseUrl + 'stores/updateCartItemQuantity', { groceryId: groceryId, itemId: itemId, quantity: quantity}).subscribe(result => {
            this.items = result.data.items;
        }, error => console.error(error));
    }
}
