import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Patient} from "../models/Patient";
import {environment} from "../../environments/environment";
import {Doctor} from '../models/Doctor';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  constructor(private httpClient: HttpClient) { }


  getAllDoctors() {
    return this.httpClient.get<Array<Doctor>>(environment.serverUrl+"api/Doctors");
  }

  getDoctorById(patientId: number) {
    return this.httpClient.get<Doctor>( `${environment.serverUrl}api/Doctors/${patientId}`);
  }

  saveDoctor(doctor: Doctor){
    return this.httpClient.post<Doctor>(environment.serverUrl+"api/Doctors", doctor);
  }

  updateDoctor(doctor: Doctor){
    return this.httpClient.put<Patient>(`${environment.serverUrl}api/Doctors/${doctor.id}`, doctor);
  }

  deletePatient(doctor: Doctor){
    return this.httpClient.delete(`${environment.serverUrl}api/Doctors/${doctor.id}`);
  }




}
