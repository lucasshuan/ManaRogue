using Godot;

namespace ManaRogue.GUI
{
  public partial class MainMenuOnlineScreen : Control
  {
    private Button _joinGameButton => GetNode<Button>("VBoxContainer/HBoxContainer/VBoxContainer/JoinGameButton");
    private LineEdit _joinGameIDEdit => GetNode<LineEdit>("VBoxContainer/HBoxContainer/VBoxContainer/JoinGameIDEdit");

    private string _gameId = "";

    private void _OnCreateGameButtonPressed()
    {
      // SteamManager.Instance.CreateLobby();
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
      _gameId = new_text;
    }

    private void _OnJoinGameButtonPressed()
    {
      // SteamManager.Instance.JoinLobby(ulong.Parse(gameId));
    }

    private void _OnBackButtonPressed()
    {
      MainMenu.Instance.GoToScreen(MainMenu.Screen.Start);
    }
  }
}