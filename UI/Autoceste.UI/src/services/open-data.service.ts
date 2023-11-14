import { environment } from './../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OpenDataService {

  constructor(private httpClient : HttpClient) { }
  getJson() : Observable<string>{
    return this.httpClient.get<string>(environment.baseApiUrl + '/json');
  }

  getCsv() : Observable<any> {
    return this.httpClient.get(environment.baseApiUrl + '/csv', {responseType: 'text'});
  }
  getJsonFiltered(property: string, term: string) : Observable<string>{
    return this.httpClient.get(environment.baseApiUrl + '/filtered/json' + '?searchProperty='+ property +'&searchTerm=' + term, {responseType: 'text'});
  }

  getCsvFiltered(property: string, term: string) : Observable<any> {
    return this.httpClient.get(environment.baseApiUrl + '/filtered/csv' + '?searchProperty='+ property +'&searchTerm=' + term, {responseType: 'text'});
  }
}
