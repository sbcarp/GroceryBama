import { Component, Inject, Output, EventEmitter, HostListener, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Authenticator } from 'src/app/_services/authenticator'
import { first } from 'rxjs/operators';
import { Subscription } from 'rxjs';
import { NavMenuService } from './nav-menu.service'

@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
    isExpanded = false;
    cartQuantity = 0;
    stores = [];
    currentStoreName = "";
    groceryIdSubscription: Subscription;
    groceryId: number;
    //@Output() toggleCart: EventEmitter<null> = new EventEmitter();
    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private authenticator: Authenticator,
        private router: Router,
        private navMenuService: NavMenuService) {
        
        this.http.get<any>(this.baseUrl + 'stores/getStores').subscribe(result => {
            this.stores = result.data;
            this.groceryIdSubscription = authenticator.groceryId.subscribe(groceryId => {
                this.groceryId = groceryId;
                this.currentStoreName = this.stores.find(element => { return element.id == groceryId }).name;
                this.getCartQuantity();
            });
        }, error => console.error(error));
    }
    getCartQuantity() {
        if (this.authenticator.isLoggedIn && this.authenticator.role == "buyer") {
            var params = new HttpParams().append('groceryId', this.groceryId.toString())
            this.http.get<any>(this.baseUrl + 'stores/getcartquantity', { params }).subscribe(result => {
                this.navMenuService.cartQuantityUpdate(result.data.cartQuantity);
            }, error => console.error(error));
        }
    }
    ngOnDestroy() {
        this.groceryIdSubscription.unsubscribe();
    }
    ngOnInit() {
        this.navMenuService.cartQuantityUpdateEvent.subscribe(quantity => {
            this.cartQuantity = quantity
        });
    }
    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }


    logout() {
        this.authenticator.logout();
    }

    login(username: string, password: string) {
        this.authenticator.login(username, password)
            .pipe(first())
            .subscribe(
                user => {
                },
                error => { console.log(error); },

            )
    }

    switchStore(groceryId: number, name: string) {
        this.authenticator.switchStore(groceryId).pipe()
            .subscribe(() => {
                this.currentStoreName = name;
            },
            error => { console.log(error); })
    }
}


