import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})

@Injectable({ providedIn: 'root' })
export class HomeComponent {
    items: object[]; 
    filters: object[];
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
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
}
