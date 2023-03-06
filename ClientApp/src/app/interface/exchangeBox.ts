import { Permission } from "./permission";
import { ShowRoom } from "./show-room";

export interface ExchangeBoxRoleRoom
{
  itemsFromAssignedRooms: ShowRoom[],
  itemsFromCandidateRooms: ShowRoom[]
  itemsFromAssignedRoles: Permission[],
  itemsFromCandidateRoles: Permission[]
}
