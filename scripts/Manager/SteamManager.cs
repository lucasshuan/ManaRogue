using Godot;
using GodotSteam;
using ManaRogue.Multiplayer;

namespace ManaRogue.Manager
{
  public partial class SteamManager : Node
  {
    private static SteamManager _instance;
    public static SteamManager Instance => _instance;

    public SteamLobby Lobby { get; set; }

    public bool SteamInitialized = false;

    public ulong SteamId;
    public string SteamName;

    public override void _EnterTree()
    {
      if (_instance != null)
      {
        QueueFree();
      }
      _instance = this;
    }

    public override void _Ready()
    {
      OS.SetEnvironment("SteamAppId", 480.ToString());
      OS.SetEnvironment("SteamGameId", 480.ToString());

      InitializeSteam();

      Steam.LobbyCreated += OnLobbyCreated;
      Steam.LobbyJoined += OnLobbyJoined;
      Steam.JoinRequested += OnJoinRequested;
      Steam.LobbyChatUpdate += OnLobbyChatUpdate;
    }

    public override void _Process(double delta)
    {
      Steam.RunCallbacks();
    }

    private void InitializeSteam()
    {
      var initResponse = Steam.SteamInitEx(true);
      if (initResponse.Status != SteamInitExStatus.SteamworksActive)
      {
        GD.Print("Failed to initialize Steam: " + initResponse.Verbal);
        SteamInitialized = false;
        return;
      }

      SteamId = Steam.GetSteamID();
      SteamName = Steam.GetPersonaName();

      SteamInitialized = true;
    }

    public void CreateLobby()
    {
      SceneManager.Instance.Loading = true;
      Steam.CreateLobby(Steam.LobbyType.Public, SteamLobby.MaxMembers);
    }

    public void JoinLobby(ulong lobbyId)
    {
      SceneManager.Instance.Loading = true;
      Steam.JoinLobby(lobbyId);
    }

    public void LeaveLobby()
    {
      Steam.LeaveLobby(Lobby.Id);
      Lobby = null;
    }

    public void UpdateLobbyMembers()
    {
      Lobby.Members = [];
      var numMembers = Steam.GetNumLobbyMembers(Lobby.Id);
      for (int index = 0; index < numMembers; index++)
      {
        var memberSteamId = Steam.GetLobbyMemberByIndex(Lobby.Id, index);
        var memberName = Steam.GetFriendPersonaName(memberSteamId);
        Lobby.Members.Add(new SteamLobbyMember { SteamId = memberSteamId, SteamName = memberName });
      }
    }

    public void OnLobbyCreated(long connect, ulong lobbyId)
    {
      if (connect == 1)
      {
        Lobby = new SteamLobby { Id = lobbyId };
        Steam.AllowP2PPacketRelay(true);
      }
    }

    public void OnLobbyJoined(ulong lobbyId, long permissions, bool locked, long response)
    {
      if (response == (long)ChatRoomEnterResponse.Success)
      {
        Lobby = new SteamLobby { Id = lobbyId };
        GD.Print("Lobby Id: " + lobbyId);
        UpdateLobbyMembers();
        SceneManager.Instance.GoToOnlineGame();
      }
      SceneManager.Instance.Loading = false;
    }

    public void OnJoinRequested(ulong lobbyId, ulong steamId)
    {
      JoinLobby(lobbyId);
    }

    public void OnLobbyChatUpdate(ulong lobbyId, long changedId, long makingChangeId, long chatState)
    {
      UpdateLobbyMembers();
    }
  }
}