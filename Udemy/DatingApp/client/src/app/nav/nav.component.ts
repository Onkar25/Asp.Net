import { Component, inject } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
@Component({
  selector: 'app-nav',
  standalone: true,
  // imports: [FormsModule, NgIf],
  imports: [FormsModule, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountService = inject(AccountService);
  model: any = {}

  login() {
    // console.log(this.model);
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);

      },
      error: error => console.log(error)
    });
  }

  logout() {
    this.accountService.logout();
  }
}
