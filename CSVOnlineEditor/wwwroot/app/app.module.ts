import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { FieldEditorComponent } from './field.component'
import { FocusDirective } from './focus.directive'
import { FormsModule }   from '@angular/forms';
import { HttpModule, JsonpModule }  from '@angular/http';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        JsonpModule,
    ],
    declarations: [
        AppComponent,
        FieldEditorComponent,
        FocusDirective
    ],
    bootstrap: [AppComponent]
})

export class AppModule { }