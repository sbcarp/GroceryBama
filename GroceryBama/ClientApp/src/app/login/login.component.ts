import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Authenticator } from 'src/app/_services/authenticator'
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatBottomSheetRef, MAT_BOTTOM_SHEET_DATA, MatBottomSheet } from '@angular/material/bottom-sheet';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent  {
    loginForm = new FormGroup({
        username: new FormControl('', Validators.required),
        password: new FormControl('', Validators.required),
    });
    constructor(
        private authenticator: Authenticator,
        private router: Router,
        private snackBar: MatSnackBar,
        private matBottomSheet: MatBottomSheet,
    ) {


    }

    ngAfterViewInit() {
        var bottomSheetRef = this.matBottomSheet.open(BottomSheetUserList);

        bottomSheetRef.afterDismissed().subscribe(result => {
            if (result == undefined) return;
            this.loginForm.controls.username.setValue(result.username);
            this.loginForm.controls.password.setValue(result.password);
        });
    }
    onSubmit() {
        this.authenticator.login(this.loginForm.controls.username.value, this.loginForm.controls.password.value)
            .pipe()
            .subscribe(
                result => {
                    if (this.authenticator.isLoggedIn) {
                        if (this.authenticator.role == 'buyer') this.router.navigate(['/']);
                        else if (this.authenticator.role == 'deliverer') this.router.navigate(['/myorders']);
                        else if (this.authenticator.role == 'manager') this.router.navigate(['/inventory']);
                        else this.router.navigate(['/']);
                    }
                    else {
                        if (result.errorCode == 5002) this.snackBar.open("Incorrect username or password :(", null, {duration: 2000});
                        else this.snackBar.open("Server error :(", null, { duration: 2000 });
                    }
                },
                error => {
                    this.snackBar.open("Network error :(", null, { duration: 2000 });
                    console.log(error);
                }
            )

    }

}

@Component({
    selector: 'bottom-sheet-user-list',
    templateUrl: 'bottom-sheet-user-list.html',
})
export class BottomSheetUserList {
    users: object[];
    constructor(private matBottomSheetRef: MatBottomSheetRef<BottomSheetUserList>,
        @Inject(MAT_BOTTOM_SHEET_DATA) private data: any,
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private authenticator: Authenticator, ) {
        this.http.get<any>(this.baseUrl + 'users/demoGetUserList').subscribe(result => {
            if (result.success) this.users = result.data;
        }, error => console.error(error));
    }
    selectUser(username, password): void {
        this.matBottomSheetRef.dismiss({ username: username, password: password });
    }
}
