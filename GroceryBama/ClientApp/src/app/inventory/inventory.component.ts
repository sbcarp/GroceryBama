import { Component, Inject, Injectable, ViewChild } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Authenticator } from 'src/app/_services/authenticator'
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-inventory',
    templateUrl: './inventory.component.html',
    styleUrls: ['./inventory.component.css']
})
/** inventory component*/
export class InventoryComponent {
/** inventory ctor */
    displayedColumns: string[] = ['itemName', 'description', 'quantity', 'retailPrice', 'wholeSalePrice', 'expirationDate', 'action'];
    OrdersData: object[];
    dataSource = this.OrdersData;
    groceryIdSubscription: Subscription;
    groceryId: number;
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private authenticator: Authenticator) {
        this.groceryIdSubscription = authenticator.groceryId.subscribe(groceryId => {
            this.groceryId = groceryId;
            this.getItems(groceryId, 1, 10, "");
        });
    }
    getItems(groceryId: number, startIndex: number, endIndex: number, foodGroup: string) {
        var params = new HttpParams().append('groceryId', groceryId.toString()).append('startIndex', startIndex.toString())
            .append('endIndex', endIndex.toString()).append('foodGroup', foodGroup);
        this.http.get<any>(this.baseUrl + 'stores/getitems', { params }).subscribe(result => {
            console.log(result);
            if (result.success) {
                this.dataSource = result.data.results;
                //this.numberOfItems = result.data.totalNumberOfResults;
            }
        }, error => console.error(error));
    }
    ngOnDestroy() {
        this.groceryIdSubscription.unsubscribe();
    }
    dateReformat(dateStr: string) {
        var dateObj: Date = new Date(dateStr);
        return dateObj.getMonth() + '/' + dateObj.getDate() + '/' + dateObj.getFullYear();
    }
}
//const OrdersData: object[] = [
//    { name: 'Dasani Water', description: '1.5 L bottled water', quantity: 3, retailPrice: 5.66, wholeSalePrice: 3.99, expirationDate: '10/23/2019' },
//    { itemName: 'Dasani Water', description: '1.5 L bottled water', quantity: 3, retailPrice: 5.66, wholeSalePrice: 3.99, expirationDate: '10/23/2019' },
//    { itemName: 'Dasani Water', description: '1.5 L bottled water', quantity: 3, retailPrice: 5.66, wholeSalePrice: 3.99, expirationDate: '10/23/2019' },
//    { itemName: 'Dasani Water', description: '1.5 L bottled water', quantity: 3, retailPrice: 5.66, wholeSalePrice: 3.99, expirationDate: '10/23/2019' },
//];

