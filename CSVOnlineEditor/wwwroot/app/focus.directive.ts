import {Directive, Input, ElementRef} from '@angular/core';

@Directive({
    selector: '[focus]'
})

export class FocusDirective {
    @Input()
    focus: any;
    constructor( private element: ElementRef) { }
    protected ngOnChanges() {
        this.element.nativeElement.focus();
    }
}