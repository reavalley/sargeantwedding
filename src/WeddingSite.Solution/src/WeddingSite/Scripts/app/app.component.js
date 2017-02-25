System.register(["@angular/core", "@angular/router", "./auth.service"], function(exports_1, context_1) {
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
    var core_1, router_1, auth_service_1;
    var AppComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (auth_service_1_1) {
                auth_service_1 = auth_service_1_1;
            }],
        execute: function() {
            AppComponent = (function () {
                function AppComponent(router, authService, zone) {
                    this.router = router;
                    this.authService = authService;
                    this.zone = zone;
                    this.title = "Ben and Hayley's Wedding";
                    if (!window.externalProviderLogin) {
                        var self = this;
                        window.externalProviderLogin = function (auth) {
                            self.zone.run(function () {
                                self.externalProviderLogin(auth);
                            });
                        };
                    }
                    this.currentUserName = this.getUserName();
                }
                AppComponent.prototype.getUserName = function () {
                    if (this.isLoggedIn()) {
                        this.authService.get().subscribe(function (user) {
                            if (user.IsSocialLogin) {
                                return "Logout " + user.Email;
                            }
                            else {
                                return "Logout" + user.UserName;
                            }
                        });
                    }
                    return "Logout";
                };
                AppComponent.prototype.isActive = function (data) {
                    return this.router.isActive(this.router.createUrlTree(data), true);
                };
                AppComponent.prototype.isLoggedIn = function () {
                    return this.authService.isLoggedIn();
                };
                AppComponent.prototype.logout = function () {
                    var _this = this;
                    this.authService.logout().subscribe(function (result) {
                        if (result) {
                            _this.router.navigate([""]);
                        }
                    });
                    return false;
                };
                AppComponent.prototype.externalProviderLogin = function (auth) {
                    this.authService.setAuth(auth);
                    console.log("External Login successful! Provider: " + this.authService.getAuth().providerName);
                    this.router.navigate([""]);
                };
                AppComponent = __decorate([
                    core_1.Component({
                        moduleId: __moduleName,
                        selector: "weddingsite",
                        templateUrl: "app.component.html"
                    }), 
                    __metadata('design:paramtypes', [router_1.Router, auth_service_1.AuthService, core_1.NgZone])
                ], AppComponent);
                return AppComponent;
            }());
            exports_1("AppComponent", AppComponent);
        }
    }
});
