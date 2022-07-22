import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthserviceService } from '../authservice.service';
import { userdto } from '../models/userdto';
import { loginresponse } from '../models/loginresponse';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  userdto:userdto={
    userName: '',
    password: ''

    }
    Role=localStorage.getItem('Role')
    isAdmin = false;
    hide = true;


  constructor(
    private readonly authservice:AuthserviceService,
    private readonly route: ActivatedRoute,
    private snackbar: MatSnackBar,
    private router: Router) { }

  ngOnInit(): void {
  }
  login(){
    console.log(this.userdto)
    this.authservice.login(this.userdto)
      .subscribe(
        (successResponse:any)=> {
          localStorage.setItem('Token', successResponse.token);
          localStorage.setItem('Username', successResponse.username);
          localStorage.setItem('MailId', successResponse.mailId);
          localStorage.setItem('Role', successResponse.role);

          this.snackbar.open('Login Successfull', undefined, {
            duration: 2000
          });
          // setTimeout(() => {

          //   this.router.navigateByUrl('/searchall');
          //  // window.location.reload();
          //   //this.router.navigateByUrl('/top')

          // }, 2000);
          setTimeout(() => {

           // this.router.navigateByUrl('/searchall');
            window.location.reload();
            //this.router.navigateByUrl('/top')

          }, 2000);

        },
        (errorResponse) => {
          console.log(errorResponse)
                    this.snackbar.open('Login failed', undefined, {
            duration: 2000
          });
          setTimeout(() => {
            this.router.navigateByUrl('/resgister');
          }, 2000);
        }
      )}
    }
