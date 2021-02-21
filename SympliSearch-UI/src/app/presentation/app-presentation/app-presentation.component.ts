import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-app-presentation',
  templateUrl: './app-presentation.component.html',
  styleUrls: ['./app-presentation.component.css']
})
export class AppPresentationComponent implements OnInit {

  @Input() providers;
  @Input() result;
  // tslint:disable-next-line: no-output-on-prefix
  @Output() onSearchClick = new EventEmitter();
  searchText = '';
  searchUrl = '';
  searchProvider = '';
  
  constructor() { }

  ngOnInit() {
  }

  onSearch() {
    const event = {
      queryString : this.searchText,
      url: this.searchUrl,
      searchProvicer: this.searchProvider
    };
    this.onSearchClick.emit(event);
  }

}
