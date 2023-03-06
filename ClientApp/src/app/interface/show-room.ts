export interface ShowRoom {
  selected: boolean;
  id: number;
  companyFk: number;
  name: string;
  shopCode: string;
  orderPrefix: number;
  defaultConsultantFk: number;
  monthlyBudget: number;
  state: string;
  loadDay: number;
}
