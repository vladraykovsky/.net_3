import { Component, OnInit } from '@angular/core';
import {PatientService} from "../../services/patient.service";
import {BehaviourService} from "../../services/behaviour.service";
import {ActivatedRoute, Route, Router} from "@angular/router";
import {Patient} from '../../models/Patient';

@Component({
  selector: 'app-patient-edit',
  templateUrl: './patient-edit.component.html',
  styleUrls: ['./patient-edit.component.css']
})
export class PatientEditComponent implements OnInit {

  patient: Patient;

  constructor(private patientService: PatientService,
              private behaviourService: BehaviourService,
              private router: Router,
              private activatedRouter: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRouter.params.subscribe( params => {
      this.patientService.getPatientById(params['id']).subscribe( patient => {
          this.patient = patient;
      });
    });
  }

  edit() {
    this.patientService.updatePatient(this.patient).subscribe( editedPatient => {
      this.behaviourService.edit(editedPatient);
       this.router.navigate(['patients', editedPatient.id]);
    });
  }


}
