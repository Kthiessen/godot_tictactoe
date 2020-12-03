using Godot;
using System;

public class MainMenu : Node2D
{
    public override void _Ready()
    {
        GetNode<Button>("StartGame").Connect("pressed", this, nameof(OnStartGamePressed));
    }

    private void OnStartGamePressed()
    {
        GetTree().ChangeScene("res://scenes/Game.tscn");
    }
}
