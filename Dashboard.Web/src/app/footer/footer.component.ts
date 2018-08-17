import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  template: `
  <p class="text-center">
    OFA Dashboard
  </p>
  `,
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
