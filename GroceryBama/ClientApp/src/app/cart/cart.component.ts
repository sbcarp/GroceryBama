import { Component, Inject, Injectable, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ItemsListComponent } from 'src/app/items-list/items-list.component'
import { Subscription } from 'rxjs'
@Component({
    selector: 'app-cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.css']
})
/** cart component*/
@Injectable({ providedIn: 'root' })
export class CartComponent {
    @ViewChild('itemsList', { static: false }) itemsList: ItemsListComponent;
    itemsListSubscription: Subscription;
    totalPrice: number;
    totalNumItems: number;
    constructor() {
        
        
    }
    ngAfterViewInit() {
        this.itemsListSubscription = this.itemsList.getItems().subscribe(items => {
            this.udpateTotal(items);
        });
    }
    ngOnDestroy() {
        this.itemsListSubscription.unsubscribe();
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
}
