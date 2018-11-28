using Godot;
using System;

public class HUD : MarginContainer
{
    [Signal]
    public delegate void PauseGame();
    [Signal]
    public delegate void ResumeGame();

    public void UpdateTimer(double time) {
        // Time should be formatted as mm:ss
        TimeSpan formattedTime = TimeSpan.FromSeconds(time);

        GetNode<Label>("HBox/HBoxContainer/TimerLabel").Text = formattedTime.ToString();
    }
    public void SetPauseScreenVisibility(bool state) {
        if (state) {
            GetNode<CenterContainer>("HBox/PausedScreen").Show();
        }else {
            GetNode<CenterContainer>("HBox/PausedScreen").Hide();
        }
	    
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


}



