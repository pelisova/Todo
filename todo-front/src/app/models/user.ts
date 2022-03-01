export class CreateUser {
  constructor(
    public username: string,
    public email: string,
    public password: string,
    public retypePassword: string
  ) {}
}

// export interface User {
//   username: string;
//   email: string;
//   password: string;
// }
