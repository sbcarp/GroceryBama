import { Component } from '@angular/core';

@Component({
    selector: 'app-outstanding-orders',
    templateUrl: './outstanding-orders.component.html',
    styleUrls: ['./outstanding-orders.component.css']
})
/** outstanding-orders component*/
export class OutstandingOrdersComponent {
/** outstanding-orders ctor */
    displayedColumns: string[] = ['storeName', 'storeAddress', 'orderId', 'date', 'totalPrice', 'totalNumberOfItems', 'deliveryAddress'];
    dataSource = OrdersData;
    constructor() {

    }
}
const OrdersData: Orders[] = [
    {storeName: 'Publix', storeAddress: '11th St', orderId: 10001, date: '11/23/2019', totalPrice: 19.5, totalNumberOfItems: 5, deliveryAddress: 'asdfasdf'},
 ];
export interface Orders {
    storeName: string,
    storeAddress: string,
    orderId: number,
    date: string,
    totalPrice: number,
    totalNumberOfItems: number,
    deliveryAddress: string,
}
