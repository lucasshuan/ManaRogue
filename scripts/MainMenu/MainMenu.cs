using Godot;
using ManaRogue.Manager;

namespace ManaRogue
{
	public partial class MainMenu : CanvasLayer
	{
		private VBoxContainer _mainMenuContainer => GetNode<VBoxContainer>("MainMenuContainer");
		private VBoxContainer _onlineContainer => GetNode<VBoxContainer>("OnlineContainer");

		private Button _joinGameButton => GetNode<Button>("OnlineContainer/HBoxContainer/VBoxContainer/JoinGameButton");
		private LineEdit _joinGameIDEdit => GetNode<LineEdit>("OnlineContainer/HBoxContainer/VBoxContainer/JoinGameIDEdit");

		private string gameId = "";

		private void _OnPlaySoloButtonPressed()
		{
			SceneManager.Instance.GoToSoloGame();
		}

		private void _OnOnlineButtonPressed()
		{
			_mainMenuContainer.Hide();
			_onlineContainer.Show();
		}

		private void _OnSettingsButtonPressed()
		{
			GD.Print("Settings button pressed");
		}

		private void _OnQuitButtonPressed()
		{
			GetTree().Quit();
		}

		private void _OnCreateGameButtonPressed()
		{
			SteamManager.Instance.CreateLobby();
		}

		private void _OnJoinGameIDEditTextChanged(string new_text)
		{
			foreach (char c in new_text)
			{
				if (!char.IsNumber(c))
				{
					new_text = new_text.Replace(c.ToString(), "");
				}
			}
			_joinGameIDEdit.Text = "";
			_joinGameIDEdit.InsertTextAtCaret(new_text);
			_joinGameButton.Disabled = string.IsNullOrWhiteSpace(new_text);
			_joinGameButton.MouseDefaultCursorShape = _joinGameButton.Disabled ? Control.CursorShape.Arrow : Control.CursorShape.PointingHand;
			gameId = new_text;
		}

		private void _OnJoinGameButtonPressed()
		{
			SteamManager.Instance.JoinLobby(ulong.Parse(gameId));
		}

		private void _OnBackButtonPressed()
		{
			_onlineContainer.Hide();
			_mainMenuContainer.Show();
		}
	}
}
