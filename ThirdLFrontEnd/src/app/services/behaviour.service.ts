import {Injectable} from "@angular/core";
import {Subject} from "rxjs";
import {Patient} from "../models/Patient";

@Injectable({
  providedIn: 'root'
})
export class BehaviourService {

  subject = new Subject<any>();

  add(patient: Patient){
    this.subject.next( {action: 'add', patient: patient})
  }

  edit(patient: Patient){
    this.subject.next( {action: 'edit', patient: patient})
  }

}
