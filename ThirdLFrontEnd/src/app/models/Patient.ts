import {Gender} from "./Gender";
import {Comment} from "./Comment";

export interface Patient {
    id: number,
    name: string,
    surname: string,
    login: string,
    password: string,
    dateOfBirth: string,
    country: string,
    region: string,
    gender: Gender,
    comments: Array<Comment>
}
