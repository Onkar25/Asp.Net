import { NgIf } from '@angular/common';
import { Component, input, Self } from '@angular/core';
import { ControlValueAccessor, FormControl, NgControl, ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerConfig, BsDatepickerModule } from 'ngx-bootstrap/datepicker';
@Component({
  selector: 'app-date-picker',
  imports: [NgIf, ReactiveFormsModule,  BsDatepickerModule],
  templateUrl: './date-picker.component.html',
  styleUrl: './date-picker.component.css'
})
export class DatePickerComponent implements ControlValueAccessor {
  label = input<string>('');
  maxDate = input<Date>();
  bsConfig?: Partial<BsDatepickerConfig>;

  constructor(@Self() public ngConrol: NgControl) {
    this.ngConrol.valueAccessor = this;

    this.bsConfig = {
      containerClass: 'theme-green',
      dateInputFormat: 'DD MMMM YYYY'
    }
  }

  writeValue(obj: any): void {

  }
  registerOnChange(fn: any): void {

  }
  registerOnTouched(fn: any): void {

  }
  get control(): FormControl {
    return this.ngConrol.control as FormControl
  }
}