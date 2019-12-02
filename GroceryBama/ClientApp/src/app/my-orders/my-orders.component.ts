import { Component, Inject, Injectable, EventEmitter, Output, ChangeDetectorRef } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Authenticator } from 'src/app/_services/authenticator'
import { MatBottomSheet, MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA } from '@angular/material/bottom-sheet';
import { Router, ActivatedRoute } from '@angular/router';
import { startWith, tap, delay } from 'rxjs/operators';
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
    constructor(private http: HttpClient,
                @Inject('BASE_URL') private baseUrl: string,
                private authenticator: Authenticator,
        private matBottomSheet: MatBottomSheet,
        private changeDetectorRef: ChangeDetectorRef) {
        
        var params = new HttpParams().append('startIndex', '1')
            .append('endIndex', '10');
        this.http.get<any>(this.baseUrl + 'stores/GetOrders', { params }).subscribe(result => {
            if (result.success) this.orders = result.data.results;
            console.log(this.orders);
        }, error => console.error(error));
    }
    ngAfterContentChecked() {
        //this.changeDetectorRef.detectChanges();
    }
    isDateDisplay(order: object) {
        var dateTime: string = order["dateTime"];
        if (order["isDateDispaly"] != null) return order["isDateDispaly"];
        var reformatedDate: string = this.dateDisplayReformat(dateTime);
        order["isDateDispaly"] = this.lastReformatedDate != reformatedDate;
        this.lastReformatedDate = reformatedDate;
        return order["isDateDispaly"] ;
    }
    dateDisplayReformat(dateTime: string) {
        var givenDateTime: Date = new Date(dateTime);
        var todayDateTime: Date = new Date();
        var diffInYears: number = todayDateTime.getFullYear() - givenDateTime.getFullYear();
        var diffInMonths: number = todayDateTime.getMonth() - givenDateTime.getMonth();
        var diffInDays: number = todayDateTime.getDate() - givenDateTime.getDate();
        if (diffInYears || diffInMonths) return (givenDateTime.getMonth()+1) + "/" + givenDateTime.getDate() + "/" + givenDateTime.getFullYear();
        if (diffInDays == 0) return "Today";
        else if (diffInDays == 1) return "Yesterday";
        else if (diffInDays > 1 && diffInDays < 7) {
            //var todayDayInTheWeek: number = todayDateTime.getDay();
            var givenDayInTheWeek: number = givenDateTime.getDay();
            var dayToName = { 1: "Monday", 2: "Tuesday", 3: "Wednesday", 4: "Thursday", 5: "Friday", 6: "Saturday", 0: "Sunday" };
            return dayToName[givenDayInTheWeek];
        }
        return (givenDateTime.getMonth()+1) + "/" + givenDateTime.getDate();
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
    //checkOrderStatus(order) {
    //    if (order.status == null) return false;
    //    if (order.status == 0) return true;
    //    return false;
    //}
    reformatTime(requestDeliveryTime) {
        var dateTimeObj: Date = new Date(requestDeliveryTime);
        var suffix: string = dateTimeObj.getHours() < 12 ? "AM" : "PM";
        var hours = dateTimeObj.getHours() <= 12 ? dateTimeObj.getHours() : dateTimeObj.getHours() - 11;
        return hours + ':' + dateTimeObj.getMinutes().toString().padStart(2, '0') + ' ' + suffix;

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
