import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {
  showSignInButton = true;
  showCabinetButton = false;
  constructor(private router: Router) { }

  ngOnInit() {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.checkRoute();
      }
    });
    this.checkRoute();
  }

  checkRoute(): void {
    this.showSignInButton = this.router.url != '/auth';
  }
}
