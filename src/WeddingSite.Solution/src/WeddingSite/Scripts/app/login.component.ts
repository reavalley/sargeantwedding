import {Component} from "@angular/core";
import {FormBuilder, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {AuthService} from "./auth.service";
import {UserEditComponent} from "./user-edit.component";

@Component({
    moduleId: __moduleName,
    selector: "login",
    templateUrl: "login.component.html"
})
export class LoginComponent {
    title = "Login";
    loginForm = null;
    loginError = false;
    externalProviderWindow = null;

    constructor(private fb: FormBuilder, private router: Router, private authService: AuthService) {
        if (this.authService.isLoggedIn()) {
            this.router.navigate([""]);
        }
        this.loginForm = fb.group({
            username: ["", Validators.required],
            password: ["", Validators.required]
        });
    }

    performLogin(e) {
        e.preventDefault();

        var username = this.loginForm.value.username;
        var password = this.loginForm.value.password;

        this.authService.login(username, password)
            .subscribe((data) => {
                    this.loginError = false;
                    var auth = this.authService.getAuth();
                    alert("Our Token is: " + auth.access_token);
                    this.router.navigate([""]);
                },
                (err) => {
                    console.log(err);
                    this.loginError = true;
                });
    }

    callExternalLogin(providerName: string) {
        var url = "api/Accounts/ExternalLogin/" + providerName;

        var w = (screen.width >= 1050) ? 1050 : screen.width;
        var h = (screen.height >= 550) ? 550 : screen.height;

        var params = "toolbar=yes, scrollbars=yes, resizable=yes,width=" + w + ", height=" + h;

        if (this.externalProviderWindow) {
            this.externalProviderWindow.close();
        }
        //this is not great in production = use proper SDK from facebook or google OAuth2 javascript SDK.
        this.externalProviderWindow = window.open(url, "ExternalProvider", params, false);
    }

    onRegister() {
        this.router.navigate(["register"]);
    }
} 