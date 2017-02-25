import {Component, Input, OnInit} from "@angular/core";
import {AuthService} from "./auth.service";
import {User} from "./model/user";
import {MenuOption} from "./model/menuOption";
import {RsvpService} from "./rsvp.service";
import {Rsvp} from "./model/rsvp";

@Component({
    moduleId: __moduleName,
    selector: "rsvp",
    templateUrl: "rsvp.component.html"
})
export class RsvpComponent implements OnInit {
    @Input() class: string;

    title = "RSVP";
    errorMessage = null;
    rsvp: Rsvp;

    constructor(
        private rsvpService: RsvpService) {        
    }

    ngOnInit(): void {
        console.log("Loading menu options");
        
        this.rsvpService.getRsvp().subscribe(
            rsvp => {
                this.rsvp = rsvp;
                console.log(rsvp);
            },
            error => this.errorMessage = <any>error
        );
    }

    save() {
        event.preventDefault();        
    }
}