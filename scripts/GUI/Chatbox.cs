// using Godot;
// using ManaRogue.Entity;
// using ManaRogue.Manager;
// using ManaRogue.Utils;

// namespace ManaRogue.GUI
// {
// 	public partial class Chatbox : CanvasLayer
// 	{
// 		[Export] private PackedScene _messageScene { get; set; }

// 		private Control _chatControl => GetNode<Control>("Chat");
// 		private VBoxContainer _messageHistory => GetNode<VBoxContainer>("Chat/ScrollContainer/MessageHistory");
// 		private TextEdit _messageEdit => GetNode<TextEdit>("Chat/MessageEdit");

// 		private bool _active = false;

// 		public override void _Ready()
// 		{
// 			Steam.LobbyChatUpdate += OnLobbyChatUpdate;
// 			Steam.LobbyMessage += OnLobbyMessage;

// 			SendServerUpdate(Steam.ChatMemberStateChange.Entered, SteamManager.Instance.SteamName);
// 		}

// 		public override void _Process(double delta)
// 		{
// 			if (Input.IsActionJustPressed(Keybind.Chat))
// 			{
// 				if (_active)
// 				{
// 					OpenChatbox();
// 				}
// 				else
// 				{
// 					SendChatMessage();
// 				}
// 			}
// 			if (Input.IsActionJustPressed(Keybind.Escape))
// 			{
// 				CloseChatbox();
// 			}
// 		}

// 		private void OpenChatbox()
// 		{
// 			if (Player.Instance.Disabled)
// 				return;

// 			_active = true;
// 			_chatControl.Modulate = new Color(1, 1, 1, 1);
// 			_messageEdit.Visible = true;
// 			_messageEdit.GrabFocus();
// 			Player.Instance.Disabled = true;
// 		}

// 		private void CloseChatbox()
// 		{
// 			if (!_active)
// 				return;

// 			_active = false;
// 			_chatControl.Modulate = new Color(1, 1, 1, 0.35f);
// 			_messageEdit.ReleaseFocus();
// 			Player.Instance.Disabled = false;
// 		}

// 		public void AddMessageToChatHistory(Color color, string message)
// 		{
// 			var label = _messageScene.Instantiate<RichTextLabel>();
// 			label.PushColor(color);
// 			label.AppendText(message);
// 			_messageHistory.AddChild(label);
// 		}

// 		private void SendChatMessage()
// 		{
// 			if (!string.IsNullOrWhiteSpace(_messageEdit.Text))
// 			{
// 				Steam.SendLobbyChatMsg(SteamManager.Instance.Lobby.Id, _messageEdit.Text);
// 			}
// 			_messageEdit.Text = "";
// 		}

// 		private void SendServerUpdate(Steam.ChatMemberStateChange state, string changerName)
// 		{
// 			var message = "<!> ";

// 			if (state == Steam.ChatMemberStateChange.Entered) message += $"{changerName} has joined";
// 			else if (state == Steam.ChatMemberStateChange.Left) message += $"{changerName} has left";
// 			else if (state == Steam.ChatMemberStateChange.Kicked) message += $"{changerName} has been kicked";
// 			else if (state == Steam.ChatMemberStateChange.Banned) message += $"{changerName} has been banned";
// 			else if (state == Steam.ChatMemberStateChange.Disconnected) message += $"{changerName} lost connection";
// 			else message += $"{changerName} did... something";

// 			AddMessageToChatHistory(Colors.Yellow, message);
// 		}

// 		public void OnLobbyChatUpdate(ulong lobbyId, long changedId, long makingChangeId, long chatState)
// 		{
// 			var changerName = Steam.GetFriendPersonaName((ulong)changedId);

// 			SendServerUpdate((Steam.ChatMemberStateChange)chatState, changerName);
// 		}

// 		public void OnLobbyMessage(ulong lobbyId, long userId, string message, long chatType)
// 		{
// 			var userName = Steam.GetFriendPersonaName((ulong)userId);
// 			AddMessageToChatHistory(Colors.White, $"<{userName}>: {message}");
// 		}
// 	}
// }