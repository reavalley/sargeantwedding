import {Injectable, EventEmitter} from "@angular/core";
import {Http, Headers, Response, RequestOptions} from "@angular/http";
import {Observable} from "rxjs/Observable";
import {AuthHttp} from "./auth.http";
import {User} from "./model/user";

@Injectable()
export class AuthService {
    authKey = "auth";

    constructor(private http: AuthHttp) {
        
    }

    login(username: string, password: string): any {
        var url = "api/connect/token";

        var data = {
            username: username,
            password: password,
            client_id: "WeddingSite",
            grant_type: "password",
            scope: "offline_access profile email"
        };

        return this.http.post(
                url,
                this.toUrlEncodedString(data),
                new RequestOptions({
                    headers: new Headers({
                        "Content-Type": "application/x-www-form-urlencoded"
                    })
                }))
            .map(response => {
                var auth = response.json();
                console.log("The following auth JSON object has been received: ");
                console.log(auth);
                this.setAuth(auth);
                return auth;
            });
    }

    logout(): any {
        return this.http.post("api/Accounts/Logout", null)
            .map(response => {
                this.setAuth(null);
                return true;
            })
            .catch(err => {
                return Observable.throw(err);
            });
    }

    toUrlEncodedString(data: any) {
        var body = "";
        for (var key in data) {
            if (body.length) {
                body += "&";
            }
            body += key + "=";
            body += encodeURIComponent(data[key]);
        }
        return body;
    }

    setAuth(auth: any): boolean {
        if (auth) {
            localStorage.setItem(this.authKey, JSON.stringify(auth));
        } else {
            localStorage.removeItem(this.authKey);
        }
        return true;
    }

    getAuth(): any {
        var i = localStorage.getItem(this.authKey);
        if (i) {
            return JSON.parse(i);
        } else {
            return null;
        }
    }

    isLoggedIn(): boolean {
        return localStorage.getItem(this.authKey) != null;
    }

    get() {
        return this.http.get("api/Accounts")
            .map(response => response.json());
    }

    add(user: User) {
        return this.http.post(
                "api/Accounts",
                JSON.stringify(user),
                new RequestOptions({
                    headers: new Headers({
                        "Content-Type": "application/json"
                    })
                }))
            .map(response => response.json());
    }

    update(user: User) {
        return this.http.put(
            "api/Accounts",
            JSON.stringify(user),
            new RequestOptions({
                headers: new Headers({
                    "Content-Type": "application/json"
                })
            }))
            .map(response => response.json());
    }
}
