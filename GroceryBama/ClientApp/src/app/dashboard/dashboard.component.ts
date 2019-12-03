import { Component, Inject, Injectable, ViewChild } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Authenticator } from 'src/app/_services/authenticator'
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
/** dashboard component*/
export class DashboardComponent {
/** dashboard ctor */
    groceryIdSubscription: Subscription;
    groceryId: number;
    statistic: any;
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, public authenticator: Authenticator) {
        this.groceryIdSubscription = authenticator.groceryId.subscribe(groceryId => {
            this.groceryId = groceryId;
            this.getStatistic(groceryId);
        });
    }
    getStatistic(groceryId) {
        var params = new HttpParams().append('groceryId', groceryId.toString());
        this.http.get<any>(this.baseUrl + 'stores/getStatistic', { params }).subscribe(result => {
            console.log(result);
            if (result.success) {
                this.statistic = result.data;
            }
        }, error => console.error(error));
    }
}
