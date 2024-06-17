import { Medicine } from "./medicine";

export interface NewPrescription {
    userId: number;
    medicineId: number;
    dose: number;
    timesPerDay: number;
    prescriptionDateStart: Date;
    prescriptionDateEnd: Date;
}