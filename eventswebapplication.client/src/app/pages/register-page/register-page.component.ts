import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css']
})
export class RegisterPageComponent {
  registerForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required]],
      surname: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      birthday: ['', [Validators.required]]
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const { name, surname, email, password, birthday } = this.registerForm.value;
      console.log('Registration Data:', { name, surname, email, password, birthday });
      // Добавить логику
    }
  }
}
