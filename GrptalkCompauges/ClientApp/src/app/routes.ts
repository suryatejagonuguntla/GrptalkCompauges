import { Routes } from '@angular/router'
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';

import { AuthGuard } from './Authorization/auth.guard';
import { RoleGuard } from './Authorization/role.guard';
import { AllLeadsComponent } from './all-leads/all-leads.component';


export const appRoutes: Routes = [
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard, RoleGuard] },
  
  {
    path: 'login', component: LoginComponent  
  },
  {
    path: 'AllLeads', component: AllLeadsComponent,  canActivate: [AuthGuard],
    data: {
      expectedRole: 'admin'
    } 
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' }

];
