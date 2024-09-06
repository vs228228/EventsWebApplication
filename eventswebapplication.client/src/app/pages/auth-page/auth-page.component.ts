import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth-service/auth.service';


@Component({
  selector: 'app-auth-page',
  templateUrl: './auth-page.component.html',
  styleUrls: ['./auth-page.component.css']
})
export class AuthPageComponent {
  authForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.authForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }

  async tryEnterToSystem(email: String, password: String): Promise<boolean> {
    return await this.authService.tryEnterToSystem(email, password);
  }

  onSubmit() {
    if (this.authForm.valid) {
      const { email, password } = this.authForm.value;
      var ans = this.tryEnterToSystem(email, password)
        .then(success => {
          if (!success) {
            alert("Неверный логин или пароль");
          }
        });
    }
  }
}
