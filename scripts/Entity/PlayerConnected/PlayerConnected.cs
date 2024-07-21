using System.Collections.Generic;
using Godot;

namespace ManaRogue.Entity
{
  public partial class PlayerConnected : CharacterBody2D
  {
    public static List<PlayerConnected> ListOfConnectedPlayers = new List<PlayerConnected>();

    private AnimationPlayer _animationPlayer => GetNode<AnimationPlayer>("AnimationPlayer");

    public override void _EnterTree()
    {
      ListOfConnectedPlayers.Add(this);
    }

    public override void _ExitTree()
    {
      ListOfConnectedPlayers.Remove(this);
    }

    public override void _PhysicsProcess(double delta)
    {
      MoveAndSlide();
    }
  }
}