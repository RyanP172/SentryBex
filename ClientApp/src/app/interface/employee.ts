import { Account } from "./account";
import { NetUser } from "./NetUser";
import { Permission } from "./permission";
import { ShowRoom } from "./show-room";

export interface Employee {
  id: number;
  firstName: string;
  middleName: any;
  lastName: string;
  dob: string;
  created: string;
  modified: string;
  accountFk: number;
  code: string;
  isContractor: boolean;
  contractorTypeFk: string;
  defaultShowroomFk: number;
  maxLeadCount: number;
  monthlyBudget: number;
  netUser: NetUser;
  account: Account;
  showRooms: ShowRoom[];
  permissions: Permission[];
}
