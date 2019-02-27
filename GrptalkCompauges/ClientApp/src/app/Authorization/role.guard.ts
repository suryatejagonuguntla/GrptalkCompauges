import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class RoleGuard implements CanActivate {
  constructor(private router: Router) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    
    const expectedRole = "1";
    const UserRole = localStorage.getItem('UserRole');
    
    if (expectedRole == UserRole) {
      return true;

    }
    else
    {
      this.router.navigate(['/login']);
      return false;
    }
      
    
  }
}
