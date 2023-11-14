import { Autocesta } from './../../../models/autocesta.model';
import { DatatableRow } from './../../../models/datatable-row.model';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { AutocestaService } from 'src/services/autocesta.service';
import { NaplatnaPostajaService } from 'src/services/naplatna-postaja.service';
import { NaplatnaPostaja } from 'src/models/naplatna-postaja.model';
import { OpenDataService } from 'src/services/open-data.service';

@Component({
  selector: 'app-datatable',
  templateUrl: './datatable.component.html',
  styleUrls: ['./datatable.component.css']
})
export class DatatableComponent implements OnInit {

  constructor(private autocestaService: AutocestaService,
     private naplatnaService: NaplatnaPostajaService,
     private openDataService: OpenDataService) { }

  // Define variables for dropdown menu and search term
  
  npFilterBy: string[] = ['Naziv', 'GeoSirina', 'GeoDuzina', 'ImaEnc', 'Any'];
  autocesteFilterBy: string[] = ['Oznaka', 'Duljina', 'Dionica', 'NeformalniNaziv'];
  dropdownOptions: string[] = this.npFilterBy.concat(this.autocesteFilterBy);
  selectedOption: string = 'Naziv';
  searchTerm: string = '';

  // MatTableDataSource to hold the table data
  dataSource = new MatTableDataSource<DatatableRow>();

  // Table columns definition (replace with your actual data model)
  displayedColumns: string[] = ['naziv', 'geoSirina', 'geoDuzina', 'imaEnc', 'oznaka', 'duljina', 'dionica', 'neformalniNaziv'];

  // Fetch initial data on component initialization
  ngOnInit() {
    this.getData();
  }


  filterData() {
    if (this.npFilterBy.includes(this.selectedOption)){
      this.filterNaplatne()
    } else {
      this.filterAutoceste()
    }
  }
  
  getData() {
    const list: DatatableRow[] = [];
    this.naplatnaService.getNaplatne().subscribe(
      (successResponse) => {        
        //console.log(successResponse)
        for(let np of successResponse){
          let a: Autocesta = {
            duljina: 0,
            oznaka: 'ax',
            dionica: '',
            neformalniNaziv: '',
            id: 0
          }
          
          this.autocestaService.getAutocestaById(np.autocestaId).subscribe(
            (successResponse) => {
              //console.log(successResponse)
              a.duljina = successResponse.duljina;
              a.oznaka = successResponse.oznaka
              a.dionica = successResponse.dionica
              a.neformalniNaziv = successResponse.neformalniNaziv
              a.id = successResponse.id

              let objToAdd: DatatableRow = {
                naziv: np.naziv,
                geoDuzina: np.geoDuzina,
                geoSirina: np.geoSirina,
                imaEnc: np.imaEnc,
                kontakt: np.kontakt,
                oznaka: a.oznaka,
                dionica: a.dionica,
                duljina : a.duljina,
                neformalniNaziv: a.neformalniNaziv
              }
              list.push(objToAdd); 

              //update dataSource
              this.dataSource = new MatTableDataSource<DatatableRow>(list)
            },
            (errorResponse) => {
              console.log(errorResponse)
            }
          )
           
        }
        this.dataSource = new MatTableDataSource<DatatableRow>(list)
      },
      (errorResponse) => {
        console.log(errorResponse)
      }
    )
  }

  filterNaplatne(){
    const list: DatatableRow[] = [];
    this.naplatnaService.getNaplatneFiltered(this.selectedOption, this.searchTerm).subscribe(
      (successResponse) => {        
        //console.log(successResponse)
        for(let np of successResponse){
          let a: Autocesta = {
            duljina: 0,
            oznaka: 'ax',
            dionica: '',
            neformalniNaziv: '',
            id: 0
          }
          
          this.autocestaService.getAutocestaById(np.autocestaId).subscribe(
            (successResponse) => {
              //console.log(successResponse)
              a.duljina = successResponse.duljina;
              a.oznaka = successResponse.oznaka
              a.dionica = successResponse.dionica
              a.neformalniNaziv = successResponse.neformalniNaziv
              a.id = successResponse.id

              let objToAdd: DatatableRow = {
                naziv: np.naziv,
                geoDuzina: np.geoDuzina,
                geoSirina: np.geoSirina,
                imaEnc: np.imaEnc,
                kontakt: np.kontakt,
                oznaka: a.oznaka,
                dionica: a.dionica,
                duljina : a.duljina,
                neformalniNaziv: a.neformalniNaziv
              }
              list.push(objToAdd); 

              //update dataSource
              this.dataSource = new MatTableDataSource<DatatableRow>(list)
            },
            (errorResponse) => {
              console.log(errorResponse)
            }
          )
           
        }
        this.dataSource = new MatTableDataSource<DatatableRow>(list)
      },
      (errorResponse) => {
        console.log(errorResponse)
      }
    )
  }

  filterAutoceste(){
    const list: DatatableRow[] = [];
    this.autocestaService.getAutocesteFiltered(this.selectedOption, this.searchTerm).subscribe(
      (successResponse) => {        
        //console.log(successResponse)
        for(let a of successResponse){
                    
          this.naplatnaService.getNaplatneByAutocestaId(a.id).subscribe(
            (successResponse) => {

              for(let np of successResponse)
              {
                let objToAdd: DatatableRow = {
                  naziv: np.naziv,
                  geoDuzina: np.geoDuzina,
                  geoSirina: np.geoSirina,
                  imaEnc: np.imaEnc,
                  kontakt: np.kontakt,
                  oznaka: a.oznaka,
                  dionica: a.dionica,
                  duljina : a.duljina,
                  neformalniNaziv: a.neformalniNaziv
                }
                list.push(objToAdd); 
              }

              this.dataSource = new MatTableDataSource<DatatableRow>(list)
            },
            (errorResponse) => {
              console.log(errorResponse)
            }
          )
           
        }
        this.dataSource = new MatTableDataSource<DatatableRow>(list)
      },
      (errorResponse) => {
        console.log(errorResponse)
      }
    )
  }

  generateFilteredJson(){
    var jsonDataString = "";
    this.openDataService.getJsonFiltered(this.selectedOption, this.searchTerm).subscribe(
      (successResponse) => {
        
        jsonDataString = JSON.stringify(successResponse);
        const dataUri = 'data:application/json;charset=utf-8,' + encodeURIComponent(jsonDataString);

        // Create a temporary anchor element
        const link = document.createElement('a');
        link.href = dataUri;
        //link.download = 'data.json';

        // Trigger the click event on the anchor element
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);

      },
      (errorResponse) => {
        console.log(errorResponse);
      }
    )
    return jsonDataString;
  }
  generateFilteredCsv(){
    var csvDataString = "";
    this.openDataService.getCsvFiltered(this.selectedOption, this.searchTerm).subscribe(
      (successResponse) => {
        
        csvDataString = successResponse;
        const dataUri = 'data:text/csv;charset=utf-8,' + encodeURIComponent(csvDataString);

        // Create a temporary anchor element
        const link = document.createElement('a');
        link.href = dataUri;
        link.download = 'autoceste-filtered.csv';

        // Trigger the click event on the anchor element
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);

      },
      (errorResponse) => {
        console.log(errorResponse);
      }
    )
    return csvDataString;
  }
}
