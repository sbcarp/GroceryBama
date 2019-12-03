import { Component, Inject } from '@angular/core';
import { Authenticator } from 'src/app/_services/authenticator'
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-account',
    templateUrl: './account.component.html',
    styleUrls: ['./account.component.css']
})
/** account component*/
export class AccountComponent {
/** account ctor */
    user = this.authenticator.currentUser;
    contact = new FormGroup({
        //username: new FormControl(this.authenticator.currentUser.username),
        //firstname: new FormControl(this.authenticator.currentUser.firstname),
        //lastname: new FormControl(this.authenticator.currentUser.lastname),
        phoneNumber: new FormControl(null, Validators.required),
        email: new FormControl(null, Validators.required),
    });
    isInfoChanged = false;
    constructor(public authenticator: Authenticator, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string ) {
        this.getUserContact();
    }
    getUserContact() {
        this.http.get<any>(this.baseUrl + 'users/getUserContact').subscribe(result => {
            this.setFormControls(result);
            this.isInfoChanged = false;
        }, error => console.error(error));
    }
    setFormControls(result) {
        this.contact.controls["phoneNumber"].setValue(result.data.phoneNumber);
        this.contact.controls["email"].setValue(result.data.email);
    }
    onSubmit() {
        var formContent = {};
        for (var key in this.contact.controls) {
            formContent[key] = this.contact.controls[key].value;
        }
        this.http.post<any>(this.baseUrl + 'users/updateUserContact', formContent).subscribe(result => {
            this.setFormControls(result);
            this.isInfoChanged = false;
        });
    }
}
