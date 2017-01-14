import {Component} from "@angular/core";

@Component({
    selector: "about",
    template: `<h2>{{title}}</h2>
        <div>
            WeddingSite: a production-ready, fully-featued SPA sample powered by ASP.NET Core Web API and Angular 2.
        </div>
    `
})
export class AboutComponent {
    title = "About";
} 