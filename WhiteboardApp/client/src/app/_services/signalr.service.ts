import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  private hubConnection: HubConnection;

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:7058/whiteboardHub')
      .build();
  }

  getHubConnection() {
    return this.hubConnection;
  }

  async connect(): Promise<void> {
    try {
      await this.hubConnection.start();
      console.log("SignalR Connected");
    } catch (error) {
      console.log("SignalR connection error : ", error);
    }
  }

  async addItem(shopingListId: string, item: string): Promise<void> {
    try {
      await this.hubConnection.invoke("AddItem", shopingListId, item);
    } catch (error) {
      console.log("Error invoking AddItem : ", error);
    }
  }

  async removeItem(shopingListId: string, item: string): Promise<void> {
    try {
      await this.hubConnection.invoke("RemoveItem", shopingListId, item);
    } catch (error) {
      console.log("Error invoking RemoveItem : ", error);
    }
  }

  async joinShoppingList(shopingListId: string): Promise<void> {
    try {
      await this.hubConnection.invoke("JoinShoppingList", shopingListId);
    } catch (error) {
      console.log("Error invoking JoinShoppingList : ", error);
    }
  }

  async createShoppingList(): Promise<void> {
    try {
      await this.hubConnection.invoke("CreateShoppingList");
    } catch (error) {
      console.log("Error invoking CreateShoppingList : ", error);
    }
  }
}
