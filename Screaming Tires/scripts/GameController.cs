using Godot;
using System;

public class GameController : Node
{

    //Vector3 START_POSITION = new Vector3();
    double raceTime;

    [Export]
    PackedScene check;

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

        // Generate Checkpoints based on Array of Vectors

        Vector3[] checkpoints = new Vector3[3];
        checkpoints[0] = new Vector3(10, 0, 0);
        checkpoints[1] = new Vector3(11, 0, 16);
        checkpoints[2] = new Vector3(0, 0, 16);

        GenerateCheckpoints(checkpoints);


    }
    public void GenerateCheckpoints(Vector3[] listOfCheckpoints) {

        Checkpoint[] tempCheckpoints = new Checkpoint[listOfCheckpoints.Length];

        // First iteration we only create instances of the checkpoint
        for (int i = 0; i < listOfCheckpoints.Length;i++) {
            Checkpoint temp = (Checkpoint)check.Instance();
            tempCheckpoints[i] = temp;
        }

        // The second pass is to add the elements to the level, and set next and last values
        for (int j = 0; j < tempCheckpoints.Length; j++) {

            tempCheckpoints[j].Initialize(listOfCheckpoints[j]);

            if (j> 0) {
                tempCheckpoints[j].Last = tempCheckpoints[j - 1];
            }
            if (j < tempCheckpoints.Length-1){
                tempCheckpoints[j].Next = tempCheckpoints[j + 1];
            }
            GetNode("CheckpointsContainer").AddChild(tempCheckpoints[j]);
        }

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
