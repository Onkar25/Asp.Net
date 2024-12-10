import { Component, inject, input, output} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  imports: [FormsModule],
  standalone:true,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  // @Input() usersFromHomeComponent: any; // Older way in 17 ver and old
  // @Output() cancelRegister = new EventEmitter();
  private accountService = inject(AccountService);
  usersFromHomeComponent = input.required<any>();
  cancelRegister = output<boolean>();
  model: any = {};

  register() {
    // console.log(this.model);
    this.accountService.register(this.model).subscribe({
      next:(res:any)=>{
        console.log(res);
        this.cancel();
      },
      error :(res:any) => console.log(res)
    })
  }

  cancel() {
    // console.log('cancelled');
    this.cancelRegister.emit(false);
  }
}
