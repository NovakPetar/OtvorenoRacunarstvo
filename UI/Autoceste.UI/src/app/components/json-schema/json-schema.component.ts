import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-json-schema',
  templateUrl: './json-schema.component.html',
  styleUrls: ['./json-schema.component.css']
})
export class JsonSchemaComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    let jsonDataString = `
      {
        "$schema":"http://json-schema.org/draft-07/schema#",
        "$id":"http://localhost:4200/json-schema",
        "type":"array",
        "items":{
          "$id":"#autocesta",
          "type":"object",
          "properties":{
              "Oznaka":{
                "type":"string",
                "description":"Oznaka autoceste (npr. A1)",
                "title":"Oznaka"
              },
              "NeformalniNaziv":{
                "type":["string", "null"],
                "description":"Neformalni naziv autoceste (npr. Istarski ipsilon)",
                "title":"Neformalni Naziv"
              },
          "Dionica":{
                "type":"string",
                "description":"Dionica autoceste",
                "title":"Dionica"
              },
              "Duljina":{
                "type":"number",
                "description":"Duljina autoceste u kilometrima",
                "title":"Duljina"
              },
              "NaplatnePostaje":{
                "type":"array",
                "items":{
                    "$id":"#naplatnaPostaja",
                    "type":"object",
                    "properties":{
                      "Naziv":{
                          "type":"string",
                          "description":"Naziv naplatne postaje, obično nazvana po najbližem gradu ili općini",
                          "title":"Naziv"
                      },
                      "GeoDuzina":{
                          "type":"number",
                          "description":"Geografska dužina naplatne postaje iskazana kao decimalni broj, pozitivni brojevi označavaju istok, a negativni zapad",
                          "title":"Geografska Dužina"
                      },
                      "GeoSirina":{
                          "type":"number",
                          "description":"Geografska širina naplatne postaje iskazana kao decimalni broj, pozitivni brojevi označavaju sjever, a negativni jug",
                          "title":"Geografska Širina"
                      },
                      "ImaEnc":{
                          "type":"boolean",
                          "description":"TRUE ako je na naplatnoj postaji moguće plaćanje cestarine ENC-uređajem, FALSE inače",
                          "title":"Ima ENC"
                      },
                      "Kontakt":{
                          "type":["string", "null"],
                          "description":"E-mail adresa za upite vezane uz naplatnu postaju",
                          "title":"Kontakt"
                      }
                    },
                    "required":[
                      "Naziv",
                      "GeoDuzina",
                      "GeoSirina",
                      "ImaEnc"
                    ]
                }
              }
          },
          "required":[
              "Oznaka",
              "Duljina",
          "Dionica",
              "NaplatnePostaje"
          ],
          "title":"Autoceste"
        }
    }
    `;
    const dataUri = 'data:application/json;charset=utf-8,' + encodeURIComponent(jsonDataString);

    // Create a temporary anchor element
    const link = document.createElement('a');
    link.href = dataUri;
    //link.download = 'data.json';

    // Trigger the click event on the anchor element
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }


}
