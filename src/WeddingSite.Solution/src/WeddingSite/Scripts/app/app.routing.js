System.register(["@angular/router", "./about.component", "./login.component", "./page-not-found.component", "./user-edit.component", "./content/itinery.component", "./content/our-story.component", "./content/bridal-party.component", "./content/menu.component", "./content/directions.component", "./content/accommodation.component", "./content/wedding-wish.component"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var router_1, about_component_1, login_component_1, page_not_found_component_1, user_edit_component_1, itinery_component_1, our_story_component_1, bridal_party_component_1, menu_component_1, directions_component_1, accommodation_component_1, wedding_wish_component_1;
    var appRoutes, AppRoutingProviders, AppRouting;
    return {
        setters:[
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (about_component_1_1) {
                about_component_1 = about_component_1_1;
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
            }],
        execute: function() {
            appRoutes = [
                {
                    path: "",
                    component: our_story_component_1.OurStoryComponent
                },
                {
                    path: "home",
                    redirectTo: ""
                },
                {
                    path: "about",
                    component: about_component_1.AboutComponent
                },
                {
                    path: "login",
                    component: login_component_1.LoginComponent
                },
                {
                    path: "register",
                    component: user_edit_component_1.UserEditComponent
                },
                {
                    path: "account",
                    component: user_edit_component_1.UserEditComponent
                },
                {
                    path: "itinery",
                    component: itinery_component_1.ItineryComponent
                },
                {
                    path: "our-story",
                    component: our_story_component_1.OurStoryComponent
                },
                {
                    path: "our-wedding-wish",
                    component: wedding_wish_component_1.WeddingWishComponent
                },
                {
                    path: "bridal-party",
                    component: bridal_party_component_1.BridalPartyComponent
                },
                {
                    path: "menu",
                    component: menu_component_1.MenuComponent
                },
                {
                    path: "directions",
                    component: directions_component_1.DirectionsComponent
                },
                {
                    path: "accommodation",
                    component: accommodation_component_1.AccommodationComponent
                },
                {
                    path: "**",
                    component: page_not_found_component_1.PageNotFoundComponent
                }
            ];
            exports_1("AppRoutingProviders", AppRoutingProviders = []);
            exports_1("AppRouting", AppRouting = router_1.RouterModule.forRoot(appRoutes));
        }
    }
});
