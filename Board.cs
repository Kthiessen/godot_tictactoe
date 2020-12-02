using Godot;
using System;
using System.Collections.Generic;

public class Board : Node2D
{
    private int playerTurn = 0;

    private List<Square> board;

    public override void _Ready()
    {
        board = new List<Square>();

        foreach (Node2D child in GetChildren())
        {
            if (child is Square)
            {
                child.Connect("Clicked", this, nameof(OnSquareClicked));
                board.Add((Square)child);
            }
        }
    }

    public void OnSquareClicked(Square target)
    {
        target.SetPlayer(playerTurn);

        if (CheckForWinner())
        {
            GD.Print((playerTurn == 0 ? "First" : "Second") + " Player Won!");
        }

        playerTurn = ++playerTurn % 2;
    }

    private bool CheckForWinner()
    {
        bool victory = true;

        // Horizontal
        for (int i = 0; i < 3; i++)
        {
            victory = true;
            for (int j = 0; j < 3; j++)
            {
                int index = i * 3 + j;
                if (board[index].selectedByPlayer != playerTurn)
                {
                    victory = false;
                    break;
                }
            }
            if (victory)
            {
                return true;
            }
        }

        // Vertical
        for (int i = 0; i < 3; i++)
        {
            victory = true;
            for (int j = 0; j < 3; j++)
            {
                int index = i + j * 3;
                if (board[index].selectedByPlayer != playerTurn)
                {
                    victory = false;
                    break;
                }
            }
            if (victory)
            {
                return true;
            }
        }

        // Diagonal Down Right
        victory = true;
        for (int i = 0; i < 3; i++)
        {
            int index = i * 4;
            if (board[index].selectedByPlayer != playerTurn)
            {
                victory = false;
                break;
            }
        }
        if (victory)
        {
            return true;
        }

        // Diagonal Up Right
        victory = true;
        for (int i = 0; i < 3; i++)
        {
            int index = i * 2 + 2;
            if (board[index].selectedByPlayer != playerTurn)
            {
                victory = false;
                break;
            }
        }
        if (victory)
        {
            return true;
        }

        return false;
    }
}
