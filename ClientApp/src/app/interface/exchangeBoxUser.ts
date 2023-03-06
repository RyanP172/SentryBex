import { AspNetUser } from "./aspNetUser";

export interface ExchangeBoxUser
{
  itemsFromAssignedUsers: AspNetUser[],
  itemsFromCandidateUsers: AspNetUser[]
}
