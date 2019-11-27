import { Component, Inject, Injectable, ViewChild } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { NavMenuService } from '../nav-menu/nav-menu.service'
import { Subscription } from 'rxjs';
import { PageEvent, MatPaginator } from '@angular/material/paginator';
//import { EDEADLK } from 'constants';
@Component({
  selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
})

@Injectable({ providedIn: 'root' })
export class HomeComponent {
    items: object[]; 
    filters: object[];
    foodGroup: string;
    numberOfItems: number;
    groceryId: number;
    groceryIdSubscription: Subscription;
    @ViewChild('paginator', { static: false}) paginator: MatPaginator;
    constructor(private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private navMenuService: NavMenuService) {
        this.filters = [
            { name: "Beverages" },
            { name: "Baking Goods" },
            { name: "Canned Goods" },
            { name: "Cleaning Products" },
            { name: "Dairy" },
            { name: "Frozen Foods" },
            { name: "Meat" },
            { name: "Personal Care" },
            { name: "Produce" },
            { name: "Others" },
        ];
        this.foodGroup = this.filters[0]["name"];
        this.groceryIdSubscription = navMenuService.getCurrentGroceryId().subscribe(groceryId => {
            this.groceryId = groceryId;
            this.getItems(groceryId, 1, 10, this.foodGroup);
        });
    }
    ngOnDestroy() {
        this.groceryIdSubscription.unsubscribe();
    }
    getItems(groceryId: number, startIndex: number, endIndex: number, foodGroup: string) {
        var params = new HttpParams().append('groceryId', groceryId.toString()).append('startIndex', startIndex.toString())
            .append('endIndex', endIndex.toString()).append('foodGroup', foodGroup);
        this.http.get<any>(this.baseUrl + 'stores/getitems', { params }).subscribe(result => {
            console.log(result);
            if (result.success) {
                this.items = result.data.results;
                this.numberOfItems = result.data.totalNumberOfResults;
            }
        }, error => console.error(error));
    }
    addToCart(itemId, ref) {
        this.adjustItemQuantity(ref, 0);
        this.http.post<any>(this.baseUrl + 'stores/addtocart', { groceryId: this.groceryId, itemId: itemId, quantity: parseInt(ref.value)}).subscribe(result => {
            if (result.success) this.navMenuService.cartQuantityUpdate(result.data.cartQuantity);
        }, error => console.error(error));
    }
    adjustItemQuantity(ref, adjustment) {
        var newQuantity = parseInt(ref.value) + adjustment;
        if (newQuantity <= 0) newQuantity= 1;
        else if (newQuantity > 999) newQuantity = 999;
        ref.value = newQuantity;
    }
    switchPage(groceryId: number, pageEvent: PageEvent) {
        var startIndex = pageEvent.pageIndex * pageEvent.pageSize + 1;
        var endIndex = startIndex + pageEvent.pageSize - 1;
        if (endIndex > pageEvent.length) endIndex = pageEvent.length;
        this.getItems(groceryId, startIndex, endIndex, this.foodGroup);
    }
}
