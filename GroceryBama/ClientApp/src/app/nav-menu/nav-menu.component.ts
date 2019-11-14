import { Component, Inject, Output, EventEmitter, HostListener, ElementRef } from '@angular/core';
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
    isCartOpened = false;
    //@Output() toggleCart: EventEmitter<null> = new EventEmitter();
    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private authenticator: Authenticator,
        private router: Router,
        private elementRef: ElementRef) {
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
    @HostListener('document:click', ['$event'])
    onClick(event) {
        if (!this.elementRef.nativeElement.contains(event.target)) {
            this.isCartOpened = false;
        }
    }

}


