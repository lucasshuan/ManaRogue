using Godot;

namespace ManaRogue.GUI
{

	public partial class MainMenu : CanvasLayer
	{
		public enum Screen
		{
			Start,
			Online,
		}

		private MainMenuStartScreen _startScreen => GetNode<MainMenuStartScreen>("StartScreen");
		private MainMenuOnlineScreen _onlineScreen => GetNode<MainMenuOnlineScreen>("OnlineScreen");

		private Screen _currentScreen = Screen.Start;

		public static MainMenu Instance { get; private set; }

		public override void _EnterTree()
		{
			Instance = this;
		}

		public void GoToScreen(Screen screen)
		{
			switch (screen)
			{
				case Screen.Start:
					_currentScreen = Screen.Start;
					_startScreen.Visible = true;
					_onlineScreen.Visible = false;
					break;
				case Screen.Online:
					_currentScreen = Screen.Online;
					_startScreen.Visible = false;
					_onlineScreen.Visible = true;
					break;
			}
		}
	}
}
