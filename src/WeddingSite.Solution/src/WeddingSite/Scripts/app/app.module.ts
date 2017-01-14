///<reference path="../../typings/index.d.ts"/>
import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {HttpModule} from "@angular/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import "rxjs/Rx";

import {AboutComponent} from "./about.component";
import {AppComponent} from "./app.component";
import {AppRouting} from "./app.routing";
import {HomeComponent} from "./home.component";
import {ItemDetailEditComponent} from "./item-detail-edit.component";
import {ItemDetailViewComponent} from "./item-detail-view.component";
import {ItemListComponent} from "./item-list.component";
import {ItemService} from "./item.service";
import {AuthService} from "./auth.service";
import {AuthHttp} from "./auth.http";
import {LoginComponent} from "./login.component";
import {PageNotFoundComponent} from "./page-not-found.component";
import {UserEditComponent} from "./user-edit.component";

@NgModule({
    declarations: [
        AboutComponent,
        AppComponent,
        HomeComponent,
        ItemDetailEditComponent,
        ItemDetailViewComponent,
        ItemListComponent,        
        LoginComponent,
        PageNotFoundComponent,
        UserEditComponent
    ],
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        AppRouting
    ],
    providers: [
        AuthHttp,
        AuthService,
        ItemService
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
    
}
