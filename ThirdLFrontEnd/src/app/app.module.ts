import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule }   from '@angular/forms';
import { AppComponent } from './app.component';
import { PatientsComponent } from './components/patients/patients.component';
import { CommentsComponent } from './components/comments/comments.component';
import { PatientAddComponent } from './components/patient-add/patient-add.component';
import { PatientEditComponent } from './components/patient-edit/patient-edit.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { PatientDetailsComponent } from './components/patient-details/patient-details.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { DoctorComponent } from './components/doctor/doctor.component';
import {LoginInterceptor} from './interceptors/login-interceptor';
import { HeaderComponent } from './components/header/header.component';
import { DoctorAddComponent } from './components/doctor-add/doctor-add.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent},
  { path: 'patients/add', component: PatientAddComponent},
  { path: 'doctors/add', component: DoctorAddComponent},
  {path: '', component: HeaderComponent, children: [
      { path: "patients/:id", component: PatientDetailsComponent},
      { path: "patients/:id/edit", component: PatientEditComponent},
      { path: "doctors/:doctorId/patients", component: DoctorComponent, children: [
          { path: ":id", component: PatientDetailsComponent}
        ]}
      ]},
  { path: '**', redirectTo: '/login', pathMatch: 'full'}

];


@NgModule({
  declarations: [
    AppComponent,
    PatientsComponent,
    CommentsComponent,
    PatientAddComponent,
    PatientEditComponent,
    PatientDetailsComponent,
    LoginComponent,
    RegisterComponent,
    DoctorComponent,
    HeaderComponent,
    DoctorAddComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule ,
    BrowserAnimationsModule,
    FormsModule,
    RouterModule.forRoot(routes, {paramsInheritanceStrategy: 'always'})
  ],
  providers: [ {
    provide: HTTP_INTERCEPTORS,
    useClass: LoginInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
