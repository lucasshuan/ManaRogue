using Godot;

namespace ManaRogue.Game
{
  public partial class MainCamera : Camera2D
  {
    private static MainCamera _instance;
    public static MainCamera Instance => _instance;

    public override void _EnterTree()
    {
      _instance = this;
    }
  }
}