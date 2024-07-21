using Godot;
using ManaRogue.Game;

namespace ManaRogue.Entity
{
  public partial class PlayerNameLabel : Label
  {
    public override void _Process(double delta)
    {
      Scale = Vector2.One / MainCamera.Active.Zoom;
    }
  }
}