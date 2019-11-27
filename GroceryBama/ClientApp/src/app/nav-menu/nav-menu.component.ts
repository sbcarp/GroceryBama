import { Component, Inject, Output, EventEmitter, HostListener, ElementRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Authenticator } from 'src/app/_utilities/authenticator'
import { first } from 'rxjs/operators';
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
    currentStoreId = 1;
    currentStoreName = "";
    //@Output() toggleCart: EventEmitter<null> = new EventEmitter();
    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private authenticator: Authenticator,
        private router: Router,
        private navMenuService: NavMenuService) {
        this.http.get<any>(this.baseUrl + 'stores/getStores').subscribe(result => {
            this.stores = result.data;
            this.currentStoreId = this.stores[0]["id"];
            this.currentStoreName = this.stores[0]["name"];
            if (authenticator.isLoggedIn) {
                this.http.get<any>(this.baseUrl + 'stores/getcartquantity').subscribe(result => {
                    this.navMenuService.cartQuantityUpdate(result.data.cartQuantity);
                }, error => console.error(error));
                this.currentStoreId = authenticator.currentUser.groceryId;
                var currentStore = this.stores.find(element => { return element.id == this.currentStoreId });
                this.currentStoreName = currentStore["name"];
                this.navMenuService.setCurrentGroceryId(this.currentStoreId);
            }
        }, error => console.error(error));
        
        
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
        this.http.post<any>(this.baseUrl + 'stores/switchStore', groceryId).subscribe(result => {
            this.navMenuService.setCurrentGroceryId(groceryId);
            this.currentStoreId = groceryId;
            this.currentStoreName = name;
            this.authenticator.switchStore(groceryId);
        }, error => console.error(error));
    }
}


