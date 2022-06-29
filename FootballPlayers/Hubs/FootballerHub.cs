using Newtonsoft.Json;
using System.Threading.Tasks;
using FootballPlayers.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using FootballPlayers.Models.Transfers;

namespace FootballPlayers.Hubs
{
    public class FootballerHub : Hub
    {
        public async Task JoinGroup(string groupName) =>
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        public async Task LeaveGroup(string groupName) =>
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        public async Task AddNewFootballer(Footballer newFootballer)
        {
            await Clients.All.SendAsync("UpdateFootballerTable", newFootballer);
        }
    }
    
}
