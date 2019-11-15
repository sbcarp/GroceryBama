import { Injectable, Output, EventEmitter } from '@angular/core'

@Injectable()
export class NavMenuService{
    constructor() {

    }
    @Output() cartQuantityUpdateEvent: EventEmitter<number> = new EventEmitter();

    cartQuantityUpdate(quantity: number) {
        this.cartQuantityUpdateEvent.emit(quantity);
    }
}
