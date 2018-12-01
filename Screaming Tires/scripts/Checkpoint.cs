using Godot;
using System;

public class Checkpoint : StaticBody
{
    bool hasBeenChecked = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void CheckIt() {
        hasBeenChecked = true;
        //change color to green;
    }

    public override void _Process(float delta)
    {
      
    }
}
