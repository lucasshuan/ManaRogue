using Godot;
using ManaRogue.Scenes;

namespace ManaRogue.GUI
{
  public partial class MainMenuStartScreen : Control
  {
    private void _OnPlaySoloButtonPressed()
    {
      SceneManager.Instance.GoToSoloGame();
    }

    private void _OnOnlineButtonPressed()
    {
      MainMenu.Instance.GoToScreen(MainMenu.Screen.Online);
    }

    private void _OnSettingsButtonPressed()
    {
      GD.Print("Settings button pressed");
    }

    private void _OnQuitButtonPressed()
    {
      GetTree().Quit();
    }
  }
}