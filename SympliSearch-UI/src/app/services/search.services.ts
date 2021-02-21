import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
  })
  export class SearchService {
    constructor(private http: HttpClient) {}

    GetProviders(): Observable<string[]> {
        return this.http.get<string[]>('http://localhost:4300/api/Search/GetAllProviders');
    }

    Search(queryString: string, url: string, searchProvicer: string): Observable<string[]> {
        let parameters = new HttpParams();
        parameters = parameters.set('queryString', queryString);
        parameters = parameters.set('url', url);
        parameters = parameters.set('searchProvicer', searchProvicer);
        return this.http.get<string[]>('http://localhost:4300/api/Search', {params : parameters});
    }

    SearchAll(queryString: string, url: string): Observable<string[]> {
        let parameters = new HttpParams();
        parameters = parameters.set('queryString', queryString);
        parameters = parameters.set('url', url);
        return this.http.get<string[]>('http://localhost:4300/api/Search/SearchAllProviders', {params : parameters});
    }
  }
