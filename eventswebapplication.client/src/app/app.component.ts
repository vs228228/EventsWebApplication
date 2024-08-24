import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  showNavBar = true;
  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.checkRoute();
      }
    });
    this.checkRoute();
  }

  /*getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }*/

  checkRoute(): void {
    this.showNavBar = this.router.url != '/auth';
  }
}
