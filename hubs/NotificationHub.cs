using Microsoft.AspNetCore.SignalR;

namespace sample_one.hubs{




public class NotificationHub :Hub
{
   public async Task SendPostNotification(string userId, string message)
    {
        await Clients.Groups(userId).SendAsync("ReceivePostNotification", message);
    }

    public async Task JoinGroup(string userId)
    {       
        Console.WriteLine("joined group " + userId);
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);
    }


}





}