using Godot;
using System;

public class GameController : Node
{

    Vector3 START_POSITION = new Vector3();
    public override void _Ready()
    {
        // Should read from loaded map what is the starting position for the player and place the player
        
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_accept")) {
            // Reset car to starting position;
        }
        
    }
}
