import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class Authenticator {
    public _currentUser: User;
    constructor(private http: HttpClient,
        @Inject('BASE_URL')
        private baseUrl: string) {
        this._currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }
    public get isLoggedIn() {
        return (this._currentUser != null && this._currentUser.token != null);
    }
    public get currentUser(): User {
        return this._currentUser;
    }
    login(username: string, password: string) {
        this.logout();
        return this.http.post<any>(this.baseUrl + 'users/login', { username, password })
            .pipe(map(user => {
                if (user) localStorage.setItem('currentUser', JSON.stringify(user));
                this._currentUser = user;
                return user;
            }));
    }
    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this._currentUser = null;
    }
}

export class User {
    username: string;
    firstname: string;
    lastname: string;
    role: string;
    token?: string;
}
