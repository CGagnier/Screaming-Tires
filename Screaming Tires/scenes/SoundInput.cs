using Godot;
using System;
//using AudioServer;

public class SoundInput : AudioStreamPlayer
{
    // Declare member variables here. Examples:
    private float baseline;
	private float peakRight;
	private float peakLeft;
	private float volume;

    private float volrec;

    private AudioEffectRecord rec;
    int recordBusIndex;
	
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        baseline = AudioServer.GetBusVolumeDb(0);
		GD.Print("Sound baseline: " + baseline );

        recordBusIndex = AudioServer.GetBusIndex("Recording");

        GD.Print("Recording bus index is " + recordBusIndex);
        rec = (AudioEffectRecord) AudioServer.GetBusEffect(1,0);
        GD.Print(rec);
        //rec.SetRecordingActive(true);

               
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	baseline = AudioServer.GetBusVolumeDb(0);
	peakLeft = AudioServer.GetBusPeakVolumeLeftDb(1, 0);
	peakRight = AudioServer.GetBusPeakVolumeRightDb(1, 0);

	volrec = AudioServer.GetBusPeakVolumeLeftDb(1, 0);
	volume = peakLeft + baseline;

	if ( volume > 0) {
		GD.Print("We heard some sound: " + volume );
		Input.ActionPress("ui_up");
	}
    if (volrec > 0)
    {
        GD.Print("We heard some soxzgdund: " + volrec);
    }

    }
}
