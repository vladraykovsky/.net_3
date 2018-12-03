import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {DoctorService} from '../../services/doctor.service';

@Component({
  selector: 'app-doctor',
  templateUrl: './doctor.component.html',
  styleUrls: ['./doctor.component.css']
})
export class DoctorComponent implements OnInit {

  doctor;

  constructor(private activatedRouter: ActivatedRoute,
              private doctorService: DoctorService) { }

  ngOnInit() {
    this.activatedRouter.params.subscribe(params => {
      console.log(params);
      this.doctorService.getDoctorById(params['doctorId']).subscribe( doctor => {
        this.doctor = doctor;
      });
    });
  }

}
