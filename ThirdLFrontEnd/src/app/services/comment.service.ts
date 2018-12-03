import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Patient} from "../models/Patient";
import {Comment} from "../models/Comment";

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private httpClient: HttpClient) { }


  getCommentById(patientId: number, commentId: number) {
    return this.httpClient.get( `${environment.serverUrl}api/Patients/${patientId}/Comments/${commentId}`);
  }

  saveComment(patientId: number, comment: Comment){
    return this.httpClient.post(`${environment.serverUrl}api/Patients/${patientId}/Comments/`, comment);
  }


  deletePatient(patientId: number, commentId: number){
    return this.httpClient.delete(`${environment.serverUrl}api/Patients/${patientId}/Comments/${commentId}`);
  }

}
