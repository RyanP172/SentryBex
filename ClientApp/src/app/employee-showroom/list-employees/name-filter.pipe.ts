import { Pipe, PipeTransform } from "@angular/core";
import { Employee } from "../../interface/employee";

@Pipe({
  name: 'filterEmployee'
  })
export class FilterPipeEmployee implements PipeTransform
{
  transform(employees: Employee[], filterText: string)
  {
    if (employees.length === 0 || filterText === '') {
      return employees;
    }
    else
    {
      return employees.filter((employee) =>
      {
        return employee.firstName.toLowerCase().includes(filterText.toLowerCase())
      });
    }
  }
}
