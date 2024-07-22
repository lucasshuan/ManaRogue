using Godot;

namespace ManaRogue.Game
{
  public partial class MainCamera : Camera2D
  {
    private static MainCamera _instance;
    public static MainCamera Instance => _instance;

    public override void _EnterTree()
    {
      if (_instance != null)
      {
        return;
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
  }
}