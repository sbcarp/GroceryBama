import { Component, Inject, Injectable, Input, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Authenticator } from 'src/app/_services/authenticator'
import { ReplaySubject, Observable, Subscription } from 'rxjs'
import { NavMenuService } from '../nav-menu/nav-menu.service'

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
    items: any;
    private itemsObservable = new ReplaySubject(1);
    @Input() isCartMode: boolean;
    @Input() orderId: number;
    groceryIdSubscription: Subscription;
    groceryId: number;
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, public authenticator: Authenticator,
                private navMenuService: NavMenuService,) {
        
    }
    ngOnInit() {
        if (!this.isCartMode) {
            this.http.get<any>(this.baseUrl + 'stores/GetOrderDetail/' + this.orderId).subscribe(result => {
                this.items = result.data.items;
            }, error => console.error(error));
        }
        this.groceryIdSubscription = this.authenticator.groceryId.subscribe(groceryId => {
            this.groceryId = groceryId;
            if (this.isCartMode) {
                var params = new HttpParams().append('groceryId', this.groceryId.toString())
                this.http.get<any>(this.baseUrl + 'stores/getcartitems', { params }).subscribe(result => {
                    this.itemsObservable.subscribe(items => {
                        this.items = items;
                    })
                    this.itemsObservable.next(result.data);
                }, error => console.error(error));
            }
        });
    }
    public getItems(): Observable<any> {
        return this.itemsObservable.asObservable();
    }
    removeItemFromCart(itemId) {
        this.http.post<any>(this.baseUrl + 'stores/RemoveItemFromCart', { groceryId: this.groceryId, itemId: itemId}).subscribe(result => {
            if (result.success) {
                this.itemsObservable.next(result.data.items);
                this.navMenuService.cartQuantityUpdate(result.data.quantity);
            }

        }, error => console.error(error));
    }
    updateCartItemQuantity(itemId, quantity) {
        this.http.post<any>(this.baseUrl + 'stores/updateCartItemQuantity', { groceryId: this.groceryId, itemId: itemId, quantity: quantity}).subscribe(result => {
            this.itemsObservable.next(result.data.items);
            this.navMenuService.cartQuantityUpdate(result.data.quantity);
        }, error => console.error(error));
    }
}
