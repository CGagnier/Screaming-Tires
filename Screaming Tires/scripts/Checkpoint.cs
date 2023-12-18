using Godot;
using System;

public partial class Checkpoint : Area3D
{
	[Signal]
	public delegate void HasBeenChecked();

	public Checkpoint Last { get; set; }
	public Checkpoint Next { get; set; }
	public bool Checked { get; set; }

	private Material unactiveMat;


	/// <summary>
	/// Initialize the Checkpoint to the specified position, then set his Checked value to false
	/// </summary>
	/// <param name="pos">Position</param>
	public void Initialize(Vector3 pos)
	{
		Translate(pos);
		Checked = false;
	}

	public override string ToString()
	{
		return ("Checked: "+ Checked +" Last: " + Last + " Next: " + Next);
	}

	public override void _Ready()
	{
		// Show the first checkpoint
		Visible = (Last == null);

		AnimationPlayer floating = GetNode<AnimationPlayer>("Arrow/Anim");
		unactiveMat = (Material)GD.Load("res://materials/UnactiveFlag.tres");

		if (Last != null)
			Last.LookAt(Position, new Vector3(0,1,0));
		
		// Rotate the checkpoint to the Next one direction
		if (Next == null){ 
			// Play self rotate animation
			floating.Play("Spin");
			Rotate(new Vector3(1,0,0),-1.57f);
		}else {
			floating.Play("Float");
		}
	}

	public void CheckIt() {

		Checked = true;
		EmitSignal("HasBeenChecked");

		// Show the Next checkpoint and hide the Last one
		if (Last != null)
			Last.Hide();
		
		// If there's no next checkpoint, game is over, else show the next one. And Change color to inacive checkpoint
		if (Next != null) {
			Next.Show(); 
			MeshInstance3D arrow = (MeshInstance3D)GetNode("Arrow");
			arrow.SetSurfaceOverrideMaterial(0,(Material)unactiveMat);
		}
		
	}

	// Waiting for collision with the vehicule to set the checkpoint visibility
	public void BodyEntered(Godot.Object col) {
		bool isVehicule = col is VehicleBody3D;
		if (!Checked && isVehicule)
			CheckIt();
	}

}
