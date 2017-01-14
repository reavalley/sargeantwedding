import {Component, OnInit} from "@angular/core";
import {Router, ActivatedRoute} from "@angular/router";
import {Item} from "./item";
import {ItemService} from "./item.service";
import {AuthService} from "./auth.service";

@Component({
    moduleId: __moduleName,
    selector: "item-detail-edit",
    templateUrl: "item-detail-edit.component.html"
})

export class ItemDetailEditComponent {
    item: Item;

    constructor(
        private authService: AuthService,
        private itemService: ItemService,
        private router: Router,
        private activatedRoute: ActivatedRoute) {
    }

    ngOnInit(): void {
        if (!this.authService.isLoggedIn()) {
            this.router.navigate([""]);
        }
        var id = + this.activatedRoute.snapshot.params["id"];

        if (id) {
            this.itemService.get(id).subscribe(item => this.item = item);            
        }
        else if (id === 0) {
            console.log("id is 0: adding a new item...");
            this.item = new Item(0, "New Item", null);
        }
        else {
            console.log("Invalid id: routing back to home...");
            this.router.navigate([""]);
        }
    }

    onInsert(item: Item) {
        this.itemService.add(item).subscribe(
            (data) => {
                this.item = data;
                console.log("Item " + this.item.Id + " has been added");
                this.router.navigate([""]);
            },
            (error) => console.log(error)
        );
    }

    onUpdate(item: Item) {
        this.itemService.update(item).subscribe(
            (data) => {
                this.item = data;
                console.log("Item " + this.item.Id + " has been updated");
                this.router.navigate(["item/view", this.item.Id]);
            },
            (error) => console.log(error)
        );
    }

    onDelete(item: Item) {
        var id = item.Id;
        this.itemService.delete(id).subscribe(
            (data) => {
                console.log("Item " + id + " has been deleted");
                this.router.navigate([""]);
            },
            (error) => console.log(error)
        );
    }

    onBack() {
        this.router.navigate([""]);
    }

    onItemDetailView(item: Item) {
        this.router.navigate(["item/view", item.Id]);
    }
}