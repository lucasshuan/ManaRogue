using ManaRogue.Utils;
using Godot;

namespace ManaRogue.Entity
{
	public partial class Player : CharacterBody2D
	{
		private float _speed = 65.0f;
		private const float _acceleration = 0.25f;
		private const float _friction = 0.35f;

		public float WalkSpeed => _speed;
		public float RunSpeed => _speed * 1.5f;

		public bool Disabled = false;

		public string CurrentDirection = Direction.Down;

		private AnimationPlayer _animationPlayer => GetNode<AnimationPlayer>("AnimationPlayer");

		public static Player Instance { get; private set; }

		public override void _EnterTree()
		{
			Instance = this;
		}

		public override void _PhysicsProcess(double delta)
		{
			if (Disabled)
			{
				Velocity = Velocity.Lerp(Vector2.Zero, _friction);
				return;
			}

			var direction = Input.GetVector(Keybind.Left, Keybind.Right, Keybind.Up, Keybind.Down);
			var isRunning = Input.IsActionPressed(Keybind.Sprint);

			if (direction == Vector2.Zero)
			{
				Velocity = Velocity.Lerp(Vector2.Zero, _friction);
				_animationPlayer.Play($"Idle/{CurrentDirection}");
			}
			else
			{
				var speed = isRunning ? RunSpeed : WalkSpeed;
				Velocity = Velocity.Lerp(direction * speed, _acceleration);
				CurrentDirection = Direction.FromVector2(direction);
				var animationGroup = isRunning ? "Run" : "Walk";
				_animationPlayer.Play($"{animationGroup}/{CurrentDirection}");
			}

			MoveAndSlide();
		}
	}
}
