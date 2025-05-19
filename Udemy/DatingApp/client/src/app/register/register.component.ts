import { Component, inject, input, OnInit, output, } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { TextInputComponent } from "../_forms/text-input/text-input.component";
import { DatePickerComponent } from "../_forms/date-picker/date-picker.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, TextInputComponent, DatePickerComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {

  cancelRegister = output<boolean>();
  registerForms: FormGroup = new FormGroup({});
  maxDate = new Date();
  validationErrors: string[] | undefined;
  private accountService = inject(AccountService);
  private fb = inject(FormBuilder);
  private router = inject(Router);

  ngOnInit(): void {
    this.initailizeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  initailizeForm() {
    this.registerForms = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValue('password')]],
    });

    this.registerForms.controls['password'].valueChanges.subscribe({
      next: () => this.registerForms.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchValue(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : { isMatching: true }
    }
  }

  register() {
    const dob = this.getDateOnly(this.registerForms.get('dateOfBirth')?.value);
    this.registerForms.patchValue({ dateOfBirth: dob })
    console.log(this.registerForms.value);
    this.accountService.register(this.registerForms.value).subscribe({
      next: _ => {
        this.router.navigateByUrl('/members');
      },
      error: error => {
        this.validationErrors = error
      }
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  private getDateOnly(dob: string | undefined) {
    if (!dob)
      return;
    return new Date(dob).toISOString().slice(0, 10);
  }
}
