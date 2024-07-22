using Godot;
using Steamworks;
using System;

namespace ManaRogue.Steam
{
  public partial class SteamManager : Node
  {
    public static SteamManager Instance { get; private set; }

    private const uint gameAppId = 480;

    public string UserName;
    public SteamId UserId;
    private bool Connected = false;

    public override void _EnterTree()
    {
      if (Instance == null)
      {
        Instance = this;
        try
        {
          SteamClient.Init(gameAppId, true);
          if (!SteamClient.IsValid)
          {
            throw new Exception("Steam client is not valid");
          }
          SteamNetworking.AllowP2PPacketRelay(true);

          UserName = SteamClient.Name;
          UserId = SteamClient.SteamId;
          Connected = true;
        }
        catch (Exception e)
        {
          GD.PrintErr("Error initializing Steam: " + e.Message);
          Connected = false;
        }
      }
    }

    public override void _Notification(int what)
    {
      if (what == NotificationWMCloseRequest)
      {
        SteamClient.Shutdown();
        GetTree().Quit();
      }
    }

    public void ReadP2PPacket()
    {
      var packetFound = SteamNetworking.AllowP2PPacketRelay(true);
      if (packetFound)
      {
        var packet = SteamNetworking.ReadP2PPacket(0);
        var senderId = packet.Value.SteamId;
        var data = packet.Value.Data;
      }
    }
  }
}