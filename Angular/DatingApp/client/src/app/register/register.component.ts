import { Component, inject, OnInit, output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { JsonPipe } from '@angular/common';
import { TextInputComponent } from "../_forms/text-input/text-input.component";
import { DatePickerComponent } from "../_forms/date-picker/date-picker.component";
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, JsonPipe, TextInputComponent, DatePickerComponent],
  standalone: true,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {

  // @Input() usersFromHomeComponent: any; // Older way in 17 ver and old
  // @Output() cancelRegister = new EventEmitter();
  // usersFromHomeComponent = input.required<any>();
  private accountService = inject(AccountService);
  private toastr = inject(ToastrService);
  private fb = inject(FormBuilder)
  private router = inject(Router);
  cancelRegister = output<boolean>();
  model: any = {};
  registerForm: FormGroup = new FormGroup({});
  maxDate = new Date();
  validationError: string | undefined;
  ngOnInit(): void {
    this.initialiseForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  initialiseForm() {
    // Traditional Approcah
    // this.registerForm = new FormGroup({
    //   username: new FormControl('', [Validators.required, Validators.nullValidator]),
    //   password: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(8)]),

    // Reactive Form builder Approach
    this.registerForm = this.fb.group({
      gender: ['male'],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(8), this.matchValues('password')]]
    });

    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value == control.parent?.get(matchTo)?.value ? null : { isMatching: true }
    }
  }

  register() {

    // Patching the DOB in proper format
    const dob =this.getDateOnly(this.registerForm.get('dateOfBirth')?.value);
    this.registerForm.patchValue({dateOfBirth:dob});

    console.log(this.registerForm.value);
    this.accountService.register(this.registerForm.value).subscribe({
      next: _ => this.router.navigateByUrl('/members'),
      error: (err: any) => this.validationError = err,
    })
  }

  cancel() {
    // console.log('cancelled');
    this.cancelRegister.emit(false);
  }

  getDateOnly(dob: string | undefined) {
if(!dob) return;
  return new Date(dob).toISOString().slice(0,10);
  }
}
