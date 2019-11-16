import { Component } from '@angular/core';
import { Authenticator } from 'src/app/_utilities/authenticator'
@Component({
    selector: 'app-account',
    templateUrl: './account.component.html',
    styleUrls: ['./account.component.css']
})
/** account component*/
export class AccountComponent {
/** account ctor */
    user = this.authenticator.currentUser;
    constructor(private authenticator: Authenticator) {

    }
}
