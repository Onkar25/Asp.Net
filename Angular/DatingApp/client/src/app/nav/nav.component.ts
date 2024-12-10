import { Component, inject, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
  imports: [FormsModule,BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
   accountService = inject(AccountService);
 
  model: any = {};

  login() {
    this.accountService.login(this.model).subscribe({
      next: (res: any) => {
        console.log(res);
      },
      error: (err: any) => console.log(err)
    });

  }

  
  logout(){
    this.accountService.logout();
  }

}
