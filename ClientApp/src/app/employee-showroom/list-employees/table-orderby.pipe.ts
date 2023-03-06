import { Pipe, PipeTransform } from '@angular/core';
import { Employee } from '../../interface/employee';
@Pipe({ name: 'orderBy' })

export class OrderByTablePipe implements PipeTransform
{
  transform(employees: Employee[], field: string, direction: string): any[] {
    if (!employees || !employees.length || !field) {
      return employees;
    }
    const sorted = employees.sort((a: any, b: any) => {
      const aEmployee = this.getPropertyValue(a, field)
      const bEmployee = this.getPropertyValue(b, field)


      if (direction === 'asc') {
        if (aEmployee < bEmployee) {
          return -1;
        } else if (aEmployee > bEmployee) {
          return 1;
        } else {
          return 0;
        }
      } else {
        if (aEmployee < bEmployee) {
          return 1;
        } else if (aEmployee > bEmployee) {
          return -1;
        } else {
          return 0;
        }
      }
    });
    return sorted;
  }

  private getPropertyValue(item: any, field: string): any {
  
    if (field.indexOf('.') === -1) {
      return item[field];
      
    }

    const fields = field.split('.');
    let value = item;

    for (let i = 0; i < fields.length; i++) {
      value = value[fields[i]];

      if (!value) {
        return null;
      }
    }

    return value;
  }
}
