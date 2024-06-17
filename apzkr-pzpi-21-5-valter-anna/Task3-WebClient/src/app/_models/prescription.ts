import { Medicine } from "./medicine";

export interface Prescription {
    id: number;
    medicine?: Medicine;
    dose: number;
    timesPerDay: number;
    prescriptionDateStart: Date;
    prescriptionDateEnd: Date;
}