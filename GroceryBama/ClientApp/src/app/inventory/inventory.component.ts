import { Component, Inject, Injectable, ViewChild, ElementRef  } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Authenticator } from 'src/app/_services/authenticator'
import { Subscription } from 'rxjs';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';

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
    resultsLength: number;
    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string,
        private authenticator: Authenticator,
        private dialog: MatDialog) {
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
                this.resultsLength = result.data.totalNumberOfResults;
                this.dataSource = result.data.results;
                //this.numberOfItems = result.data.totalNumberOfResults;
            }
        }, error => console.error(error));
    }
    switchPage(groceryId: number, pageEvent: PageEvent) {
        var startIndex = pageEvent.pageIndex * pageEvent.pageSize + 1;
        var endIndex = startIndex + pageEvent.pageSize - 1;
        if (endIndex > pageEvent.length) endIndex = pageEvent.length;
        this.getItems(groceryId, startIndex, endIndex, "");
    }
    ngOnDestroy() {
        this.groceryIdSubscription.unsubscribe();
    }
    openDialog(item): void {
        const dialogRef = this.dialog.open(UpdateQuantityDialog, {
            data: { }
        });

        dialogRef.afterClosed().subscribe(result => {
            console.log(result, item.itemId);
            if (result != undefined) {
                var quantity = result.quantity;
                this.http.post<any>(this.baseUrl + 'stores/UpdateInventoryItemQuantity', { itemId: item.id, quantity: quantity}).subscribe(result => {
                    if (result.success) {
                        item.quantity = quantity;
                    }
                }, error => console.error(error));
            }
            
        });
    }
    deleteItemFromInventory(item) {
        this.http.post<any>(this.baseUrl + 'stores/DeleteItemFromInventory', { groceryId: this.groceryId, itemId: item.itemId}).subscribe(result => {
            if (result.success) {
                var tmp = this.dataSource.slice(0);
                tmp.splice(tmp.indexOf(item), 1)
                this.dataSource = tmp;
            }
        }, error => console.error(error));
    }
    dateReformat(dateStr: string) {
        var dateObj: Date = new Date(dateStr);
        return dateObj.getMonth() + '/' + dateObj.getDate() + '/' + dateObj.getFullYear();
    }
}


@Component({
    selector: 'update-quantity-dialog',
    templateUrl: 'update-quantity-dialog.html',
})
export class UpdateQuantityDialog {
    @ViewChild('ConfirmButton', { static: false}) ConfirmButton: ElementRef;
    constructor(
        public dialogRef: MatDialogRef<UpdateQuantityDialog>,
        @Inject(MAT_DIALOG_DATA) public data: object) {
        
    }
    ngAfterViewInit() {
        console.log(this.ConfirmButton);
    }
    onNoClick(): void {
        this.dialogRef.close();
    }

}
