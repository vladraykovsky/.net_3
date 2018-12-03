import { Component, OnInit } from '@angular/core';
import {Gender} from "../../models/Gender";
import {Router} from "@angular/router";
import {DoctorService} from '../../services/doctor.service';

@Component({
  selector: 'app-doctor-add',
  templateUrl: './doctor-add.component.html',
  styleUrls: ['./doctor-add.component.css']
})
export class DoctorAddComponent implements OnInit {

  doctor = {
    id: 0,
    name: '',
    surname: '',
    login: '',
    password: '',
    dateOfBirth: '',
    country: '',
    region: '',
    gender: Gender.MALE,
    patients: []
  };

  constructor(private router: Router,
              private doctorService: DoctorService) {
  }

  ngOnInit() {
  }

  addDoctor() {
    this.doctorService.saveDoctor(this.doctor).subscribe(receivedPatient => {
      this.doctor = {
        id: 0,
        name: '',
        surname: '',
        login: '',
        password: '',
        dateOfBirth: '',
        country: '',
        region: '',
        gender: Gender.MALE,
        patients: []
      };
      this.router.navigate(['login']);
    });
  }

}
