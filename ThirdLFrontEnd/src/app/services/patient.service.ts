import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Patient} from '../models/Patient';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  constructor(private httpClient: HttpClient) { }


  getAllPatient() {
    return this.httpClient.get<Array<Patient>>(environment.serverUrl+"api/Patients");
  }

  getPatientById(patientId: number) {
    return this.httpClient.get<Patient>( `${environment.serverUrl}api/Patients/${patientId}`);
  }

  savePatient(patient: Patient){
    return this.httpClient.post<Patient>(environment.serverUrl+"api/Patients", patient);
  }

  updatePatient(patient: Patient){
    return this.httpClient.put<Patient>(`${environment.serverUrl}api/Patients/${patient.id}`, patient);
  }

  deletePatient(patient: Patient){
    return this.httpClient.delete(`${environment.serverUrl}api/Patients/${patient.id}`);
  }


}
