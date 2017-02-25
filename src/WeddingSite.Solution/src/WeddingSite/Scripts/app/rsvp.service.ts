import {Injectable} from "@angular/core";
import {Http, Response, Headers, RequestOptions} from "@angular/http";
import {Observable} from "rxjs/Observable";
import {AuthHttp} from "./auth.http";

@Injectable()
export class RsvpService {
    constructor(private http: AuthHttp) { }

    private baseUrl = "api/rsvp/";

    getRsvp() {
        let url = this.baseUrl;
        
        return this.http.get(url)
            .map(response => response.json())
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || "Server error");
    }
}