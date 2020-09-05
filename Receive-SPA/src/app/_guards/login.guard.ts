
import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AuthService } from '../@core/_services/auth.service';

@Injectable({
    providedIn: "root"
})
export class LoginGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) { }
    canActivate(): boolean {
        if (!this.authService.loggedIn()) {
            return true;
        } else {
            this.router.navigate(['/page']);
            return false;
        }
    }
}
