import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http'
@Component({
    selector: 'app-order-details',
    templateUrl: './order-details.component.html',
    styleUrls: ['./order-details.component.css']
})
/** order-details component*/
export class OrderDetailsComponent {
/** order-details ctor */
    receiptMode: boolean;
    orderId: number;
    order = {};
    items = {};
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string,
                private route: ActivatedRoute) {
        var queryParamMap = this.route.snapshot.queryParamMap;
        this.receiptMode = queryParamMap.get("receiptmode") == "true";
        this.orderId = parseInt(queryParamMap.get("orderId"));
        this.http.get<any>(this.baseUrl + 'stores/GetOrderDetail/' + this.orderId).subscribe(result => {
            this.order = result.data;
            this.items = result.data.items;
        }, error => console.error(error));
    }
    ngOnInit() {
        
    }
    orderStatusToString(status) {
        var statusToString = { 0: "Waiting", 1: "Driver On the Way", 2: "Deliveried", 3: "Canceled" };
        return statusToString[status];
    }
}
