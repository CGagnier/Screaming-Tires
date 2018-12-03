using Godot;
using System;

public class Checkpoint : Area
{
    [Signal]
    public delegate void HasBeenChecked();
    [Signal]
    public delegate void Won();

    bool Checked;
    public Checkpoint previous = null;
    public Checkpoint next = null;

    public void Initialize(Vector3 pos, Checkpoint prev)
    {
        Translate(pos);
        if (prev != null) {
            previous = prev;
        }
       
        Checked = false;

        // We want to show the first checkpoint
        Visible = (previous == null);

        GD.Print("READY " + next);
        // We want then to rotate it to point to the next checkpoint
        if (next != null)
        {
            GD.Print("Should trun to next");
            //Rotate head to next.Transform
            //LookAt(new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        }
    }


    public static Checkpoint AddCheckpoint(Checkpoint head,Checkpoint child)
    {
        Checkpoint p = child;


        if (head == null)
            head = p;
        else if (head.next == null)
            head.next = p;
        else
        {
            Checkpoint start = head;
            while (start.next != null)
                start = start.next;
            start.next = p;

        }
        return head;
    }

    public override void _Ready()
    {


    }

    // Used at initialisation to 
    public void MoveToPosition(float x, float y) {

    }

    public void CheckIt() {

        Checked = true;

        // We want to show the next checkpoint and hide the previous one
        if (next != null) {
            EmitSignal("HasBeenChecked");
            next.Show();
            if (previous != null) {
                previous.Hide();
            }
        }
        else {// If there's no next checkpoint, game is over.
            // Time should stop in 3 seconds, slowly
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
