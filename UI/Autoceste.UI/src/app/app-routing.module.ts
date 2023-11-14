import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DescriptionComponent } from './description/description.component';
import { DatatableComponent } from './components/datatable/datatable.component';


const routes: Routes = [
  {
    path: '',
    component: DescriptionComponent
  },
  {
    path: 'datatable',
    component: DatatableComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
