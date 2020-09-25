import { Component, OnInit } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { UploadService, UploadResult } from "./upload.service";

@Component({
  selector: "app-upload-transactions",
  templateUrl: "./upload.component.html",
  styleUrls: ["./upload.component.css"]
})
export class UploadComponent implements OnInit {
  file: any;
  uploadResult: UploadResult;
  constructor(private http: HttpClient, private uploadService: UploadService) { }

  ngOnInit(): void {
    console.log('Upload component init');
  }

  fileChanged(event) {
    this.file = event.target.files[0];
    console.log(this.file);
  }

  submit() {
    const formData = new FormData();

    formData.append("file", this.file);
    console.log(formData);

    this.uploadService.upload(formData).subscribe(
      (result) => {
        this.uploadResult = result;
        console.log(this.uploadResult);
        if (result.errors.length > 0) {
          console.log("Validation errors occured during uppload");
        } else {
          console.log("Success");
        }
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
