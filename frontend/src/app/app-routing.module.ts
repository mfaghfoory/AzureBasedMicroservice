import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomersComponent } from './customers/customers.component';
import { AlteringsComponent } from './alterings/alterings.component';


const routes: Routes = [
  {
    path: 'customers',
    component: CustomersComponent
  },
  {
    path: '',
    component: CustomersComponent
  },
  {
    path: 'alterations/:id',
    component: AlteringsComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
