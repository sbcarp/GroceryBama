import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get<any>(baseUrl + 'stores/getcartitems').subscribe(result => {
            console.log(result);
            this.items = result;
        }, error => console.error(error));
    }
}
