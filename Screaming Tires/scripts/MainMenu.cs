using Godot;
using System;

public class MainMenu : TextureRect
{
	void LoadGame() {
        GD.Print("Change to gameß");
        GetTree().ChangeScene("res://scenes/Main.tscn");
	}
    void QuitGame() {
        GetTree().Quit();
    }
    void PraticeGame() {

    }

}
