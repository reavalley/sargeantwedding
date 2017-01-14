System.register(["@angular/core", "@angular/router", "./item", "./item.service", "./auth.service"], function (exports_1, context_1) {
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
    var core_1, router_1, item_1, item_service_1, auth_service_1, ItemDetailEditComponent;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (item_1_1) {
                item_1 = item_1_1;
            },
            function (item_service_1_1) {
                item_service_1 = item_service_1_1;
            },
            function (auth_service_1_1) {
                auth_service_1 = auth_service_1_1;
            }
        ],
        execute: function () {
            ItemDetailEditComponent = (function () {
                function ItemDetailEditComponent(authService, itemService, router, activatedRoute) {
                    this.authService = authService;
                    this.itemService = itemService;
                    this.router = router;
                    this.activatedRoute = activatedRoute;
                }
                ItemDetailEditComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    if (!this.authService.isLoggedIn()) {
                        this.router.navigate([""]);
                    }
                    var id = +this.activatedRoute.snapshot.params["id"];
                    if (id) {
                        this.itemService.get(id).subscribe(function (item) { return _this.item = item; });
                    }
                    else if (id === 0) {
                        console.log("id is 0: adding a new item...");
                        this.item = new item_1.Item(0, "New Item", null);
                    }
                    else {
                        console.log("Invalid id: routing back to home...");
                        this.router.navigate([""]);
                    }
                };
                ItemDetailEditComponent.prototype.onInsert = function (item) {
                    var _this = this;
                    this.itemService.add(item).subscribe(function (data) {
                        _this.item = data;
                        console.log("Item " + _this.item.Id + " has been added");
                        _this.router.navigate([""]);
                    }, function (error) { return console.log(error); });
                };
                ItemDetailEditComponent.prototype.onUpdate = function (item) {
                    var _this = this;
                    this.itemService.update(item).subscribe(function (data) {
                        _this.item = data;
                        console.log("Item " + _this.item.Id + " has been updated");
                        _this.router.navigate(["item/view", _this.item.Id]);
                    }, function (error) { return console.log(error); });
                };
                ItemDetailEditComponent.prototype.onDelete = function (item) {
                    var _this = this;
                    var id = item.Id;
                    this.itemService.delete(id).subscribe(function (data) {
                        console.log("Item " + id + " has been deleted");
                        _this.router.navigate([""]);
                    }, function (error) { return console.log(error); });
                };
                ItemDetailEditComponent.prototype.onBack = function () {
                    this.router.navigate([""]);
                };
                ItemDetailEditComponent.prototype.onItemDetailView = function (item) {
                    this.router.navigate(["item/view", item.Id]);
                };
                return ItemDetailEditComponent;
            }());
            ItemDetailEditComponent = __decorate([
                core_1.Component({
                    moduleId: __moduleName,
                    selector: "item-detail-edit",
                    templateUrl: "item-detail-edit.component.html"
                }),
                __metadata("design:paramtypes", [auth_service_1.AuthService,
                    item_service_1.ItemService,
                    router_1.Router,
                    router_1.ActivatedRoute])
            ], ItemDetailEditComponent);
            exports_1("ItemDetailEditComponent", ItemDetailEditComponent);
        }
    };
});
