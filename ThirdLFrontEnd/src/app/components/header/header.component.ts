import { Component, OnInit } from '@angular/core';
import {Router, ActivatedRoute} from '@angular/router';
import {RouterParams} from "../../services/router.params";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  value = [];
  constructor(private route: Router,
              private routerParams: RouterParams,
              private activatedRouter: ActivatedRoute) { }

  ngOnInit() {
    this.routerParams.subject.subscribe(value => {
    this.value = value;
    console.log(this.value);
  });
  }

  logout() {
    localStorage.clear();
    this.route.navigate(['login'], );
  }

  editUser() {
    this.activatedRouter.params.subscribe( params => {
      console.log(params);
      if(this.value['params']['doctorId'] !== undefined) {
        this.route.navigate(['doctors',this.value['params']['doctorId'] , 'edit']);
      }
      if(this.value['params']['id'] !== undefined) {
        this.route.navigate(['patients', this.value['params']['id'], 'edit']);
      }


    });

  }

}
