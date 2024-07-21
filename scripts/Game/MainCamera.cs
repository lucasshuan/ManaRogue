using Godot;

namespace ManaRogue.Game
{
  public partial class MainCamera : Camera2D
  {
    private static MainCamera _active;
    public static MainCamera Active => _active;

    public override void _EnterTree()
    {
      if (_active != null)
      {
        return;
      }
      _active = this;
    }
  }
}