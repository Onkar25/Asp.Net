import { Component, inject, OnInit } from '@angular/core';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-messages',
  imports: [],
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
