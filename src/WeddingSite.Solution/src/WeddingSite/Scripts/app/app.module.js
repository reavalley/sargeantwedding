System.register(["@angular/core", "@angular/platform-browser", "@angular/http", "@angular/forms", "@angular/router", "rxjs/Rx", "./about.component", "./app.component", "./app.routing", "./home.component", "./auth.service", "./auth.http", "./login.component", "./page-not-found.component", "./user-edit.component", "./content/itinery.component", "./content/our-story.component", "./content/bridal-party.component", "./content/menu.component", "./content/directions.component", "./content/accommodation.component", "./content/wedding-wish.component", "./rsvp.component", "./rsvp.service"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, platform_browser_1, http_1, forms_1, router_1, about_component_1, app_component_1, app_routing_1, home_component_1, auth_service_1, auth_http_1, login_component_1, page_not_found_component_1, user_edit_component_1, itinery_component_1, our_story_component_1, bridal_party_component_1, menu_component_1, directions_component_1, accommodation_component_1, wedding_wish_component_1, rsvp_component_1, rsvp_service_1;
    var AppModule;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (platform_browser_1_1) {
                platform_browser_1 = platform_browser_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (forms_1_1) {
                forms_1 = forms_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (_1) {},
            function (about_component_1_1) {
                about_component_1 = about_component_1_1;
            },
            function (app_component_1_1) {
                app_component_1 = app_component_1_1;
            },
            function (app_routing_1_1) {
                app_routing_1 = app_routing_1_1;
            },
            function (home_component_1_1) {
                home_component_1 = home_component_1_1;
            },
            function (auth_service_1_1) {
                auth_service_1 = auth_service_1_1;
            },
            function (auth_http_1_1) {
                auth_http_1 = auth_http_1_1;
            },
            function (login_component_1_1) {
                login_component_1 = login_component_1_1;
            },
            function (page_not_found_component_1_1) {
                page_not_found_component_1 = page_not_found_component_1_1;
            },
            function (user_edit_component_1_1) {
                user_edit_component_1 = user_edit_component_1_1;
            },
            function (itinery_component_1_1) {
                itinery_component_1 = itinery_component_1_1;
            },
            function (our_story_component_1_1) {
                our_story_component_1 = our_story_component_1_1;
            },
            function (bridal_party_component_1_1) {
                bridal_party_component_1 = bridal_party_component_1_1;
            },
            function (menu_component_1_1) {
                menu_component_1 = menu_component_1_1;
            },
            function (directions_component_1_1) {
                directions_component_1 = directions_component_1_1;
            },
            function (accommodation_component_1_1) {
                accommodation_component_1 = accommodation_component_1_1;
            },
            function (wedding_wish_component_1_1) {
                wedding_wish_component_1 = wedding_wish_component_1_1;
            },
            function (rsvp_component_1_1) {
                rsvp_component_1 = rsvp_component_1_1;
            },
            function (rsvp_service_1_1) {
                rsvp_service_1 = rsvp_service_1_1;
            }],
        execute: function() {
            AppModule = (function () {
                function AppModule() {
                }
                AppModule = __decorate([
                    core_1.NgModule({
                        declarations: [
                            about_component_1.AboutComponent,
                            app_component_1.AppComponent,
                            home_component_1.HomeComponent,
                            login_component_1.LoginComponent,
                            page_not_found_component_1.PageNotFoundComponent,
                            user_edit_component_1.UserEditComponent,
                            itinery_component_1.ItineryComponent,
                            our_story_component_1.OurStoryComponent,
                            bridal_party_component_1.BridalPartyComponent,
                            menu_component_1.MenuComponent,
                            directions_component_1.DirectionsComponent,
                            accommodation_component_1.AccommodationComponent,
                            wedding_wish_component_1.WeddingWishComponent,
                            rsvp_component_1.RsvpComponent
                        ],
                        imports: [
                            platform_browser_1.BrowserModule,
                            http_1.HttpModule,
                            forms_1.FormsModule,
                            forms_1.ReactiveFormsModule,
                            router_1.RouterModule,
                            app_routing_1.AppRouting
                        ],
                        providers: [
                            auth_http_1.AuthHttp,
                            auth_service_1.AuthService,
                            rsvp_service_1.RsvpService
                        ],
                        bootstrap: [app_component_1.AppComponent]
                    }), 
                    __metadata('design:paramtypes', [])
                ], AppModule);
                return AppModule;
            }());
            exports_1("AppModule", AppModule);
        }
    }
});
