import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { MemberCardComponent } from '../member-card/member-card.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { UserParams } from '../../_models/userParam';
import { AccountService } from '../../_services/account.service';
import { FormsModule } from '@angular/forms';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
@Component({
  selector: 'app-member-list',
  imports: [MemberCardComponent, PaginationModule, FormsModule , ButtonsModule],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
  memberService = inject(MembersService);

  genderList = [{ value: 'male', display: 'Males' }, { value: 'female', display: 'Females' }];

  ngOnInit(): void {
    if (!this.memberService.paginationResult())
      this.loadMembers();
  }

  loadMembers() {
    this.memberService.getMembers();
  }

  resetFilter() {
    this.memberService.resetParams()
    this.loadMembers();
  }

  pageChanged(event: any) {
    if (this.memberService.userParamss().pageNumber != event.page) {
      this.memberService.userParamss().pageNumber = event.page;
      this.loadMembers();

    }
  }
}
