import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
/** register component*/
export class RegisterComponent {
/** register ctor */
    registerBuyer = new FormGroup({
        username: new FormControl('', Validators.required),
        password: new FormControl('', Validators.required),
        lastname: new FormControl('', Validators.required),
        firstname: new FormControl('', Validators.required),
        email: new FormControl('', Validators.required),
        phoneNumber: new FormControl('', Validators.required),
        streetAddress: new FormControl('', Validators.required),
        addressLine2: new FormControl(''),
        city: new FormControl('', Validators.required),
        state: new FormControl('', Validators.required),
        zipCode: new FormControl('', Validators.required),
    });
    registerManager = new FormGroup({
        username: new FormControl('', Validators.required),
        password: new FormControl('', Validators.required),
        lastname: new FormControl('', Validators.required),
        firstname: new FormControl('', Validators.required),
        email: new FormControl('', Validators.required),
        phoneNumber: new FormControl('', Validators.required),
        confirmationCode: new FormControl('', Validators.required),
        groceryId: new FormControl('', Validators.required),
    });
    registerDeliverer = new FormGroup({
        username: new FormControl('', Validators.required),
        password: new FormControl('', Validators.required),
        lastname: new FormControl('', Validators.required),
        firstname: new FormControl('', Validators.required),
        email: new FormControl('', Validators.required),
        phoneNumber: new FormControl('', Validators.required),
        confirmationCode: new FormControl('', Validators.required),
    });
    fakeFormGroup = new FormGroup({
        username: new FormControl('', Validators.required),
        password: new FormControl('', Validators.required),
        lastname: new FormControl('', Validators.required),
        firstname: new FormControl('', Validators.required),
        email: new FormControl('', Validators.required),
        phoneNumber: new FormControl('', Validators.required),
    });
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

    }
    onRegisterBuyerSubmit() {
        var formContent = {};
        for (var key in this.registerBuyer.controls) {
            formContent[key] = this.registerBuyer.controls[key].value;
        }
        formContent["role"] = "buyer";
        this.http.post<any>(this.baseUrl + 'users/register', formContent).subscribe(result => {
            console.log(result);
        }, error => console.error(error));
        
    }
    onRegisterDelivererSubmit() {

    }
    onRegisterManagerSubmit() {

    }
}
