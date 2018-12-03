import { Component, OnInit } from '@angular/core';
import {Patient} from '../../models/Patient';
import {Gender} from '../../models/Gender';
import {PatientService} from '../../services/patient.service';
import {BehaviourService} from "../../services/behaviour.service";
import {Router} from '@angular/router';

@Component({
  selector: 'app-patient-add',
  templateUrl: './patient-add.component.html',
  styleUrls: ['./patient-add.component.css']
})
export class PatientAddComponent implements OnInit {

  patient = {id:0, name: '',surname: '', login: '', password: '' ,dateOfBirth:'',country:'', region:'' , gender: Gender.MALE ,comments: []};

  constructor(private patientService: PatientService,
              private router: Router,
              private behaviourService: BehaviourService) { }

  ngOnInit() {
  }

  addPatient(){
    this.patientService.savePatient(this.patient).subscribe( receivedPatient => {
      this.behaviourService.add(receivedPatient);
      this.patient = {id:0, name: '',surname: '' , login: '', password: '' ,dateOfBirth:'',country:'', region:'' , gender: Gender.MALE ,comments: []};
      this.router.navigate(['login']);
    });
  }

}
