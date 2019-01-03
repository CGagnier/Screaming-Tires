using Godot;
using System;

public class GameController : Node
{

    //Vector3 START_POSITION = new Vector3();
    double raceTime;
    int totalCheckpoints;
    int checkpointsLeft;

    public override void _Ready()
    {
        // Should read from loaded map what is the starting position for the player and place the player

        GetNode<HUD>("HUD").SetPauseScreenVisibility(false);
        NewGame();
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_cancel")) 
            TogglePause(true);
           
    }
    public void ReceivedCheckpoint() {
        GD.Print("One more");
        checkpointsLeft--;
        if (checkpointsLeft == 0){
            GameOver();
        }
    }
    public void GameOver() // This is called once the user win the game
    {
        GD.Print("All checkpoint reached");
        GetNode<Timer>("timers/Timer").Stop();
        GetTree().Paused = true; // Stop player inputs instead

        //Open menu to say restart or main menu + save time to highscores?

    }
    public void NewGame() {

        raceTime = 0.0;
        GetNode<Timer>("timers/Timer").Start();
        

        // Generate Checkpoints based on Array of Vectors

        Vector3[] checkpoints = new Vector3[3];
        checkpoints[0] = new Vector3(10, 0, 0);
        checkpoints[1] = new Vector3(11, 0, 16);
        checkpoints[2] = new Vector3(0, 0, 16);

        totalCheckpoints = checkpoints.Length;
        checkpointsLeft = totalCheckpoints;

        GetNode<CheckpointsGenerator>("CheckpointsContainer").GenerateCheckpoints(checkpoints);
    }


    public void RestartGame() {
        // TODO: Reset time, replace objects, place car back to start
        //GetTree().Paused = false;
        //GetTree().ChangeScene("res://scenes/Main.tscn");
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
