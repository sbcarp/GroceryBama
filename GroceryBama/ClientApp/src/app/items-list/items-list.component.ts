import { Component, Inject, Injectable, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Authenticator } from 'src/app/_utilities/authenticator'
import { ReplaySubject, Observable } from 'rxjs'
@Component({
    selector: 'app-items-list',
    templateUrl: './items-list.component.html',
    styleUrls: ['./items-list.component.css'],
})
/** items-list component*/
@Injectable({ providedIn: 'root' })
export class ItemsListComponent {
/** items-list ctor */
    //items: object[];
    private items;
    private itemsObservable = new ReplaySubject(1);
    @Input() isCartMode: boolean;
    @Input() orderId: number;
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private authenticator: Authenticator ) {
        http.get<any>(baseUrl + 'stores/getcartitems').subscribe(result => {
            this.itemsObservable.subscribe(items => {
                this.items = items;
            })
            this.itemsObservable.next(result.data);
        }, error => console.error(error));
    }
    public getItems(): Observable<any> {
        return this.itemsObservable.asObservable();
    }
    removeItemFromCart(itemId) {
        var groceryId = this.authenticator.currentUser.groceryId;
        this.http.post<any>(this.baseUrl + 'stores/RemoveItemFromCart', { groceryId: groceryId, itemId: itemId}).subscribe(result => {
            this.itemsObservable.next(result.data.items);
        }, error => console.error(error));
    }
    updateCartItemQuantity(itemId, quantity) {
        var groceryId = this.authenticator.currentUser.groceryId;
        this.http.post<any>(this.baseUrl + 'stores/updateCartItemQuantity', { groceryId: groceryId, itemId: itemId, quantity: quantity}).subscribe(result => {
            this.itemsObservable.next(result.data.items);
        }, error => console.error(error));
    }
}
