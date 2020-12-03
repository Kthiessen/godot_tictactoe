using Godot;
using System;

public class Game : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Board board = GetNode<Board>("Board");
        board.Connect("Victory", this, nameof(OnVictory));
        board.Connect("GameOver", this, nameof(OnGameOver));

        GetNode<Button>("Restart").Connect("pressed", this, nameof(OnRestartPressed));
        GetNode<Button>("MainMenu").Connect("pressed", this, nameof(OnMainMenuPressed));
    }


    public void ShowFinish(string text)
    {
        Label endGameText = GetNode<Label>("EndGameText");
        endGameText.Text = text;
        endGameText.Visible = true;

        GetNode<Button>("Restart").Visible = true;
        GetNode<Button>("MainMenu").Visible = true;
    }

    private void OnVictory(int playerNum)
    {
        ShowFinish((playerNum == 0 ? "First" : "Second") + " Player Won!");
    }

    private void OnGameOver()
    {
        ShowFinish("Cat's Game!");
    }

    private void OnRestartPressed()
    {
        GetTree().ChangeScene("res://scenes/Game.tscn");
    }

    private void OnMainMenuPressed()
    {
        GetTree().ChangeScene("res://scenes/MainMenu.tscn");
    }
}
