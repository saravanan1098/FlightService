import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthserviceService } from '../authservice.service';
import { user } from '../models/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  user:user={
  userName: '',
  password: '',
  emailAddress: '',

  }
  hide=true;
  // @ViewChild('registerDetailsForm')registerDetailsForm ?: NgForm;

  constructor(
    private readonly authservice:AuthserviceService,
    private readonly route: ActivatedRoute,
    private snackbar: MatSnackBar,
    private router: Router) {

  }

  ngOnInit(): void {


  }
  // if(this.registerDetailsForm?.form.valid)
  // {

  // }
  addUser(){
    console.log(this.user)
    this.authservice.adduser(this.user)
      .subscribe(
        (successResponse)=> {
          this.snackbar.open('user register sucessfully', undefined, {
            duration: 2000
          });
          setTimeout(() => {
            this.router.navigateByUrl('/login');
          }, 2000);

        },
        (errorResponse) => {
          // Log
        }
      )}
    }







