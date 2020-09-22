import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-upload-transactions",
  templateUrl: "./upload.component.html",
})
export class UploadComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    console.log('Upload component init');
  }

}
