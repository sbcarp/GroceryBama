import { Injectable, Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, ReplaySubject } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';


@Injectable({ providedIn: 'root' })
export class Authenticator{
    private _currentUser;
    private currentGroceryId = new ReplaySubject<number>(1);
    private currentGroceryIdValue: number = 1;
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
        console.log("Authenticator is initialized");
        this._currentUser = JSON.parse(localStorage.getItem('currentUser'));
        //var gid = localStorage.getItem('currentGroceryId');
        this.setGroceryId(1);
        if (this._currentUser != null) {
            fetch(this.baseUrl + 'users/CheckAuthenticationStatus', { headers: new Headers({ "Authorization": "Bearer " + this._currentUser.token }) })
                .then(response => { if (response.status == 401) this.logout() })
                .catch(() => console.log("CheckAuthenticationStatus network error"));
        }
        
    }

    public get isLoggedIn() {
        return (this._currentUser != null && this._currentUser.token != null);
    }
    public get role() {
        if (this._currentUser == null) return null;
        return this._currentUser.role;
    }
    public get currentUser() {
        return this._currentUser;
    }
    public get groceryId(): Observable<number> {
        return this.currentGroceryId.asObservable();
    }
    public get groceryIdValue(): number {
        return this.currentGroceryIdValue;
    }
    public setGroceryId(groceryId: number) {
        //localStorage.setItem('currentGroceryId', this.currentGroceryId._getNow().toString());
        this.currentGroceryIdValue = groceryId;
        this.currentGroceryId.next(groceryId);
    }
    switchStore(groceryId: number) {
        return this.http.post<any>(this.baseUrl + 'stores/switchStore', groceryId).pipe(map(() =>{
            if (this._currentUser != null)this._currentUser.groceryId = groceryId;
            this.setGroceryId(groceryId);
        }));
    }
    login(username: string, password: string) {
        this.logout();
        return this.http.post<any>(this.baseUrl + 'users/login', { username, password })
            .pipe(map(result => {
                    if (result['success']) {
                        localStorage.setItem('currentUser', JSON.stringify(result['data']));
                        this._currentUser = result['data'];
                        this.setGroceryId(this._currentUser.groceryId);
                    }
                    return result;
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
    groceryId: number;
}
