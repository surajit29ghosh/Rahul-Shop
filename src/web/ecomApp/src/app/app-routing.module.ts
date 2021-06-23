import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


import { SearchComponent } from './Views/search/search.component';
import { ProductDetailsComponent } from './views/product-details/product-details.component';

const routes: Routes = [
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path: 'home', component: SearchComponent },
  {path: 'product/:id', component: ProductDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
