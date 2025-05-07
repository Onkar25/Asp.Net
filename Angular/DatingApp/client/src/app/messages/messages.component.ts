import { Component, inject, OnInit } from '@angular/core';
import { MessageService } from '../_services/message.service';
import { FormsModule } from '@angular/forms';
import { ButtonsModule } from 'ngx-bootstrap/buttons';

@Component({
  selector: 'app-messages',
  imports: [FormsModule,ButtonsModule],
  templateUrl: './messages.component.html',
  styleUrl: './messages.component.css'
})
export class MessagesComponent implements OnInit {

  messageService = inject(MessageService);
  container = "Inbox";
  pageNumber = 1;
  pageSize = 5;

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages() {
    this.messageService.getMessages(this.pageNumber, this.pageSize, this.container);
  }

  pageChanged($even: any) {
    if (this.pageNumber == $even.page) {
      this.pageNumber = $even.page;
      this.loadMessages();
    }
  }
}
