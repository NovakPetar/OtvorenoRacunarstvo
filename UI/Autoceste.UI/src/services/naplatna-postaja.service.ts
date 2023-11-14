import { Injectable } from '@angular/core';
import { environment } from './../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { NaplatnaPostaja } from 'src/models/naplatna-postaja.model';

@Injectable({
  providedIn: 'root'
})
export class NaplatnaPostajaService {

  constructor(private httpClient : HttpClient) { }
  getNaplatne() : Observable<NaplatnaPostaja[]>{
    return this.httpClient.get<NaplatnaPostaja[]>(environment.baseApiUrl + '/naplatne-postaje');
  }
  getNaplatnaById(id : number) : Observable<NaplatnaPostaja>{
    return this.httpClient.get<NaplatnaPostaja>(environment.baseApiUrl + '/naplatne-postaje/'+ id);
  }
  getNaplatneFiltered(property: string, term: string) : Observable<NaplatnaPostaja[]>{
    return this.httpClient.get<NaplatnaPostaja[]>(environment.baseApiUrl + '/search/naplatne-postaje?searchProperty='+ property +'&searchTerm=' + term);
  }

  getNaplatneByAutocestaId(autocestaId: number) : Observable<NaplatnaPostaja[]>{
    return this.httpClient.get<NaplatnaPostaja[]>(environment.baseApiUrl + '/autoceste/' + autocestaId + '/naplatne-postaje');
  }

}
