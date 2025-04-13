import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from 'src/app/core/services/auth.service'; 

@Component({
  selector: 'app-login-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login-register.component.html',
  styleUrls: ['./login-register.component.css']
})
export class LoginRegisterComponent {
  isLogin = true; 
  loginData = { username: '', password: '' };
  registerData = { username: '', password: '', confirmPassword: '' };

  constructor(private authService: AuthService) {}

  toggleTab(): void {
    this.isLogin = !this.isLogin;
  }

  login(): void {
    this.authService.login(this.loginData).subscribe({
      next: () => alert('Autentificat cu succes!'),
      error: (err) => alert('Eroare la login: ' + err.error)
    });
  }

  register(): void {
    if (this.registerData.password !== this.registerData.confirmPassword) {
      return alert('Parolele nu coincid!');
    }

    this.authService.register(this.registerData).subscribe({
      next: () => alert('Înregistrare reușită!'),
      error: (err) => alert('Eroare la înregistrare: ' + err.error)
    });
  }
}
