import { Component, OnInit } from '@angular/core';
import { AuthService } from '../@core/_services/auth.service';
import { Router } from '@angular/router';
import { notifySuccess, notifyError, showLoading, hideLoading } from '../app.helper';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  user: any = {};
  username: string;
  constructor(private authService: AuthService,
              private router: Router,) { }

  ngOnInit() {
  }
  login() {
    showLoading();
    this.authService.login(this.user).subscribe(
      next => {
        setTimeout(() => {
          /** spinner ends after 5 seconds */
          hideLoading();
          this.router.navigate(["/page/"]);
          notifySuccess('Login Success!!');
        }, 1000);
      },error => {
        notifyError('Login failed!!');
      }
    );
  }
}
