import { Injectable }               from "@angular/core";
import { Http, Response }           from "@angular/http";
import { Headers, RequestOptions }  from "@angular/http";

import { Applicant }                from "./applicant";
import { Observable }               from "rxjs/Observable";

@Injectable()
export class ApplicantService {
    private applicantsUrl = "api/applicants";
    private csvUrl = "api/csv/applicants";

    constructor(private http: Http) { }

    getApplicants(): Observable<Applicant[]> {
        return this.http.get(this.applicantsUrl)
            .map(this.extractData)
            .catch(this.handleError);
    }

    getCSV(): Observable<Response> {
        return this.http.get(this.csvUrl)
            .catch(this.handleError);
    }

    updateApplicant(obj: Applicant): Observable<Applicant> {
        let route = this.applicantsUrl + '/' + obj.id;
        return this.http.put(route, obj, this.getDefaultRequestOptions())
            .map(this.extractData)
            .catch(this.handleError);
    }
    
    uploadFile(files: string[]): Observable<Response> {
        return this.http.post(this.csvUrl, files, this.getDefaultRequestOptions())
            .catch(this.handleError);
    }

    cleanDB(): Observable<Response> {
        return this.http.delete(this.applicantsUrl)
            .catch(this.handleError);
    }

    private getDefaultRequestOptions(): RequestOptions {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        return new RequestOptions({ headers: headers });
    }
    
    private extractData(res: Response) {
        return res.json() || res;
    }

    private handleError(error: Response | any) {
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable.throw(errMsg);
    }

    downloadFile(res: Response) {
        var blob = new Blob([res.text()], { type: 'text/csv' });
        saveAs(blob, 'applicants.csv');
    }
}