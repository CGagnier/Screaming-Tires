using Godot;
using System;

public class Checkpoint : Area
{
    [Signal]
    public delegate void HasBeenChecked();

    public Checkpoint Last { get; set; }
    public Checkpoint Next { get; set; }
    public bool Checked { get; set; }

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

        GD.Print("I'm at",Translation);

        if (Last != null){
            GD.Print("The last is at", Last.Translation);
            Last.LookAt(Translation, new Vector3(0,1,0));
        }

        // Rotate the checkpoint to the Next one direction
        if (Next == null){ 
            GD.Print("Final Checkpoint, should look at the ground");
            // TODO: Add animation (u/d + rotate)
            Rotate(new Vector3(1,0,0),-1.57f);
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
    }

    // Waiting for collision with the vehicule to set the checkpoint visibility
    public void BodyEntered(Godot.Object col) {
        bool isVehicule = col is VehicleBody;
        if (!Checked && isVehicule)
            CheckIt();
    }

}
