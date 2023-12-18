using Godot;
using System;

public partial class HUD : MarginContainer
{
	[Signal]
	public delegate void PauseGame();
	[Signal]
	public delegate void ResumeGame();
	[Signal]
	public delegate void RestartGame();
	[Signal]
	public delegate void MainMenu();

	private int totNumOfCheckpoints;
	private int currentNumOfCheckpoints;

	public void UpdateTimer(double time) {
		// Time should be formatted as mm:ss
		TimeSpan formattedTime = TimeSpan.FromSeconds(time);
		GetNode<Label>("GameControls/Top/labels/TimerLabel").Text = formattedTime.ToString();
	}
	public void UpdateHighScore(double time) {
		TimeSpan formattedHSTime = TimeSpan.FromSeconds(time);
		GetNode<Label>("GameControls/Top/labels/HighScoreLabel").Text = "Best time "+ formattedHSTime.ToString();
	}
	public void SetPauseScreenVisibility(bool state) {
		if (state) {
			GetNode<CenterContainer>("GameControls/PausedScreen").Show();
		}else {
			GetNode<CenterContainer>("GameControls/PausedScreen").Hide();
		}
		
	}

	public void SetNumberOfCheckpoints(int num) {
		totNumOfCheckpoints = num;
		currentNumOfCheckpoints = 0;

		GetNode<Label>("GameControls/Top/Checkpoints/FlagLabel").Text = currentNumOfCheckpoints+ "/"+totNumOfCheckpoints;
	}
	public void IncreaseCheckpointCount() {
		currentNumOfCheckpoints++;
		GetNode<Label>("GameControls/Top/Checkpoints/FlagLabel").Text = currentNumOfCheckpoints+ "/"+totNumOfCheckpoints;
	}

	private void OnButtonPausePressed()
	{
		SetPauseScreenVisibility(true);
		EmitSignal("PauseGame");
	}
	private void OnResume() {
		SetPauseScreenVisibility(false);
		EmitSignal("ResumeGame");
	}
	private void OnRestart() {
		EmitSignal("RestartGame");
	}
	private void OnMainMenu()
	{
		EmitSignal("MainMenu");
	}


}



