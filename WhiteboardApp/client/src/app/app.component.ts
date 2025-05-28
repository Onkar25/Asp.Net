import { Component, inject, OnInit } from '@angular/core';
import { SignalrService } from './_services/signalr.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FormsModule, CommonModule],
  providers: [SignalrService],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  newShoppingListItem = '';
  shoppingListIdInput = '';
  shoppingList: string[] = [];
  shoppingListId: string | null = null;


  constructor(private signalr: SignalrService) {

  }

  ngOnInit() {
    this.connectSignalR();
  }

  private connectSignalR() {
    this.signalr.connect().then(() => {
      this.signalr.getHubConnection().on("ReciveShppingList",
        (items: string[]) => {
          this.shoppingList = items;
        });

      this.signalr.getHubConnection().on("ShoppingListCreated",
        (createdId: string) => {
          this.shoppingListId = createdId;
        });

      this.signalr.getHubConnection().on("JoinShoppingList",
        (shoppingListId: string, items: string[]) => {
          this.shoppingListId = shoppingListId;
          this.shoppingList = items;
        });
    })
  }

  addItemToShoppingList() {
    if (this.newShoppingListItem && this.shoppingListId) {
      this.signalr.addItem(this.shoppingListId, this.newShoppingListItem);
      this.newShoppingListItem = '';
    }
  }


  removeItemFromToShoppingList(item: string) {
    if (this.shoppingListId) {
      this.signalr.removeItem(this.shoppingListId, item);
      this.newShoppingListItem = '';
    }
  }

  createShoppingList() {
    this.signalr.createShoppingList();
  }

  joinShoppingList() {
    this.signalr.joinShoppingList(this.shoppingListIdInput);
  }
}


