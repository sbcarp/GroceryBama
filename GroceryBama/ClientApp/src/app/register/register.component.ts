import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
/** register component*/
export class RegisterComponent {
/** register ctor */
    stores = [];
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
        groceryId: new FormControl(1, Validators.required),
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
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router,
                private snackBar: MatSnackBar,) {
        this.http.get<any>(this.baseUrl + 'stores/getStores').subscribe(result => {
            this.stores = result.data;
        }, error => console.error(error));
    }
    getFormContent(formGroup: FormGroup) {
        var formContent = {};
        for (var key in formGroup.controls) {
            formContent[key] = formGroup.controls[key].value;
        }
        return formContent;
    }
    onRegisterBuyerSubmit() {
        var formContent = this.getFormContent(this.registerBuyer);
        formContent["role"] = "buyer";
        this.callRegistration(formContent);
    }
    onRegisterDelivererSubmit() {
        var formContent = this.getFormContent(this.registerDeliverer);
        formContent["role"] = "deliverer";
        this.callRegistration(formContent);
    }
    onRegisterManagerSubmit() {
        var formContent = this.getFormContent(this.registerManager);
        formContent["role"] = "manager";
        this.callRegistration(formContent);
    }
    callRegistration(formContent) {
        this.http.post<any>(this.baseUrl + 'users/register', formContent).subscribe(result => {
            if (result.success) {
                this.snackBar.open("Your account has been created :)", null, { duration: 4000, verticalPosition: "bottom", panelClass: "mat-stack-bar-success" });
                this.router.navigate(['/login']);
            }
            else {
                this.snackBar.open("Registration failed: " + result.message, null, { duration: 4000, verticalPosition: "bottom", panelClass: "mat-stack-bar-success" });
            }
        }, error => this.snackBar.open("Network error :(", null, { duration: 2000, verticalPosition: "bottom", panelClass: "mat-stack-bar-success" }));
    }
}
