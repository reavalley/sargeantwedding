System.register(["@angular/router", "./about.component", "./home.component", "./item-detail-edit.component", "./item-detail-view.component", "./login.component", "./page-not-found.component", "./user-edit.component"], function (exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var router_1, about_component_1, home_component_1, item_detail_edit_component_1, item_detail_view_component_1, login_component_1, page_not_found_component_1, user_edit_component_1, appRoutes, AppRoutingProviders, AppRouting;
    return {
        setters: [
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (about_component_1_1) {
                about_component_1 = about_component_1_1;
            },
            function (home_component_1_1) {
                home_component_1 = home_component_1_1;
            },
            function (item_detail_edit_component_1_1) {
                item_detail_edit_component_1 = item_detail_edit_component_1_1;
            },
            function (item_detail_view_component_1_1) {
                item_detail_view_component_1 = item_detail_view_component_1_1;
            },
            function (login_component_1_1) {
                login_component_1 = login_component_1_1;
            },
            function (page_not_found_component_1_1) {
                page_not_found_component_1 = page_not_found_component_1_1;
            },
            function (user_edit_component_1_1) {
                user_edit_component_1 = user_edit_component_1_1;
            }
        ],
        execute: function () {
            appRoutes = [
                {
                    path: "",
                    component: home_component_1.HomeComponent
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
                    path: "item/edit/:id",
                    component: item_detail_edit_component_1.ItemDetailEditComponent
                },
                {
                    path: "item/view/:id",
                    component: item_detail_view_component_1.ItemDetailViewComponent
                },
                {
                    path: "**",
                    component: page_not_found_component_1.PageNotFoundComponent
                }
            ];
            exports_1("AppRoutingProviders", AppRoutingProviders = []);
            exports_1("AppRouting", AppRouting = router_1.RouterModule.forRoot(appRoutes));
        }
    };
});
