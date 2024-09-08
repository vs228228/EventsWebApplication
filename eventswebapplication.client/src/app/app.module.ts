import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { AuthPageComponent } from './pages/auth-page/auth-page.component';
import { AboutCompanyPageComponent } from './pages/about-company-page/about-company-page.component';
import { EventCatalogPageComponent } from './pages/event-catalog-page/event-catalog-page.component';
import { EventPageComponent } from './pages/event-page/event-page.component';
import { RegisterPageComponent } from './pages/register-page/register-page.component';
import { AccountPageComponent } from './pages/account-page/account-page.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { NotificationsComponent } from './components/notifications/notifications.component';
import { UserEventsComponent } from './components/user-events/user-events.component';
import { AdminPanelPageComponent } from './pages/admin-panel-page/admin-panel-page.component';
import { EditEventComponent } from './components/edit-event/edit-event.component';
import { CreateEventComponent } from './components/create-event/create-event.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    AuthPageComponent,
    AboutCompanyPageComponent,
    EventCatalogPageComponent,
    EventPageComponent,
    RegisterPageComponent,
    AccountPageComponent,
    UserProfileComponent,
    NotificationsComponent,
    UserEventsComponent,
    AdminPanelPageComponent,
    EditEventComponent,
    CreateEventComponent,
  ],
  imports: [
    MatButtonModule,
    MatPaginatorModule,
    MatCardModule,
    MatDividerModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatListModule,
    MatIconModule,
    MatIconButton,
    MatGridListModule,
    MatTableModule,
    MatDialogModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
