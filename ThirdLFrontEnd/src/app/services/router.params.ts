import {Subject} from "rxjs";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class RouterParams {
  subject = new Subject<any>();

  addParams(obj){
    this.subject.next({params: obj});
  }

}
