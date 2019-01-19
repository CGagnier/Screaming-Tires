using Godot;
using System;
using System.Collections.Generic;

public class GameController : Node
{

    //Vector3 START_POSITION = new Vector3();
    double raceTime;
    public double bestTime;
    int totalCheckpoints;
    int checkpointsLeft;

    HUD hudNode;

    public int circuitID;
    

    const string filepath = "user://highscore.save";

    public override void _Ready()
    {
        circuitID = 1; // temp debug

        // Should read from loaded map what is the starting position for the player and place the player
        hudNode = GetNode<HUD>("HUD");
        hudNode.SetPauseScreenVisibility(false);

        bestTime = LoadHighScore(circuitID);
        hudNode.UpdateHighScore(bestTime);
        
        NewGame();
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_cancel")) 
            TogglePause(true);     
    }

    public void ReceivedCheckpoint() {
        checkpointsLeft--;
		hudNode.IncreaseCheckpointCount();
        if (checkpointsLeft == 0){
            GameOver();
        }
    }
    public void GameOver() // This is called once the user win the game
    {
        GetNode<Timer>("timers/Timer").Stop();
        GetTree().Paused = true; // Stop player inputs instead

        //Open menu to say restart or main menu + save time to highscores?
        if (bestTime == 0 || raceTime < bestTime) {
            SaveHighScore();
        }
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

        hudNode.SetNumberOfCheckpoints(totalCheckpoints);

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

    #region HighScore + Dictionary Methods
    public void SaveHighScore() {

        var ScoreFile = new File();
        ScoreFile.Open(filepath,(int)File.ModeFlags.Write);

        Dictionary<int,double> savedValues = Save();

        ScoreFile.StoreLine(DicToString(savedValues));
        ScoreFile.Close();
    }
    public double LoadHighScore(int pCircuitId) {
        var SavedFile = new File();
        if (!SavedFile.FileExists(filepath)) {
            return 0.0;
        }else {
            SavedFile.Open(filepath,(int)File.ModeFlags.Read);

            while(!SavedFile.EofReached()) {

                var gotine = SavedFile.GetLine();
                if (gotine==null) 
                    continue;

                Dictionary<int,double> loadedLine = StringToDic(gotine);
                if (loadedLine.ContainsKey(pCircuitId)) {
                    return loadedLine[pCircuitId];
                }                
            }

            SavedFile.Close();
        }
        return 0.0;
    }
    public Dictionary<int, double> Save()
    {
        return new Dictionary<int, double>()
        {
            {circuitID, raceTime }
        };
    }
    public string DicToString(Dictionary<int,double> dic) {
        
        var tempa = new List<string>();
        foreach( var ke in dic) {
            tempa.Add(ke.Key +":"+ ke.Value);
        }
        var a = tempa.ToArray();

        return "{" + string.Join(",",a) + "}";
        
    }
    public Dictionary<int,double> StringToDic(string dic) {

        if (dic.StartsWith("{") && dic.EndsWith("}")) {
            string[] processed = dic.Substr(1,dic.Length-2).Split(':');

            int keyInt;
            double valueDbl;
            if (processed.Length <= 2) {
                if (Int32.TryParse(processed[0],out keyInt) && Double.TryParse(processed[1],out valueDbl) )
                    return new Dictionary<int,double>(){{keyInt,valueDbl}};
            }
        }
        return new Dictionary<int,double>(){{0,0.0}}; // The conversion didn't work we still return a dictionary
    }
    #endregion HighScore

}


