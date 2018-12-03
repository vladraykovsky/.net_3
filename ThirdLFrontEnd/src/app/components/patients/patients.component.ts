import {Component, Input, OnInit} from '@angular/core';
import {PatientService} from '../../services/patient.service';
import {Patient} from '../../models/Patient';
import {BehaviourService} from '../../services/behaviour.service';
import {ActivatedRoute, Router} from "@angular/router";
import {Gender} from "../../models/Gender";

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent implements OnInit {

  currentPatient = {id:0, name: '',surname: '' ,dateOfBirth:'',country:'', region:'' , gender: Gender.MALE ,comments: []};

  @Input() patients = new Array<Patient>();

  constructor(private patientService: PatientService,
              private router: Router,
              private activatedRouter: ActivatedRoute,
              private behaviourService: BehaviourService) { }

  ngOnInit() {
 //   this.patientService.getAllPatient().subscribe( patients => {
 //     this.patients = patients;
 //   });

 //   this.behaviourService.subject.subscribe( action => {
 //     if( action.action === 'edit') {
 //       for(let i = 0 ; i < this.patients.length; i++) {
 //         if(this.patients[i].id === action.patient.id) {
 //           this.patients[i] = action.patient;
 //           this.router.navigate(['patients', action.patient.id]);
 //         }
 //       }
 //     }
 //     if( action.action === 'add') {
 //       this.patients.push(action.patient);
 //       this.router.navigate(['patients', action.patient.id]);
 //     }
 //   });

  }

 // deletePatient(patient: Patient) {
 //   event.stopPropagation();
 //   this.patientService.deletePatient(patient).subscribe( next => {
 //     this.patients = this.patients.filter( it => it.id !== patient.id);
 //   });
//  }

 // editPatient(patient: Patient, event) {
 //   event.stopPropagation();
 //   this.router.navigate( ['../patients', patient.id, 'edit']);
 // }


}
