// using System.Collections.Generic;
// using Godot;
// using GodotSteam;
// using ManaRogue.Entity;
// using ManaRogue.Manager;
// using ManaRogue.Multiplayer;

// namespace ManaRogue.Game
// {
//   public partial class OnlineGame : Node2D
//   {
//     private List<PlayerConnected> _listOfConnectedPlayers = [];

//     public override void _Ready()
//     {
//       SpawnConnectedPlayers();
//     }

//     private void SpawnConnectedPlayers()
//     {
//       foreach (var lobbyMember in SteamManager.Instance.Lobby.Members)
//       {
//         if (lobbyMember.SteamId == SteamManager.Instance.SteamId)
//           continue;

//         var player = new PlayerConnected();
//         _listOfConnectedPlayers.Add(player);
//       }
//     }

//     public override void _Process(double delta)
//     {
//     }
//   }
// }