import {Component, OnInit} from "@angular/core";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Router, ActivatedRoute} from "@angular/router";
import {AuthService} from "./auth.service";
import {User} from "./model/user";

@Component({
    moduleId: __moduleName,
    selector: "user-edit",
    templateUrl: "user-edit.component.html"
})
export class UserEditComponent {
    title = "New User Registration";
    userForm: FormGroup;
    errorMessage = null;
    isRegister: boolean;

    constructor(
        private fb: FormBuilder,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private authService: AuthService) {

        this.isRegister = (activatedRoute.snapshot.url[0].path === "register");
        if ((this.isRegister && this.authService.isLoggedIn())
            || (!this.isRegister && !this.authService.isLoggedIn())) {
            this.router.navigate([""]);
        }
        if (!this.isRegister) {
            this.title = "Edit Account";
        }
    }

    ngOnInit() {
        this.userForm = this.fb.group(
            {
                username: ["", [
                    Validators.required,
                    Validators.pattern("[a-zA-Z0-9]+")
                ]],
                email: ["", [
                    Validators.required,
                    Validators.pattern("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9]*[a-z0-9])?")
                ]],
                password: ["", [
                    Validators.required,
                    Validators.minLength(6)
                ]],
                passwordConfirm: ["", [
                    Validators.required,
                    Validators.minLength(6)
                ]],
                displayName: ["", null]            
            },
            {
                validator: this.compareValidator('password', 'passwordConfirm')
            }
        );

        if (!this.isRegister) {
            //add new control for current password
            this.userForm.addControl("passwordCurrent",new FormControl("", Validators.required));

            //remove required validators from password
            var password = this.userForm.find("password");
            password.clearValidators();
            password.setValidators(Validators.minLength(6));

            //remove required validator from password confirm
            var passwordConfirm = this.userForm.find("passwordConfirm");
            passwordConfirm.clearValidators();
            passwordConfirm.setValidators(Validators.minLength(6));

            this.authService.get().subscribe(
                user => {
                    this.userForm.find("username")
                        .setValue(user.UserName);
                    this.userForm.find("email")
                        .setValue(user.Email);
                    this.userForm.find("displayName")
                        .setValue(user.DisplayName);
                }
            );
        }
    }

    compareValidator(fc1: string, fc2: string) {
        return (group: FormGroup): { [key: string]: any } => {
            let password = group.controls[fc1];
            let passwordConfirm = group.controls[fc2];

            if (password.value === passwordConfirm.value) {
                return null;
            }
            return { compareFailed: true }
        }
    }

    onSubmit() {
        event.preventDefault();

        if (this.isRegister) {
            this.authService.add(this.userForm.value)
                .subscribe((data) => {
                    if (data.error == null) {
                        this.errorMessage = null;
                        this.authService.login(this.userForm.value.username, this.userForm.value.password)
                            .subscribe((data) => {
                                this.errorMessage = null;
                                this.router.navigate([""]);
                            },
                            (err) => {
                                console.log(err);
                                this.errorMessage = "Warning: Username or password mismatch";
                            });
                    }
                    else {
                        this.errorMessage = data.error;
                    }
                },
                (err) => {
                    this.errorMessage = err;
                });
        }
        else {
            let user = new User(
                this.userForm.value.username,
                this.userForm.value.passwordCurrent,
                this.userForm.value.password,
                this.userForm.value.email,
                this.userForm.value.displayName);
            this.authService.update(user)
                .subscribe((data) => {
                    if (data.error == null) {
                        // update successful
                        this.errorMessage = null;
                        this.router.navigate([""]);
                    } else {
                        // update failure
                        this.errorMessage = data.error;
                    }
                },
                (err) => {
                    // server/connection error
                    this.errorMessage = err;
                });
        }
    }
}