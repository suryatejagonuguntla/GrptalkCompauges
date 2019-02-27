import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule, NgbDropdownModule} from '@ng-bootstrap/ng-bootstrap';


import { AppComponent } from './app.component';

import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { AllUsersComponent } from './all-users/all-users.component';
import { AllLeadsComponent } from './all-leads/all-leads.component';

import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';


import { UserService } from './shared/user.service';
import { EncryDecry } from './shared/EncryDecry.service';
import { DataService } from './shared/test.service';
import { Person } from './shared/test.service';
import { toasterService } from './shared/toaster.service';
import { appRoutes } from './routes';
import { AuthGuard } from './Authorization/auth.guard';
import { RoleGuard } from './Authorization/role.guard';
import { ToastrModule } from 'ng6-toastr-notifications';
//import { AutocompleteLibModule } from 'angular-ng-autocomplete';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NgSelectModule } from '@ng-select/ng-select';
import { ImageCropperModule } from 'ngx-image-cropper';




@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    HeaderComponent,
    AllUsersComponent,
    AllLeadsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    NgbModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    ToastrModule.forRoot(),
    NgSelectModule,
    NgbDropdownModule,
    ImageCropperModule
    
    //AutocompleteLibModule
    
  ],
  providers: [UserService, AuthGuard, toasterService, DataService, RoleGuard, EncryDecry],
  bootstrap: [AppComponent]
})
export class AppModule { }
