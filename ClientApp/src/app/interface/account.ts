export interface Account {
  id: number
  userName: string
  samAccountName: string
  password: any
  passwordSalt: any
  created: string
  resetPwdGuid: string
  resetPwdDatetime: any
  modified: string
  status: string
}
