import { Component, Inject, Injectable, ViewChild } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { NavMenuService } from '../nav-menu/nav-menu.service'
import { Subscription } from 'rxjs';
import { PageEvent, MatPaginator } from '@angular/material/paginator';
import { Authenticator } from 'src/app/_services/authenticator'
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

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
        private navMenuService: NavMenuService,
        public authenticator: Authenticator,
        private router: Router,
        private snackBar: MatSnackBar) {
        this.filters = [
            { name: "Beverages", value: "Beverages"},
            { name: "Baking Goods", value: "BakingGoods" },
            { name: "Canned Goods", value: "CannedGoods" },
            { name: "Cleaning Products", value: "CleaningProducts" },
            { name: "Dairy", value: "Dairy" },
            { name: "Frozen Foods", value: "FrozenFoods" },
            { name: "Meat", value: "Meat" },
            { name: "Personal Care", value: "PersonalCare" },
            { name: "Produce", value: "Produce" },
            { name: "Others", value: "Others" },
        ];
        this.foodGroup = this.filters[0]["name"];
        this.groceryIdSubscription = authenticator.groceryId.subscribe(groceryId => {
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
                window.scroll(0, 0);
            }
        }, error => console.error(error));
    }
    addToCart(itemId, ref) {
        this.adjustItemQuantity(ref, 0);
        if (this.authenticator.isLoggedIn) {
            var quantity = parseInt(ref.value);
            this.http.post<any>(this.baseUrl + 'stores/addtocart', { groceryId: this.groceryId, itemId: itemId, quantity: quantity }).subscribe(result => {
                if (result.success) this.navMenuService.cartQuantityUpdate(result.data.cartQuantity);
                this.snackBar.open("Your " + (quantity <= 1 ? "item is":"items are") + " added to cart", null, { duration: 2000, verticalPosition: "bottom", panelClass: "mat-stack-bar-success" });
            }, error => console.error(error));
        }
        else {
            this.router.navigateByUrl('/login');
        }
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
    showExpireDate(dateStr: string) {
        var dateObj: Date = new Date(dateStr);
        this.snackBar.open("The item expires on " + (dateObj.getMonth()+1) + "/" + dateObj.getDate() + "/" + dateObj.getFullYear(), null, { duration: 2000, verticalPosition: "bottom", panelClass: "mat-stack-bar-success" });
    }
}
