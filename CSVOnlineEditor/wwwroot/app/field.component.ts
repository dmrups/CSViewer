import { Input, Component }    from '@angular/core';
import './rxjs-operators';

import { Applicant }            from './applicant';
import { FocusDirective }       from './focus.directive'
import { ApplicantService }     from './app.service';

@Component({
    selector: 'field-editor',
    providers: [ApplicantService],
    template: `
        <div *ngIf="oldValue != null">
            <input [focus]="oldValue"
                    [(ngModel)]="row[field]"
                    (keyup.enter)="saveChanges(row)"
                    (focusout)="cancelEdit(row)" />
        </div>
        <div *ngIf="oldValue == null"
                (click)="oldValue = row[field]">
            {{row[field]}}
        </div>
`
})

export class FieldEditorComponent {
    @Input() row: any;
    @Input() field: string;

    constructor(private service: ApplicantService) { }

    private selected = false;
    private oldValue: string;

    private cancelEdit(row: any) {
        debugger;
        row[this.field] = this.oldValue; 
        this.oldValue = null;
    }

    private saveChanges(obj: Applicant) {
        this.oldValue = null;
        this.service.updateApplicant(obj).subscribe(
            (result) => obj[this.field] = result[this.field]);
    }
}