import { Component, OnInit }    from '@angular/core';
import './rxjs-operators';


import { Applicant }            from './applicant';
import { ApplicantService}      from './app.service';

@Component({
    selector: 'app',
    providers: [ApplicantService],
    templateUrl: 'app/app.component.html'
})

export class AppComponent implements OnInit {
    title = 'Applicants for Mars Invasion Program';
    data = new Array<Applicant>();
    error: string;

    constructor(private service: ApplicantService) { }

    ngOnInit() { this.getApplicants(); }

    private getApplicants() {
        this.service.getApplicants()
            .subscribe(applicants => this.applyData(applicants));
    }

    private applyData(data: Applicant[]) {
        this.data = data;
    }

    private fileInputChanged(event) {
        var files = event.srcElement.files;

        var texts = new Array<string>();
        var count = files.length;

        for (var file of files) {
            this.readFile(file, target => {
                texts.push(target.result);
                count--;

                if (count == 0) {
                    this.service.uploadFile(texts).subscribe(
                        () => this.getApplicants(),
                        (err) => this.showError(err.message || 'Unrecognized error'));
                }
            });
        }
    }

    private downloadCSV() {
        this.service.getCSV().subscribe((csv) => this.service.downloadFile(csv));
    }

    private cleanDB() {
        this.service.cleanDB().subscribe(() => this.getApplicants());
    }

    private showError(msg: string) {
        this.error = 'Error: ' + msg;
    }

    private readFile(file: File, callback: Function) {
        let reader = new FileReader();
        reader.onload = e => callback(e.target);
        reader.readAsText(file);
    }
}
