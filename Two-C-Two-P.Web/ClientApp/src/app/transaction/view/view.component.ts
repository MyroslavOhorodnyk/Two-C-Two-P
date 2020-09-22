import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-view-transactions",
  templateUrl: "./view.component.html",
})
export class ViewComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    console.log('View component init');
  }
  
}
