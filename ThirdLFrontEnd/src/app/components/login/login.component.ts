import { Component, OnInit } from '@angular/core';
import {LoginService} from '../../services/login.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login = '';
  password = '';
  userType = 'patient';

  selectedTypes = ['patient', 'doctor'];

  constructor(private loginService: LoginService,
              private  router: Router) { }

  ngOnInit() {
  }

  signUp() {
    if(this.userType === 'patient') {
      this.router.navigate(['patients','add']);
    }
    if(this.userType === 'doctor') {
      this.router.navigate( ['doctors', 'add'], );
    }
  }



  loginPatient() {
    console.log(this.userType);
    if( this.userType === 'patient') {
      this.loginService.loginPatient({login: this.login, password: this.password}).subscribe(dto => {
        console.log(dto);
        localStorage.setItem("token", dto['token']);
        localStorage.setItem("firstName", dto['firstName']);
        localStorage.setItem("lastName", dto['lastName']);
        this.router.navigate(['patients', dto['id']]);
      });
    }
    if( this.userType === 'doctor') {
      this.loginService.loginDoctor({login: this.login, password: this.password}).subscribe(dto => {
        localStorage.setItem("token", dto['token']);
        localStorage.setItem("firstName", dto['firstName']);
        localStorage.setItem("lastName", dto['lastName']);
        this.router.navigate(['doctors', dto['id'],'patients']);
      });
    }
  }
}
