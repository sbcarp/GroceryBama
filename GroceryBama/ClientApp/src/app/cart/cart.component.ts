import { Component, Input, ElementRef, HostListener} from '@angular/core';
import { Authenticator } from 'src/app/_utilities/authenticator'

@Component({
    selector: 'app-cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.css']
})
/** cart component*/
export class CartComponent {
/** cart ctor */
    @Input() isCartOpened: boolean;
    constructor(private authenticator: Authenticator, private elementRef: ElementRef) {

    }
}
