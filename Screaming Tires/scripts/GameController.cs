using Godot;
using System;

public class GameController : Node
{

    //Vector3 START_POSITION = new Vector3();
    double raceTime;
    int NbOfCheckpoints;
    int currentCheckpoints;

    public override void _Ready()
    {
        // Should read from loaded map what is the starting position for the player and place the player
        // Place the checkpoints 

        GetNode<HUD>("HUD").SetPauseScreenVisibility(false);
        NewGame();
        
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_cancel")) 
            TogglePause(true);
           
    }
    public void GameOver() // This is called once the user win the game
    {
        GetNode<Timer>("Timer").Stop();
        GetTree().Paused = true; // Or stop time and player?

        //Should open menu to say restart or main menu + save time to highscores?

    }
    public void NewGame() {

        raceTime = 0.0;
        GetNode<Timer>("Timer").Start();
        currentCheckpoints = 0;
        NbOfCheckpoints = 0;


    }
    public void RestartGame() {
        // Reset time, replace objects, place car back to start
        GetTree().Paused = false;
        GetTree().ChangeScene("res://scenes/Main.tscn");
    }
    public void OnTimerTimeout() {

        raceTime++;
        GetNode<HUD>("HUD").UpdateTimer(raceTime);
    }

    // Use to toggle the game pause state
    public void TogglePause(bool state) {
        GetTree().Paused = state;
    }

    public void ChangeMain() {
        GetTree().ChangeScene("res://scenes/MainMenu.tscn");
    }
}
