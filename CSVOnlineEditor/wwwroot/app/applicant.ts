export class Applicant {
    public selected: string;

    constructor(
        public id: number,
        public firstName: string,
        public middleName: string,
        public lastName: string,
        public birthDate: string,
        public email: string,
        public phone: string) { }
}