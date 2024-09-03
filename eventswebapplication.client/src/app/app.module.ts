import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDividerModule } from '@angular/material/divider';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { AuthPageComponent } from './pages/auth-page/auth-page.component';
import { AboutCompanyPageComponent } from './pages/about-company-page/about-company-page.component';
import { EventCatalogPageComponent } from './pages/event-catalog-page/event-catalog-page.component';
import { EventPageComponent } from './pages/event-page/event-page.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    AuthPageComponent,
    AboutCompanyPageComponent,
    EventCatalogPageComponent,
    EventPageComponent
  ],
  imports: [
    MatButtonModule,
    MatPaginatorModule,
    MatCardModule,
    MatDividerModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
