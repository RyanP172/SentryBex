export interface CreateEmployee {
  firstName: string
  middleName: string
  lastName: string
  dob: string
  code: string
  isContractor: boolean
  contractorTypeId: string
  defaultShowroomFk: number
  maxLeadCount: number
  monthlyBudget: number
  companyId: number
  email: string
  samAccount: string
  password: string
  passwordSalt: string
  status: string
}
