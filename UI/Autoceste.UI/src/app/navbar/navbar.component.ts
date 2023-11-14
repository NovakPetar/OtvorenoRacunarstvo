import { Component, OnInit } from '@angular/core';
import { OpenDataService } from 'src/services/open-data.service';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private openDataService: OpenDataService) { }

  ngOnInit(): void {
  }

  onButtonClick(s: string){

  }

  generateJson(): string {
    var jsonDataString = "";
    this.openDataService.getJson().subscribe(
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

  generateCsv(): string {
    var csvDataString = "";
    this.openDataService.getCsv().subscribe(
      (successResponse) => {
        
        csvDataString = successResponse;
        const dataUri = 'data:text/csv;charset=utf-8,' + encodeURIComponent(csvDataString);

        // Create a temporary anchor element
        const link = document.createElement('a');
        link.href = dataUri;
        link.download = 'autoceste.csv';

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
