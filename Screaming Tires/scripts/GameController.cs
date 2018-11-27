using Godot;
using System;

public class GameController : Node
{

    //Vector3 START_POSITION = new Vector3();

    Container pauseScreen;

    public override void _Ready()
    {
        // Should read from loaded map what is the starting position for the player and place the player
        // Place the checkpoints
        // Start a Timer
        // 
        pauseScreen = (Container) GetNode("UI/HBox/PausedScreen");
        pauseScreen.Visible = false;
        
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_accept")) {
            // Reset car to starting position then add 1 sec to the timer

        }




        if (Input.IsActionJustPressed("ui_cancel")) 
            TogglePause(true);
           
    }
    // Use to toggle the game pause state
    public void TogglePause(bool state) {
        GD.Print("Toggle Pause " + state.ToString());

        pauseScreen.Visible = state;

        GetTree().Paused = state;


    }
}
