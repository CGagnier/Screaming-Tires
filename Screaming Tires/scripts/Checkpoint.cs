using Godot;
using System;

public class Checkpoint : Area
{
    [Signal]
    public delegate void HasBeenChecked();
    [Signal]
    public delegate void Won();

    public Checkpoint Last { get; set; }
    public Checkpoint Next { get; set; }
    public bool Checked { get; set; }

    public void Initialize(Vector3 pos)
    {
        Translate(pos);
        Checked = false;
    }


    public override void _Ready()
    {
        // Show the first checkpoint
        Visible = (Last == null);

        // Rotate the checkpoint to the Next one direction
        if (Next != null){
            //LookAt(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        }
    }

    public void CheckIt() {

        Checked = true;
        EmitSignal("HasBeenChecked");
        // Need to change color from green to gray? 

        // Show the Next checkpoint and hide the Last one
        if (Last != null){
            Last.Hide();
        }

        // If there's no next checkpoint, game is over, else show the next one.
        if (Next != null) {
            Next.Show();
        }
        else {
            EmitSignal("Won");
        }

    }

    // Waiting for collision with the vehicule to set the checkpoint visibility
    public void BodyEntered(Godot.Object col) {
        bool isVehicule = col is VehicleBody;
        if (isVehicule)
            CheckIt();
    }

}
