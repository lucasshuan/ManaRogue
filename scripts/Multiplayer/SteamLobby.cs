using System.Collections.Generic;

namespace ManaRogue.Multiplayer
{
  public class SteamLobby
  {
    public ulong Id { get; set; }
    public List<SteamLobbyMember> Members = [];
    public const string Name = "unnamed";
    public const int MaxMembers = 8;
  }
}