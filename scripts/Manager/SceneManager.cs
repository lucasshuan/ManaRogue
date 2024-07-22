using Godot;

namespace ManaRogue.Manager
{
  public partial class SceneManager : Node
  {
    [Export] public PackedScene MainMenuScene { get; set; }
    [Export] public PackedScene SoloGameScene { get; set; }
    [Export] public PackedScene OnlineGameScene { get; set; }

    private bool _loading = false;
    public bool Loading
    {
      get { return _loading; }
      set
      {
        _loading = value;
        _loadingGUI.Visible = _loading;
      }
    }

    private Node _currentScene;

    private CanvasLayer _loadingGUI => GetNode<CanvasLayer>("LoadingOverlay");

    private static SceneManager _instance;
    public static SceneManager Instance => _instance;

    public override void _EnterTree()
    {
      if (_instance != null)
      {
        QueueFree();
      }
      _instance = this;
    }

    public override void _ExitTree()
    {
      if (_instance == this)
      {
        _instance = null;
      }
    }

    public override void _Ready()
    {
      GoToMainMenu();
    }

    public void GoTo(Node scene)
    {
      if (_currentScene != null)
      {
        _currentScene.QueueFree();
      }
      AddChild(scene);
      _currentScene = scene;
    }

    public void GoToMainMenu() => GoTo(MainMenuScene.Instantiate());

    public void GoToSoloGame() => GoTo(SoloGameScene.Instantiate());

    public void GoToOnlineGame() => GoTo(OnlineGameScene.Instantiate());
  }
}