using Godot;
using System;

public class Square : Sprite
{
    private Rect2 boundingBox;

    public int selectedByPlayer { get; private set; } = -1;
    public Texture[] playerTextures;

    [Signal]
    public delegate void Clicked(Square target);

    public override void _Ready()
    {
        PreloadTextures();
        GetBoundingBox();
    }

    private void PreloadTextures()
    {
        playerTextures = new Texture[] {
            GD.Load<Texture>("res://assets/X.png"),
            GD.Load<Texture>("res://assets/O.png")
        };
    }

    private void GetBoundingBox()
    {
        boundingBox = new Rect2(new Vector2(-50, -50), new Vector2(100, 100));
    }
    public override void _Input(InputEvent inputEvent)
    {
        base._Input(inputEvent);
        if (inputEvent.IsActionPressed("click"))
        {
            if (selectedByPlayer >= 0)
            {
                return;
            }

            Vector2 mousePos = ToLocal(((InputEventMouse)inputEvent).Position);

            if (boundingBox.HasPoint(mousePos))
            {
                EmitSignal(nameof(Clicked), this);
            }
        }
    }

    public void SetPlayer(int playerTurn)
    {
        if (selectedByPlayer < 0)
        {
            Texture = playerTextures[playerTurn];
            selectedByPlayer = playerTurn;
        }
    }
}
