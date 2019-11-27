import { Injectable, Output, Input, EventEmitter } from '@angular/core'
import { BehaviorSubject, Observable, ReplaySubject} from 'rxjs';

@Injectable()
export class NavMenuService{
    currentGroceryId = new ReplaySubject<number>(1);
    constructor() {
        
    }
    @Output() cartQuantityUpdateEvent: EventEmitter<number> = new EventEmitter();
    cartQuantityUpdate(quantity: number) {
        this.cartQuantityUpdateEvent.emit(quantity);
    }

    getCurrentGroceryId(): Observable<number> {
        return this.currentGroceryId.asObservable();
    }
    setCurrentGroceryId(groceryId: number) {
        this.currentGroceryId.next(groceryId);
    }
}
