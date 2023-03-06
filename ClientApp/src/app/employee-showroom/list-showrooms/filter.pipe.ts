import { Pipe, PipeTransform } from "@angular/core";
import { ShowRoom } from "../../interface/show-room";

@Pipe({
  name: 'filterShowroom'
  })
export class FilterPipeShowRoom implements PipeTransform
{
  transform(showRooms: ShowRoom[], filterText: string)
  {
    if (showRooms.length === 0 || filterText === '') {
      return showRooms;
    }
    else
    {
      return showRooms.filter((showRoom) =>
      {
        return showRoom.name.toLowerCase().includes(filterText.toLowerCase())
      });
    }
  }
}
