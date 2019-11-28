import { Component, Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Authenticator } from 'src/app/_services/authenticator'
import { MatBottomSheet, MatBottomSheetRef } from '@angular/material/bottom-sheet';


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
    constructor(http: HttpClient,
        @Inject('BASE_URL') baseUrl: string,
        private authenticator: Authenticator,
        private matBottomSheet: MatBottomSheet,
    ) {
        http.get<any>(baseUrl + 'stores/getcartitems').subscribe(result => {
            if (result.success) this.items = result.data;
        }, error => console.error(error));
    }
    openBottomSheet() {
        this.matBottomSheet.open(BottomSheetOrderUpdate);
    }
}

@Component({
    selector: 'bottom-sheet-order-update',
    templateUrl: 'bottom-sheet-order-update.html',
})
export class BottomSheetOrderUpdate {
    constructor(private matBottomSheetRef: MatBottomSheetRef<BottomSheetOrderUpdate>) { }

    openLink(event: MouseEvent): void {
        this.matBottomSheetRef.dismiss();
        event.preventDefault();
    }
}
