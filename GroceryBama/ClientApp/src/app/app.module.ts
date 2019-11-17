import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { RequestInterceptor } from './_utilities/request-interceptor'


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NavMenuService } from './nav-menu/nav-menu.service'
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { ManagerComponent } from './manager/manager.component';
import { DelivererComponent } from './deliverer/deliverer.component';
import { BuyerComponent } from './buyer/buyer.component';
import { CartComponent } from './cart/cart.component';
import { AccountComponent } from './account/account.component';
import { AddressComponent } from './address/address.component';
import { PaymentMethodsComponent } from './payment-methods/payment-methods.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { MyOrdersComponent } from './my-orders/my-orders.component';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { ItemsListComponent } from './items-list/items-list.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';
import { MatInputModule } from '@angular/material/input';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSelectModule } from '@angular/material/select';
import { MatDividerModule } from '@angular/material/divider';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        LoginComponent,
        AccountComponent,
        ManagerComponent,
        DelivererComponent,
        BuyerComponent,
        CartComponent,
        AddressComponent,
        PaymentMethodsComponent,
        CheckoutComponent,
        MyOrdersComponent,
        OrderDetailsComponent,
        ItemsListComponent,
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: '/home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'login', component: LoginComponent },
            { path: 'cart', component: CartComponent },
            { path: 'account', component: AccountComponent },
            { path: 'checkout', component: CheckoutComponent },
            { path: 'myorders', component: MyOrdersComponent },
            { path: 'myorders/orderdetails', component: OrderDetailsComponent },
        ]),
        BrowserAnimationsModule,
        MatButtonModule,
        MatCheckboxModule,
        MatRadioModule,
        MatInputModule,
        MatExpansionModule,
        MatSelectModule,
        MatDividerModule
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: RequestInterceptor, multi: true },
        NavMenuService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
