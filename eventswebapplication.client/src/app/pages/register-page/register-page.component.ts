import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth-service/auth.service';
import { DatePipe } from '@angular/common';
import { BirthdayDto, UserRegistration } from '../../models/user.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css'],
  providers: [DatePipe]
})
export class RegisterPageComponent {
  registerForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private datePipe: DatePipe,private router: Router) {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required]],
      surname: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      birthday: ['', [Validators.required]]
    });
  }

  async onSubmit() {
    if (this.registerForm.valid) {
      const { name, surname, email, password, birthday } = this.registerForm.value;

      const date = new Date(birthday);
      const birthdayDto: BirthdayDto = {
        year: date.getFullYear(),
        month: date.getMonth() + 1,
        day: date.getDate()
      };

      const userData: UserRegistration = {
        name,
        surname,
        email,
        password,
        birthday: birthdayDto,
        isAdmin: false 
      };

      try {
        const success = await this.authService.registerUser(userData)
          .then(success => {
            if (success) {
              alert("Успешная регистрация! Войдите в аккаунт, чтобы продолжить");
              this.router.navigate(['/auth']);
            }
          });
        
      } catch (error) {
        console.error('Error during registration:', error);
      }
    }
  }
}
