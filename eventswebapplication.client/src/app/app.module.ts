import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { CatalogPageComponent } from './pages/catalog-page/catalog-page.component';
import { AuthPageComponent } from './pages/auth-page/auth-page.component';
import { AboutCompanyPageComponent } from './pages/about-company-page/about-company-page.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    CatalogPageComponent,
    AuthPageComponent,
    AboutCompanyPageComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
