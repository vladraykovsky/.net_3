import { Injectable } from "@angular/core";
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse
} from "@angular/common/http";
import {Observable} from "rxjs";


@Injectable()
export class LoginInterceptor implements HttpInterceptor {

  constructor(){

  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.url.includes("api/")) {
      req = req.clone({
        headers: req.headers.set("Authorization", "Bearer " + localStorage.getItem("token"))
      });
    }
    return next.handle(req);
  }
}
