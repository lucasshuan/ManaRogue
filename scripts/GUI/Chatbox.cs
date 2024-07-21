using Godot;
using GodotSteam;
using ManaRogue.Entity;
using ManaRogue.Manager;
using ManaRogue.Utils;

namespace ManaRogue.GUI
{
	public partial class Chatbox : CanvasLayer
	{
		[Export] private PackedScene _messageScene { get; set; }

		private Control _chatControl => GetNode<Control>("Chat");
		private VBoxContainer _messageHistory => GetNode<VBoxContainer>("Chat/ScrollContainer/MessageHistory");
		private TextEdit _messageEdit => GetNode<TextEdit>("Chat/MessageEdit");

		private bool _active = false;

		public enum ChatMessageType
		{
			Chat,
			Warn,
		}

		public override void _Ready()
		{
			Steam.LobbyChatUpdate += OnLobbyChatUpdate;
			Steam.LobbyMessage += OnLobbyMessage;

			SendChatMessage(ChatMessageType.Warn, $"<!> {SteamManager.Instance.SteamName} joined\n");
		}

		public override void _Process(double delta)
		{
			if (Input.IsActionJustPressed(Keybind.Chat))
			{
				_active = !_active;
				_messageEdit.Visible = _active;
				Player.Instance.Disabled = _active;
				if (_active)
				{
					_chatControl.Modulate = new Color(1, 1, 1, 1);
					_messageEdit.GrabFocus();
				}
				else
				{
					_chatControl.Modulate = new Color(1, 1, 1, 0.35f);
					_messageEdit.ReleaseFocus();
					if (!string.IsNullOrWhiteSpace(_messageEdit.Text))
					{
						Steam.SendLobbyChatMsg(SteamManager.Instance.Lobby.Id, _messageEdit.Text);
					}
					_messageEdit.Text = "";
				}
			}
		}

		public void OnLobbyChatUpdate(ulong lobbyId, long changedId, long makingChangeId, long chatState)
		{
			var changerName = Steam.GetFriendPersonaName((ulong)changedId);

			var state = (Steam.ChatMemberStateChange)chatState;
			var message = "<!>";

			if (state == Steam.ChatMemberStateChange.Entered) message += $"{changerName} joined";
			else if (state == Steam.ChatMemberStateChange.Left) message += $"{changerName} left";
			else if (state == Steam.ChatMemberStateChange.Kicked) message += $"{changerName} has been kicked";
			else if (state == Steam.ChatMemberStateChange.Banned) message += $"{changerName} has been banned";
			else if (state == Steam.ChatMemberStateChange.Disconnected) message += $"{changerName} lost connection";
			else message += $"{changerName} did... something";

			SendChatMessage(ChatMessageType.Warn, message);
		}

		public void OnLobbyMessage(ulong lobbyId, long userId, string message, long chatType)
		{
			var userName = Steam.GetFriendPersonaName((ulong)userId);
			SendChatMessage(ChatMessageType.Chat, $"<{userName}>: {message}");
		}

		public void SendChatMessage(ChatMessageType type, string message)
		{
			Color color = type switch
			{
				ChatMessageType.Chat => Colors.White,
				ChatMessageType.Warn => Colors.Yellow,
				_ => Colors.White
			};
			var label = _messageScene.Instantiate<RichTextLabel>();
			label.PushColor(color);
			label.AppendText(message);
			_messageHistory.AddChild(label);
		}
	}
}