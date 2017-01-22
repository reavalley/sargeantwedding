import {ModuleWithProviders} from "@angular/core";
import {Routes, RouterModule} from "@angular/router";

import {AboutComponent} from "./about.component";
import {HomeComponent} from "./home.component";
import {LoginComponent } from "./login.component";
import {PageNotFoundComponent} from "./page-not-found.component";
import {UserEditComponent} from "./user-edit.component";
import {ItineryComponent} from "./content/itinery.component";
import {OurStoryComponent} from "./content/our-story.component";
import {BridalPartyComponent} from "./content/bridal-party.component";
import {MenuComponent} from "./content/menu.component";
import {DirectionsComponent} from "./content/directions.component";
import {AccommodationComponent} from "./content/accommodation.component";

const appRoutes: Routes = [
    {
        path: "",
        component: HomeComponent
    },
    {
        path: "home",
        redirectTo: ""
    },
    {
        path: "about",
        component: AboutComponent
    },
    {
        path: "login",
        component: LoginComponent
    },
    {
        path: "register",
        component: UserEditComponent
    },
    {
        path: "account",
        component: UserEditComponent
    },
    {
        path: "itinery",
        component: ItineryComponent
    },
    {
        path: "our-story",
        component: OurStoryComponent
    },
    {
        path: "bridal-party",
        component: BridalPartyComponent
    },
    {
        path: "menu",
        component: MenuComponent
    },
    {
        path: "directions",
        component: DirectionsComponent
    },
    {
        path: "accommodation",
        component: AccommodationComponent
    },
    {
        path: "**",
        component: PageNotFoundComponent
    }
];

export const AppRoutingProviders: any[] = [];

export const AppRouting: ModuleWithProviders = RouterModule.forRoot(appRoutes);

