import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
    constructor(private route: ActivatedRoute) {
    }
    ngOnInit() {
        var queryParamMap = this.route.snapshot.queryParamMap ;
        this.receiptMode = queryParamMap.get("receiptmode") == "true";
        this.orderId = parseInt(queryParamMap.get("orderid"));
    }
}
