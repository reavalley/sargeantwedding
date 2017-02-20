import {Component, NgZone} from "@angular/core";
import {Router} from "@angular/router";
import {AuthService} from "./auth.service";

@Component({
    moduleId: __moduleName,
    selector: "weddingsite",
    templateUrl: "app.component.html"
})
export class AppComponent {
    title = "Ben and Hayley's Wedding";
    userName: string;

    constructor(public router: Router, public authService: AuthService, public zone: NgZone) {
        if (!(<any>window).externalProviderLogin) {
            var self = this;

            (<any>window).externalProviderLogin = function (auth) {
                self.zone.run(() => {
                    self.externalProviderLogin(auth);
                });
            }
        }
    }
    
    getUserName(): string {
        if (this.isLoggedIn()) {
            this.authService.get().subscribe(
                user => {
                    if (user.IsSocialLogin) {
                        return user.Email;
                    }
                    else {
                        return user.UserName;
                    }
                }
            );
        }
        return "";
    }

    isActive(data: any[]): boolean {
        return this.router.isActive(this.router.createUrlTree(data), true);       
    }

    isLoggedIn(): boolean {
        return this.authService.isLoggedIn();
    }

    logout(): boolean {
        this.authService.logout().subscribe(result => {

            if (result) {
                this.router.navigate([""]);
            }
        });
           
        return false;
    }

    externalProviderLogin(auth: any) {
        this.authService.setAuth(auth);

        console.log("External Login successful! Provider: " + this.authService.getAuth().providerName);
        this.router.navigate([""]);
    }
} 