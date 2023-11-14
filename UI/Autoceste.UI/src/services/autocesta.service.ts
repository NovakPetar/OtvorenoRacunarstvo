import { Injectable } from '@angular/core';
import { environment } from './../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Autocesta } from 'src/models/autocesta.model';

@Injectable({
  providedIn: 'root'
})
export class AutocestaService {

  constructor(private httpClient : HttpClient) { }
  getAutoceste() : Observable<Autocesta[]>{
    return this.httpClient.get<Autocesta[]>(environment.baseApiUrl + '/autoceste');
  }
  getAutocestaById(id : number) : Observable<Autocesta>{
    return this.httpClient.get<Autocesta>(environment.baseApiUrl + '/autoceste/'+ id);
  }
  getAutocesteFiltered(property: string, term: string) : Observable<Autocesta[]>{
    return this.httpClient.get<Autocesta[]>(environment.baseApiUrl + '/search/autoceste?searchProperty='+ property +'&searchTerm=' + term);
  }
}
