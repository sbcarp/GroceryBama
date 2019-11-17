import { Component, Inject, Injectable, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-items-list',
    templateUrl: './items-list.component.html',
    styleUrls: ['./items-list.component.css'],
})
/** items-list component*/
@Injectable({ providedIn: 'root' })
export class ItemsListComponent {
/** items-list ctor */
    items: object[];
    @Input() isCartMode: boolean;
    @Input() orderId: number;
    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get<any>(baseUrl + 'stores/getcartitems').subscribe(result => {
            console.log(result);
            this.items = result;
        }, error => console.error(error));
    }
}
