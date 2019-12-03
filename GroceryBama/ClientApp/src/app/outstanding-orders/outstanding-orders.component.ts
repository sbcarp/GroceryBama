import { Component, Inject, Injectable, ViewChild } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Authenticator } from 'src/app/_services/authenticator'
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-outstanding-orders',
    templateUrl: './outstanding-orders.component.html',
    styleUrls: ['./outstanding-orders.component.css']
})
/** outstanding-orders component*/
export class OutstandingOrdersComponent {
/** outstanding-orders ctor */
    displayedColumns: string[] = ['storeName', 'orderId', 'date', 'totalPrice', 'totalNumberOfItems', 'deliveryAddress'];
    OrdersData: object[];
    dataSource = this.OrdersData;
    groceryIdSubscription: Subscription;
    groceryId: number;

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, public authenticator: Authenticator) {
        this.groceryIdSubscription = authenticator.groceryId.subscribe(groceryId => {
            this.groceryId = groceryId;
            this.getOutstandingOrders(groceryId, 1, 10);
        });
    }
    ngOnDestroy() {
        this.groceryIdSubscription.unsubscribe();
    }
    getOutstandingOrders(groceryId: number, startIndex: number, endIndex: number,) {
        var params = new HttpParams().append('groceryId', groceryId.toString()).append('startIndex', startIndex.toString())
            .append('endIndex', endIndex.toString());
        this.http.get<any>(this.baseUrl + 'stores/getOutstandingOrders', { params }).subscribe(result => {
            console.log(result);
            if (result.success) {
                this.dataSource = result.data.results;
                //this.numberOfItems = result.data.totalNumberOfResults;
            }
        }, error => console.error(error));
    }
    dateReformat(dateStr: string) {
        var dateObj: Date = new Date(dateStr);
        console.log(dateObj);
        return (dateObj.getMonth()+1) + '/' + dateObj.getDate() + '/' + dateObj.getFullYear();
    }
    addressReformat(order: object) {
        // { { element.addressLine2 + "<br>" } } { { element.streetAddress + ', ' + element.city + ', ' + element.state } }
        var addressStr: string = "";
        if (order["addressLine2"] != "") addressStr += order["addressLine2"] + ' <br /> ';
        addressStr += order["streetAddress"] + ', ' + order["city"] + ', ' + order["state"];
        return addressStr;
    }
}
