import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CatalogPageComponent } from './pages/catalog-page/catalog-page.component';
import { AuthPageComponent } from './pages/auth-page/auth-page.component';
import { AboutCompanyPageComponent } from './pages/about-company-page/about-company-page.component';

const routes: Routes = [
  { path: '', component: CatalogPageComponent },
  { path: 'auth', component: AuthPageComponent },
  { path: 'about-company', component: AboutCompanyPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
