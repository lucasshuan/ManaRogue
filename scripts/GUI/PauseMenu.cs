using Godot;
using ManaRogue.Entity;
using ManaRogue.Manager;
using ManaRogue.Utils;

namespace ManaRogue.GUI
{
	public partial class PauseMenu : CanvasLayer
	{
		private HBoxContainer _gameIDContainer => GetNode<HBoxContainer>("GameIDContainer");
		private Label _gameIDField => GetNode<Label>("GameIDContainer/VBoxContainer/GameIDField");

		public override void _Ready()
		{
			Visible = false;
			var isOnline = SteamManager.Instance.Lobby != null;
			_gameIDContainer.Visible = isOnline;
			if (isOnline)
			{
				_gameIDField.Text = SteamManager.Instance.Lobby.Id.ToString();
			}
		}

		public override void _Input(InputEvent @event)
		{
			if (@event.IsActionPressed(Keybind.Escape))
			{
				Visible = !Visible;
				Player.Instance.Disabled = Visible;
			}
		}

		private void _OnCopyButtonPressed()
		{
			DisplayServer.ClipboardSet(SteamManager.Instance.Lobby.Id.ToString());
		}

		private void _OnOptionsButtonPressed()
		{
			GD.Print("Options button pressed");
		}

		private void _OnLeaveGameButtonPressed()
		{
			if (SteamManager.Instance.Lobby != null)
			{
				SteamManager.Instance.LeaveLobby();
			}
			SceneManager.Instance.GoToMainMenu();
		}
	}
}
