import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-address',
    templateUrl: './address.component.html',
    styleUrls: ['./address.component.css']
})
/** address component*/
export class AddressComponent {
/** address ctor */
    isInfoChanged = false;
    address = new FormGroup({
        streetAddress: new FormControl(null, Validators.required),
        addressLine2: new FormControl(null),
        city: new FormControl(null, Validators.required),
        state: new FormControl(null, Validators.required),
        zipCode: new FormControl(null, Validators.required),
    });
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string ) {
        this.getUserAddress();
    }
    getUserAddress() {
        this.http.get<any>(this.baseUrl + 'users/getUserAddress').subscribe(result => {
            this.setFormControls(result.data);
            this.isInfoChanged = false;
        }, error => console.error(error));
    }
    onSaveAddress() {
        var formContent = {};
        for (var key in this.address.controls) {
            formContent[key] = this.address.controls[key].value;
        }
        this.http.post<any>(this.baseUrl + 'users/updateUserAddress', formContent).subscribe(result => {
            this.setFormControls(result.data);
            this.isInfoChanged = false;
        }, error => console.error(error));
    }
    setFormControls(data) {
        this.address.controls["streetAddress"].setValue(data.streetAddress);
        this.address.controls["addressLine2"].setValue(data.addressLine2);
        this.address.controls["city"].setValue(data.city);
        this.address.controls["state"].setValue(data.state);
        this.address.controls["zipCode"].setValue(data.zipCode);
    }
}
