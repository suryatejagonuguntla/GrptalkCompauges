import { Component } from '@angular/core';
import { UserService } from '../shared/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { EncryDecry } from '../shared/EncryDecry.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
  styleUrls: ['./login.component.css', '../../assets/predefinded/css/custom.css']
})
/** Login component*/
export class LoginComponent {
    /** Login ctor */
  isLoginError: boolean = false;
  UniqueId: string = '';
  Password: string = '';
  passwordType: string = "password";
  
  showPasswordStyle: any = { 'display': 'unset' };
  hidePasswordStyle: any = { 'display': 'none' };
  constructor(private userService: UserService, private router: Router, private encryDecry : EncryDecry ) {

  }

  LoginRequest() {

    var encrypted = this.encryDecry.set(this.UniqueId, this.Password);
    var decrypted = this.encryDecry.get(this.UniqueId, encrypted);
    var encrysha = this.encryDecry.sha3Encryption(this.Password);

    //alert(encrypted);
    //alert(decrypted);
    ////return;
    //alert(encrysha);
    this.Password = encrysha;
    console.log(encrysha);
    
    this.userService.userAuthentication(this.UniqueId, encrysha).subscribe((data: any) => {
      localStorage.setItem('userToken', data.access_token);
      localStorage.setItem('UserRole', data.role);
      this.router.navigate(['/AllLeads']);
    },
      (err: HttpErrorResponse) => {
        console.log(err);
        this.isLoginError = true;
        alert("Login Failed");
      });
  }

  togglePasswordType()
  {
    if (this.passwordType == "text") {
      this.passwordType = "password";
      this.showPasswordStyle = { 'display': 'unset' };
      this.hidePasswordStyle = { 'display': 'none' };
      
    }
    else
    {
      this.passwordType = "text";
      this.showPasswordStyle = { 'display': 'none' };
      this.hidePasswordStyle = { 'display': 'unset' }; 
    }
  }
}
