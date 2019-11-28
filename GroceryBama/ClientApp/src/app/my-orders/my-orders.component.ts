import { Component, Inject, Injectable, EventEmitter, Output } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Authenticator } from 'src/app/_services/authenticator'
import { MatBottomSheet, MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '@angular/material/bottom-sheet';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-my-orders',
    templateUrl: './my-orders.component.html',
    styleUrls: ['./my-orders.component.css']
})
/** my-orders component*/
@Injectable({ providedIn: 'root' })
export class MyOrdersComponent {
/** my-orders ctor */
    items: object[];
    orders: object[];
    lastReformatedDate: string = "";
    constructor(http: HttpClient,
                @Inject('BASE_URL') baseUrl: string,
                private authenticator: Authenticator,
                private matBottomSheet: MatBottomSheet, ) {

        var params = new HttpParams().append('startIndex', '1')
            .append('endIndex', '10');
        http.get<any>(baseUrl + 'stores/GetOrders', { params }).subscribe(result => {
            if (result.success) this.orders = result.data.results;
            console.log(this.orders);
        }, error => console.error(error));
    }
    isDateDisplay(dateTime: string) {
        var reformatedDate: string = this.dateDisplayReformat(dateTime);
        var result = this.lastReformatedDate != reformatedDate;
        this.lastReformatedDate = reformatedDate;
        return result;
    }
    dateDisplayReformat(dateTime: string) {
        var givenDateTime: Date = new Date(dateTime);
        var todayDateTime: Date = new Date();
        var diffInYears: number = todayDateTime.getFullYear() - givenDateTime.getFullYear();
        var diffInMonths: number = todayDateTime.getMonth() - givenDateTime.getMonth();
        var diffInDays: number = todayDateTime.getDate() - givenDateTime.getDate();
        if (diffInYears || diffInMonths) return givenDateTime.getMonth() + "/" + givenDateTime.getDate() + "/" + givenDateTime.getFullYear();
        if (diffInDays == 0) return "Today";
        else if (diffInDays == 1) return "Yesterday";
        else if (diffInDays > 1 && diffInDays < 7) {
            //var todayDayInTheWeek: number = todayDateTime.getDay();
            var givenDayInTheWeek: number = givenDateTime.getDay();
            var dayToName = { 1: "Monday", 2: "Tuesday", 3: "Wednesday", 4: "Thursday", 5: "Friday", 6: "Saturday", 0: "Sunday" };
            return dayToName[givenDayInTheWeek];
        }
        return givenDateTime.getMonth() + "/" + givenDateTime.getDate();
    }
    orderStatusToString(status: number) {
        var statusToString = { 0: "Waiting", 1: "Driver On the Way", 2: "Deliveried", 3: "Canceled" };
        return statusToString[status];
    }
    orderStatusToClass(status: number) {
        var statusToClass = { 0: "wait", 1: "in-progress", 2: "deliveried", 3: "canceled" };
        return statusToClass[status];
    }
    openUpdateOrderOptions(orderId: number) {
        var bottomSheetRef = this.matBottomSheet.open(BottomSheetOrderUpdate, {
            data: { orderId: orderId },
        });

        bottomSheetRef.afterDismissed().subscribe(result => {
            if (result == undefined) return; 
            this.orders.find(element => { return element["orderId"] == result.orderId; })["status"] = result.status;
        });
    }
}

@Component({
    selector: 'bottom-sheet-order-update',
    templateUrl: 'bottom-sheet-order-update.html',
})
export class BottomSheetOrderUpdate {
    constructor(private matBottomSheetRef: MatBottomSheetRef<BottomSheetOrderUpdate>,
                @Inject(MAT_BOTTOM_SHEET_DATA) private data: any,
                private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private authenticator: Authenticator,) {
    }
    updateOrder(status: number): void {
        this.http.post<any>(this.baseUrl + 'stores/UpdateOrderStatus', { orderId: this.data.orderId, status: status }).subscribe(result => {
            if (result.success) this.matBottomSheetRef.dismiss({ orderId: this.data.orderId, status: status });
            else this.matBottomSheetRef.dismiss();
        }, error => console.error(error));
    }
}
