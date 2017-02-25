System.register(["@angular/core", "./rsvp.service"], function(exports_1, context_1) {
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
    var core_1, rsvp_service_1;
    var RsvpComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (rsvp_service_1_1) {
                rsvp_service_1 = rsvp_service_1_1;
            }],
        execute: function() {
            RsvpComponent = (function () {
                function RsvpComponent(rsvpService) {
                    this.rsvpService = rsvpService;
                    this.title = "RSVP";
                    this.errorMessage = null;
                }
                RsvpComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    console.log("Loading menu options");
                    this.rsvpService.getRsvp().subscribe(function (rsvp) {
                        _this.rsvp = rsvp;
                        console.log(rsvp);
                    }, function (error) { return _this.errorMessage = error; });
                };
                RsvpComponent.prototype.save = function () {
                    event.preventDefault();
                };
                __decorate([
                    core_1.Input(), 
                    __metadata('design:type', String)
                ], RsvpComponent.prototype, "class", void 0);
                RsvpComponent = __decorate([
                    core_1.Component({
                        moduleId: __moduleName,
                        selector: "rsvp",
                        templateUrl: "rsvp.component.html"
                    }), 
                    __metadata('design:paramtypes', [rsvp_service_1.RsvpService])
                ], RsvpComponent);
                return RsvpComponent;
            }());
            exports_1("RsvpComponent", RsvpComponent);
        }
    }
});
