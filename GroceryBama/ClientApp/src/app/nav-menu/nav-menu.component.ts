import { Component, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Authenticator } from 'src/app/_utilities/authenticator'
import { first } from 'rxjs/operators';

@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
    isExpanded = false;
    isLoggedIn = false;
    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private authenticator: Authenticator,
        private router: Router) {
        this.isLoggedIn = this.authenticator.isLoggedIn();
    }
    
    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }


    logout() {
        this.authenticator.logout();
        this.isLoggedIn = this.authenticator.isLoggedIn();
    }

    login(username: string, password: string) {
        this.authenticator.login(username, password)
            .pipe(first())
            .subscribe(
                user => {
                    this.isLoggedIn = this.authenticator.isLoggedIn();
                },
                error => { console.log(error); },

            )
        //this.http.post<any>(this.baseUrl + 'users/login', { username, password }).subscribe(user => {
        //    console.log(user);
        //    this.isLoggedIn = true;
        //}, error => console.error(error));

        //this.http.post<any>(`https://localhost:44372/users/login`, { username, password })
        //    .pipe(map(user => {
        //        // login successful if there's a jwt token in the response
        //        if (user && user.token) {
        //            // store user details and jwt token in local storage to keep user logged in between page refreshes
        //            localStorage.setItem('currentUser', JSON.stringify(user));
        //            this.currentUserSubject.next(user);
        //        }

        //        return user;
        //    }));
        
    }

}


