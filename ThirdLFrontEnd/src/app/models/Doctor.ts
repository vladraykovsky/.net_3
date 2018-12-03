import {Gender} from "./Gender";
import {Patient} from "./Patient";

export interface Doctor {
  id: number,
  name: string,
  surname: string,
  login: string,
  password: string,
  dateOfBirth: string,
  country: string,
  region: string,
  gender: Gender,
  patients: Array<Patient>
}
