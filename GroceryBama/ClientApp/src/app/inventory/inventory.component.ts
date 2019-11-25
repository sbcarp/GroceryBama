import { Component } from '@angular/core';

@Component({
    selector: 'app-inventory',
    templateUrl: './inventory.component.html',
    styleUrls: ['./inventory.component.css']
})
/** inventory component*/
export class InventoryComponent {
/** inventory ctor */
    displayedColumns: string[] = ['itemName', 'description', 'quantity', 'retailPrice', 'wholeSalePrice', 'expirationDate', 'action'];
    dataSource = OrdersData;
    constructor() {

    }
}
const OrdersData: Orders[] = [
    { itemName: 'Dasani Water', description: '1.5 L bottled water', quantity: 3, retailPrice: 5.66, wholeSalePrice: 3.99, expirationDate: '10/23/2019' },
    { itemName: 'Dasani Water', description: '1.5 L bottled water', quantity: 3, retailPrice: 5.66, wholeSalePrice: 3.99, expirationDate: '10/23/2019' },
    { itemName: 'Dasani Water', description: '1.5 L bottled water', quantity: 3, retailPrice: 5.66, wholeSalePrice: 3.99, expirationDate: '10/23/2019' },
    { itemName: 'Dasani Water', description: '1.5 L bottled water', quantity: 3, retailPrice: 5.66, wholeSalePrice: 3.99, expirationDate: '10/23/2019' },
];
export interface Orders {
    itemName: string,
    description: string,
    quantity: number,
    retailPrice: number,
    wholeSalePrice: number,
    expirationDate: string,
}
