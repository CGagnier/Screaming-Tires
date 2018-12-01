using Godot;
using System;

public class MainMenu : TextureRect
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
	void LoadGame() {
        GetTree().ChangeScene("res://scenes/Main.tscn");
	}
    void QuitGame() {
        GetTree().Quit();
    }
    void PraticeGame() {

    }

}
