using Godot;
using System;

public class CheckpointsGenerator : Spatial
{
    [Signal]
    public delegate void HasBeenChecked();

    [Export]
    public PackedScene checkpointNode;


    /// <summary>
    /// Generates the checkpoints 
    /// </summary>
    /// <param name="listOfCheckpoints">Array of Vector3 position where the checkpoints should be placed</param>
    public void GenerateCheckpoints(Vector3[] listOfCheckpoints)
    {
        GD.Print("Started to generate checkpoints");
        Checkpoint[] tempCheckpoints = new Checkpoint[listOfCheckpoints.Length];

        // First iteration we only create instances of the checkpoint
        for (int i = 0; i < listOfCheckpoints.Length; i++)
        {
            Checkpoint temp = (Checkpoint)checkpointNode.Instance();
            tempCheckpoints[i] = temp;
        }

        // The second pass is to add the elements to the level, and set next and last values
        for (int j = 0; j < tempCheckpoints.Length; j++)
        {
            tempCheckpoints[j].Initialize(listOfCheckpoints[j]);

            if (j > 0){
                GD.Print("We have a last");
                tempCheckpoints[j].Last = tempCheckpoints[j - 1];
            }
            if (j < tempCheckpoints.Length - 1){
                GD.Print("We have a next");
                GD.Print(tempCheckpoints[j + 1]);
                tempCheckpoints[j].Next = tempCheckpoints[j + 1];
            }
            tempCheckpoints[j].Connect("HasBeenChecked", this, nameof(Checked));
            AddChild(tempCheckpoints[j]);
        }
    }

    public void Checked(){EmitSignal("HasBeenChecked");}
}
