using Godot;
using System;

public class Square : Godot.Node2D
{
    private Rect2 boundingBox;
    private Polygon2D poly;

    public int selectedByPlayer { get; private set; } = -1;

    [Signal]
    public delegate void Clicked(Square target);

    public override void _Ready()
    {
        poly = GetNode<Polygon2D>("Polygon2D");

        Vector2 minPoint = new Vector2(float.MaxValue, float.MaxValue);
        Vector2 maxPoint = new Vector2(float.MinValue, float.MinValue);

        foreach (Vector2 vertex in poly.Polygon)
        {
            minPoint.x = Math.Min(vertex.x, minPoint.x);
            minPoint.y = Math.Min(vertex.y, minPoint.y);
            maxPoint.x = Math.Max(vertex.x, maxPoint.x);
            maxPoint.y = Math.Max(vertex.y, maxPoint.y);
        }

        minPoint.x *= poly.Scale.x;
        minPoint.y *= poly.Scale.y;
        maxPoint.x *= poly.Scale.x;
        maxPoint.y *= poly.Scale.y;

        boundingBox = new Rect2(ToGlobal(minPoint), maxPoint - minPoint);
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

            Vector2 mousePos = ((InputEventMouse)inputEvent).Position;

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
            if (playerTurn == 0)
            {
                GetNode<Polygon2D>("Polygon2D").Color = new Color(0, 255, 0);
            }
            else
            {
                GetNode<Polygon2D>("Polygon2D").Color = new Color(255, 0, 0);
            }
            selectedByPlayer = playerTurn;
        }
    }
}
