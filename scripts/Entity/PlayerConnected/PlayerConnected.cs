using Godot;

namespace ManaRogue.Entity
{
  public partial class PlayerConnected : CharacterBody2D
  {
    private AnimationPlayer _animationPlayer => GetNode<AnimationPlayer>("AnimationPlayer");

    public override void _PhysicsProcess(double delta)
    {
      MoveAndSlide();
    }
  }
}