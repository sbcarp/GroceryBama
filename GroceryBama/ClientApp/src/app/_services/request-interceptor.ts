import { Injectable, Inject } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Authenticator } from "./authenticator";

@Injectable()
export class RequestInterceptor implements HttpInterceptor {
    constructor(private authenticator: Authenticator, @Inject('BASE_URL') private baseUrl: string) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this.authenticator == undefined) return next.handle(request);
        var currentUser = this.authenticator.currentUser;
        if (currentUser != null && currentUser.token) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentUser.token}`,
                    //Accept: '',
                }
            });
        }

        return next.handle(request);
    }
}
