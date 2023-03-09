export interface CreateEmployee {
  firstName: string
  middleName: string
  lastName: string
  dob: string | null
  code: string
  isContractor: boolean
  //contractorTypeId: string
  defaultShowroomFk: number
  maxLeadCount: number
  monthlyBudget: number
  companyId: number
  email: string
  samAccountName: string
  //password: string
  //passwordSalt: string
  status: string
  
}
