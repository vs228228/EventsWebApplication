import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthPageComponent } from './pages/auth-page/auth-page.component';
import { AboutCompanyPageComponent } from './pages/about-company-page/about-company-page.component';
import { EventCatalogPageComponent } from './pages/event-catalog-page/event-catalog-page.component';
import { EventPageComponent } from './pages/event-page/event-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { AccountPageComponent } from './pages/account-page/account-page.component';
import { AdminPanelPageComponent } from './pages/admin-panel-page/admin-panel-page.component';

const routes: Routes = [
  { path: '', component: EventCatalogPageComponent },
  { path: 'auth', component: AuthPageComponent },
  { path: 'about-company', component: AboutCompanyPageComponent },
  { path: 'event/:id', component: EventPageComponent },
  { path: `register`, component: RegisterPageComponent },
  { path: 'account', component: AccountPageComponent },
  { path: 'adminPanel', component: AdminPanelPageComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
