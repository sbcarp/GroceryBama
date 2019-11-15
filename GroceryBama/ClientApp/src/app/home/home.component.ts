import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NavMenuService } from '../nav-menu/nav-menu.service'
@Component({
  selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})

@Injectable({ providedIn: 'root' })
export class HomeComponent {
    items: object[]; 
    filters: object[];
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
        http.get<any>(baseUrl + 'stores/getitems').subscribe(result => {
            console.log(result);
            this.items = result;
        }, error => console.error(error));
    }

    addToCart(itemId, ref) {
        this.adjustItemQuantity(ref, 0);
        this.http.post<any>(this.baseUrl + 'stores/addtocart', { id: itemId, quantity: parseInt(ref.value)}).subscribe(result => {
            console.log(result);
            this.navMenuService.cartQuantityUpdate(result.cartQuantity);
        }, error => console.error(error));
    }
    adjustItemQuantity(ref, adjustment) {
        var newQuantity = parseInt(ref.value) + adjustment;
        if (newQuantity <= 0) newQuantity= 1;
        else if (newQuantity > 999) newQuantity = 999;
        ref.value = newQuantity;
    }
}
