using System.Collections.Generic;

namespace ManaRogue.Steam
{
  public class SteamLobby
  {
    public ulong Id { get; set; }
    public List<SteamLobbyMember> Members = new();
    public const string Name = "unnamed";
    public const int MaxMembers = 8;
  }
}