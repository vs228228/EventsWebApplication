import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AuthService } from '../../services/auth-service/auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {
  showSignInButton = true;
  showCabinetButton = true;
  isAdmin = false;
  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.checkRoute();
      }
    });
    this.checkRoute();
    this.authService.isLoggedIn$.subscribe(isLoggedIn => {
      this.checkForLogin(isLoggedIn)
    });
    this.authService.checkLoginStatus();
  }

  checkRoute(): void {
    this.showSignInButton = this.router.url != '/auth' && localStorage.getItem('isLoggedIn') !== 'true';
  }

  checkForLogin(isLoggedIn: boolean) {
    if (isLoggedIn) {
      this.showCabinetButton = true;
    } else {
      this.showCabinetButton = false;
    }
    this.isAdmin = localStorage.getItem(`isAdmin`) == 'true';

  }
}
