using Godot;
using System;
public partial class Circuit {

		public static int count = 0;

		private int id;
		private string name;
		private Vector3 spawnpoint;
		private Vector3[] checkpoints;
		public Circuit()
		{
			// By default the name will be Race 1, Race 2, etc
			name = "Race "+ (count+1);
			id = count;
			spawnpoint = new Vector3();
			count++;
		}
		public Circuit(Vector3[] pCheckpoints):this(){
			checkpoints = pCheckpoints;
		}
		public Circuit(Vector3[] pCheckpoints, string pName):this(pCheckpoints){
			name = pName;
		}

}
