import { Pipe, PipeTransform } from "@angular/core";
import { AspNetUser } from "../../interface/aspNetUser";


@Pipe({
  name: 'filterEmail'
})

export class FilterPipeAspUser implements PipeTransform {
  transform(aspUsers: AspNetUser[], filterText: string) {
    if (aspUsers.length === 0 || filterText === '') {
      return aspUsers;
    }
    else {
      return aspUsers.filter((aspUsers) => {
        return aspUsers.email.toLowerCase().includes(filterText.toLowerCase())
      });
    }
  }
}

