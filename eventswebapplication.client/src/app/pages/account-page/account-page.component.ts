import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account-page',
  templateUrl: './account-page.component.html',
  styleUrl: './account-page.component.css'
})
export class AccountPageComponent {
  constructor(private authService: AuthService, private router: Router) { }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['']);
  }

}
