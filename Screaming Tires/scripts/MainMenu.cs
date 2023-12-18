using Godot;
using System;

public partial class MainMenu : TextureRect
{
	public Circuit[] circuits;

	public Node Race = null;
	public override void _Ready() {
		GenerateCircuits();
	}

	void LoadGame(int indexForCircuit) {
		GD.Print("Change to circuit id: "+ indexForCircuit);

		Race = GD.Load<PackedScene>("res://scenes/Main.tscn").Instantiate();
		Race.GetNode("HUD").Connect("MainMenu", new Callable(this, "BackToMenu"));
		// TODO: Call generate checkpoints with the selected circuit

		GetParent().AddChild(Race);
		Hide();
	}
	public void BackToMenu() {
		Race.QueueFree();
		Show();
	}
	void ShowRaces() {
		// Hide inital menu, and show level select
	}
	void QuitGame() {
		GetTree().Quit();
	}
	void PraticeGame() {
		// Load map, without timer or checkpoints
	}

	void GenerateCircuits() {

		circuits = new Circuit[2];

		Vector3[] checkpoints = new Vector3[3];
		checkpoints[0] = new Vector3(10, 0, 0);
		checkpoints[1] = new Vector3(11, 0, 16);
		checkpoints[2] = new Vector3(0, 0, 16);

		circuits[0] = new Circuit(checkpoints);

		checkpoints = new Vector3[2];
		checkpoints[0] = new Vector3(1, 0, 0);
		checkpoints[1] = new Vector3(0, 0, 15);

		circuits[1] = new Circuit(checkpoints,"Race B");
	}

}

