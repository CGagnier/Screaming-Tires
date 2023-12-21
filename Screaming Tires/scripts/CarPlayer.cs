using Godot;
using System;

public partial class CarPlayer : VehicleBody3D
{

	const float maxEngineForce = 30f;
	const float steerMax = 0.8f;
	const float steerSpeed = 0.5f;

	double steer_angle = 0;
	double steer_target = 0;

	MeshInstance3D car;

	public StandardMaterial3D lightsOff;
	public StandardMaterial3D lightsOn;

	Node3D lightsGroup;


	public override void _Ready()
	{

		//lightsOff = (SpatialMaterial)ResourceLoader.Load("res://materials/FrontLights_OFF.tres");
		//lightsOn = (SpatialMaterial)ResourceLoader.Load("res://materials/FrontLights_ON.tres");

		lightsGroup = (Node3D)GetNode("Front_Lights");

		car = (MeshInstance3D) GetNode("CarMesh");

	}
	public override void _PhysicsProcess(double delta)
	{
		// Check for inputs. Steer + speed
		GD.Print("Steer: " + steer_angle);

		// Need to replace by voice pitch
		if (Input.IsActionPressed("ui_left")) // High Pitch
			steer_target = steerMax; 
		else if (Input.IsActionPressed("ui_right")) // Low Pitch
			steer_target = -steerMax;
		else 
			steer_target = 0;

		if (Input.IsActionPressed("ui_up")) {


			EngineForce = maxEngineForce;
			Console.WriteLine("EngineForce: " + EngineForce);

			//Can we turn on the front lights? 
			//car.SetSurfaceMaterial(0, lightsOn);
			lightsGroup.Show();

		}
		else if (Input.IsActionPressed("ui_down")){
			Brake = 0;
			EngineForce = -maxEngineForce;
		}
		else {
			//car.SetSurfaceMaterial(0, lightsOff);
			lightsGroup.Hide();
			EngineForce = 0;
			Brake = 0.5f;

		}

		if (steer_target < steer_angle) {

			steer_angle -= steerSpeed * delta; //Steer speed should match volume?
			if (steer_target > steer_angle){
				steer_angle = steer_target;
			}

		}else if (steer_target > steer_angle) {
			steer_angle += steerSpeed * delta; //Steer speed should match volume?
			if (steer_target < steer_angle){
				steer_angle = steer_target;
			}
		}

		Steering = (float)steer_angle;
	}

	public void OnCollision (Object body) {
		GD.Print(body);
	}
}
