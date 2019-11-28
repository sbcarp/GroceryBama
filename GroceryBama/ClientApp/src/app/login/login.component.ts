import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { Authenticator } from 'src/app/_services/authenticator'
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    loginForm = new FormGroup({
        username: new FormControl('', Validators.required),
        password: new FormControl('', Validators.required),
    });
    constructor(
        private authenticator: Authenticator,
        private router: Router
    ) { }

    ngOnInit() {

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
                },
                error => { console.log(error); }
            )

    }

}
