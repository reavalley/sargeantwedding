System.register(["@angular/core", "@angular/http", "rxjs/Observable", "./auth.http"], function (exports_1, context_1) {
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
    var core_1, http_1, Observable_1, auth_http_1, AuthService;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (Observable_1_1) {
                Observable_1 = Observable_1_1;
            },
            function (auth_http_1_1) {
                auth_http_1 = auth_http_1_1;
            }
        ],
        execute: function () {
            AuthService = (function () {
                function AuthService(http) {
                    this.http = http;
                    this.authKey = "auth";
                }
                AuthService.prototype.login = function (username, password) {
                    var _this = this;
                    var url = "api/connect/token";
                    var data = {
                        username: username,
                        password: password,
                        client_id: "WeddingSite",
                        grant_type: "password",
                        scope: "offline_access profile email"
                    };
                    return this.http.post(url, this.toUrlEncodedString(data), new http_1.RequestOptions({
                        headers: new http_1.Headers({
                            "Content-Type": "application/x-www-form-urlencoded"
                        })
                    }))
                        .map(function (response) {
                        var auth = response.json();
                        console.log("The following auth JSON object has been received: ");
                        console.log(auth);
                        _this.setAuth(auth);
                        return auth;
                    });
                };
                AuthService.prototype.logout = function () {
                    var _this = this;
                    return this.http.post("api/Accounts/Logout", null)
                        .map(function (response) {
                        _this.setAuth(null);
                        return true;
                    })
                        .catch(function (err) {
                        return Observable_1.Observable.throw(err);
                    });
                };
                AuthService.prototype.toUrlEncodedString = function (data) {
                    var body = "";
                    for (var key in data) {
                        if (body.length) {
                            body += "&";
                        }
                        body += key + "=";
                        body += encodeURIComponent(data[key]);
                    }
                    return body;
                };
                AuthService.prototype.setAuth = function (auth) {
                    if (auth) {
                        localStorage.setItem(this.authKey, JSON.stringify(auth));
                    }
                    else {
                        localStorage.removeItem(this.authKey);
                    }
                    return true;
                };
                AuthService.prototype.getAuth = function () {
                    var i = localStorage.getItem(this.authKey);
                    if (i) {
                        return JSON.parse(i);
                    }
                    else {
                        return null;
                    }
                };
                AuthService.prototype.isLoggedIn = function () {
                    return localStorage.getItem(this.authKey) != null;
                };
                AuthService.prototype.get = function () {
                    return this.http.get("api/Accounts")
                        .map(function (response) { return response.json(); });
                };
                AuthService.prototype.add = function (user) {
                    return this.http.post("api/Accounts", JSON.stringify(user), new http_1.RequestOptions({
                        headers: new http_1.Headers({
                            "Content-Type": "application/json"
                        })
                    }))
                        .map(function (response) { return response.json(); });
                };
                AuthService.prototype.update = function (user) {
                    return this.http.put("api/Accounts", JSON.stringify(user), new http_1.RequestOptions({
                        headers: new http_1.Headers({
                            "Content-Type": "application/json"
                        })
                    }))
                        .map(function (response) { return response.json(); });
                };
                return AuthService;
            }());
            AuthService = __decorate([
                core_1.Injectable(),
                __metadata("design:paramtypes", [auth_http_1.AuthHttp])
            ], AuthService);
            exports_1("AuthService", AuthService);
        }
    };
});
