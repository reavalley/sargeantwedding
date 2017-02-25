System.register(["@angular/core", "rxjs/Observable", "./auth.http"], function(exports_1, context_1) {
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
    var core_1, Observable_1, auth_http_1;
    var RsvpService;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (Observable_1_1) {
                Observable_1 = Observable_1_1;
            },
            function (auth_http_1_1) {
                auth_http_1 = auth_http_1_1;
            }],
        execute: function() {
            RsvpService = (function () {
                function RsvpService(http) {
                    this.http = http;
                    this.baseUrl = "api/rsvp/";
                }
                RsvpService.prototype.getRsvp = function () {
                    var url = this.baseUrl;
                    return this.http.get(url)
                        .map(function (response) { return response.json(); })
                        .catch(this.handleError);
                };
                RsvpService.prototype.handleError = function (error) {
                    console.error(error);
                    return Observable_1.Observable.throw(error.json().error || "Server error");
                };
                RsvpService = __decorate([
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [auth_http_1.AuthHttp])
                ], RsvpService);
                return RsvpService;
            }());
            exports_1("RsvpService", RsvpService);
        }
    }
});
