System.register(["@angular/core", "@angular/forms", "@angular/router", "./auth.service", "./model/user"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var __moduleName = context_1 && context_1.id;
    var core_1, forms_1, router_1, auth_service_1, user_1, UserEditComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (forms_1_1) {
                forms_1 = forms_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (auth_service_1_1) {
                auth_service_1 = auth_service_1_1;
            },
            function (user_1_1) {
                user_1 = user_1_1;
            }
        ],
        execute: function () {
            UserEditComponent = (function () {
                function UserEditComponent(fb, router, activatedRoute, authService) {
                    this.fb = fb;
                    this.router = router;
                    this.activatedRoute = activatedRoute;
                    this.authService = authService;
                    this.title = "New User Registration";
                    this.errorMessage = null;
                    this.isRegister = (activatedRoute.snapshot.url[0].path === "register");
                    if ((this.isRegister && this.authService.isLoggedIn())
                        || (!this.isRegister && !this.authService.isLoggedIn())) {
                        this.router.navigate([""]);
                    }
                    if (!this.isRegister) {
                        this.title = "Edit Account";
                    }
                }
                UserEditComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    this.userForm = this.fb.group({
                        username: ["", [
                                forms_1.Validators.required,
                                forms_1.Validators.pattern("[a-zA-Z0-9]+")
                            ]],
                        email: ["", [
                                forms_1.Validators.required,
                                forms_1.Validators.pattern("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9]*[a-z0-9])?")
                            ]],
                        password: ["", [
                                forms_1.Validators.required,
                                forms_1.Validators.minLength(6)
                            ]],
                        passwordConfirm: ["", [
                                forms_1.Validators.required,
                                forms_1.Validators.minLength(6)
                            ]],
                        displayName: ["", null]
                    }, {
                        validator: this.compareValidator('password', 'passwordConfirm')
                    });
                    if (!this.isRegister) {
                        //add new control for current password
                        this.userForm.addControl("passwordCurrent", new forms_1.FormControl("", forms_1.Validators.required));
                        //remove required validators from password
                        var password = this.userForm.find("password");
                        password.clearValidators();
                        password.setValidators(forms_1.Validators.minLength(6));
                        //remove required validator from password confirm
                        var passwordConfirm = this.userForm.find("passwordConfirm");
                        passwordConfirm.clearValidators();
                        passwordConfirm.setValidators(forms_1.Validators.minLength(6));
                        this.authService.get().subscribe(function (user) {
                            _this.userForm.find("username")
                                .setValue(user.UserName);
                            _this.userForm.find("email")
                                .setValue(user.Email);
                            _this.userForm.find("displayName")
                                .setValue(user.DisplayName);
                        });
                    }
                };
                UserEditComponent.prototype.compareValidator = function (fc1, fc2) {
                    return function (group) {
                        var password = group.controls[fc1];
                        var passwordConfirm = group.controls[fc2];
                        if (password.value === passwordConfirm.value) {
                            return null;
                        }
                        return { compareFailed: true };
                    };
                };
                UserEditComponent.prototype.onSubmit = function () {
                    var _this = this;
                    event.preventDefault();
                    if (this.isRegister) {
                        this.authService.add(this.userForm.value)
                            .subscribe(function (data) {
                            if (data.error == null) {
                                _this.errorMessage = null;
                                _this.authService.login(_this.userForm.value.username, _this.userForm.value.password)
                                    .subscribe(function (data) {
                                    _this.errorMessage = null;
                                    _this.router.navigate([""]);
                                }, function (err) {
                                    console.log(err);
                                    _this.errorMessage = "Warning: Username or password mismatch";
                                });
                            }
                            else {
                                _this.errorMessage = data.error;
                            }
                        }, function (err) {
                            _this.errorMessage = err;
                        });
                    }
                    else {
                        var user = new user_1.User(this.userForm.value.username, this.userForm.value.passwordCurrent, this.userForm.value.password, this.userForm.value.email, this.userForm.value.displayName);
                        this.authService.update(user)
                            .subscribe(function (data) {
                            if (data.error == null) {
                                // update successful
                                _this.errorMessage = null;
                                _this.router.navigate([""]);
                            }
                            else {
                                // update failure
                                _this.errorMessage = data.error;
                            }
                        }, function (err) {
                            // server/connection error
                            _this.errorMessage = err;
                        });
                    }
                };
                return UserEditComponent;
            }());
            UserEditComponent = __decorate([
                core_1.Component({
                    moduleId: __moduleName,
                    selector: "user-edit",
                    templateUrl: "user-edit.component.html"
                }),
                __metadata("design:paramtypes", [forms_1.FormBuilder,
                    router_1.Router,
                    router_1.ActivatedRoute,
                    auth_service_1.AuthService])
            ], UserEditComponent);
            exports_1("UserEditComponent", UserEditComponent);
        }
    };
});
