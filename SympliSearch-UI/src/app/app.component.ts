import { Component, OnInit } from '@angular/core';
import { SearchService } from './services/search.services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'SympliSearch-UI';
  providers;
  result;
  constructor(private services: SearchService) { }
  ngOnInit() {
    this.services.GetProviders().subscribe(
      res => {
        res.push('ALL');
        this.providers = res;
      });
  }

  onSearch(event) {
    if (event.searchProvicer === 'ALL') {
      this.services.SearchAll(event.queryString, event.url).subscribe(
        res => {
          this.result = res;
        });
    } else {
    this.services.Search(event.queryString, event.url, event.searchProvicer).subscribe(
      res => {
        this.result = res;
      },
      (error) => {
        alert(error.statusText + ' : ' + error.error);
      });
    }
  }
}
