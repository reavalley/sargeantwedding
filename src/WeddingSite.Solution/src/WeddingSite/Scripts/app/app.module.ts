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
import {AuthService} from "./auth.service";
import {AuthHttp} from "./auth.http";
import {LoginComponent} from "./login.component";
import {PageNotFoundComponent} from "./page-not-found.component";
import {UserEditComponent} from "./user-edit.component";
import {ItineryComponent} from "./content/itinery.component";
import {OurStoryComponent} from "./content/our-story.component";
import {BridalPartyComponent} from "./content/bridal-party.component";
import {MenuComponent} from "./content/menu.component";
import {DirectionsComponent} from "./content/directions.component";
import {AccommodationComponent} from "./content/accommodation.component";

@NgModule({
    declarations: [
        AboutComponent,
        AppComponent,
        HomeComponent,
        LoginComponent,
        PageNotFoundComponent,
        UserEditComponent,
        ItineryComponent,
        OurStoryComponent,
        BridalPartyComponent,
        MenuComponent,
        DirectionsComponent,
        AccommodationComponent
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
        AuthService
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
    
}
