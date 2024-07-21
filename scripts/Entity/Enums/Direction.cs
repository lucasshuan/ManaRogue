using Godot;

namespace ManaRogue.Entity
{
  public static class Direction
  {
    public const string Left = "left";
    public const string Right = "right";
    public const string Up = "up";
    public const string Down = "down";
    public const string UpLeft = "upleft";
    public const string UpRight = "upright";
    public const string DownLeft = "downleft";
    public const string DownRight = "downright";

    public static string FromVector2(Vector2 vector)
    {
      if (vector.X < 0 && vector.Y > 0) return DownLeft;
      if (vector.X > 0 && vector.Y > 0) return DownRight;
      if (vector.X < 0 && vector.Y < 0) return UpLeft;
      if (vector.X > 0 && vector.Y < 0) return UpRight;
      if (vector.X < 0) return Left;
      if (vector.X > 0) return Right;
      if (vector.Y > 0) return Down;
      if (vector.Y < 0) return Up;
      return string.Empty;
    }
  }
}