import { Component, OnInit } from '@angular/core';
import {PatientService} from '../../services/patient.service';
import { ActivatedRoute, Router } from '@angular/router';
import {Patient} from '../../models/Patient';
import {RouterParams} from '../../services/router.params';

@Component({
  selector: 'app-patient-details',
  templateUrl: './patient-details.component.html',
  styleUrls: ['./patient-details.component.css']
})
export class PatientDetailsComponent implements OnInit {

  patient: Patient;
  constructor(private patientService: PatientService,
              private routerParams: RouterParams,
              private router: Router,
              private activatedRouter: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRouter.params.subscribe( params => {
      this.routerParams.addParams(params);
      this.patientService.getPatientById(params['id']).subscribe( patient => {
        this.patient = patient;
      //  this.router.navigate(['patients', params['id']]);
      });
    })
  }

}
