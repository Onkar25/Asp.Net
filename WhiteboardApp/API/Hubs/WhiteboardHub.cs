using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class WhiteboardHub : Hub
{
  private static readonly ConcurrentDictionary<string, List<string>> _groupIdShoppingLists = new();


  public async Task CreateShoppingList()
  {
    string shoppingListId = Guid.NewGuid().ToString();
    _groupIdShoppingLists.TryAdd(shoppingListId, []);
    await Groups.AddToGroupAsync(Context.ConnectionId, groupName: shoppingListId);
    await Clients.Caller.SendAsync("ShoppingListCreated", shoppingListId);
  }

  public async Task AddItem(string shoppingListId, string itemName)
  {
    if (_groupIdShoppingLists.TryGetValue(shoppingListId, out var list))
    {
      list.Add(itemName);
      await Clients.Group(shoppingListId).SendAsync(method: "ReciveShppingList", list);
    }
  }

  public async Task RemoveItem(string shoppingListId, string itemName)
  {
    if (_groupIdShoppingLists.TryGetValue(shoppingListId, out var list))
    {
      list.Remove(itemName);
      await Clients.Group(shoppingListId).SendAsync(method: "ReciveShppingList", list);
    }
  }

  public async Task JoinShoppingList(string shoppingListId)
  {
    if (_groupIdShoppingLists.TryGetValue(shoppingListId, out var list))
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, groupName: shoppingListId);
      await Clients.Caller.SendAsync(method: "JoinShoppingList", shoppingListId, list);
    }
  }
}
