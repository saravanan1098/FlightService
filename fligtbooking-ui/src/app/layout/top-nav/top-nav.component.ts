import { Component, OnInit } from '@angular/core';
import { FlighService } from 'src/app/FlightService/fligh.service';

@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.css']
})
export class TopNavComponent implements OnInit {
  Token = localStorage.getItem('Token')
  Role=localStorage.getItem('Role')

  isAdmin = false;
  isUserLoggedIn = false;

  constructor() {


   }

  ngOnInit(): void {

    if(this.Role=='admin')
    {

      this.isAdmin = true;
    }else{

      this.isAdmin = false;
    }

     if(this.Token!=null)
    {

       this.isUserLoggedIn = true;
    }else{

        this.isUserLoggedIn = false;
    }

  }



  logout(){
    localStorage.removeItem('Token');
    localStorage.removeItem('Username');
    localStorage.removeItem('MailId');
    localStorage.removeItem('Role');
    this.isUserLoggedIn=false;
    this.isAdmin=false;

  }


}
