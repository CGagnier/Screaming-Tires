using Godot;
using System;

public class CarPlayer : VehicleBody
{

    const float maxEngineForce = 50f;
    const float steerMax = 0.9f;
    const float steerSpeed = 1;

    float steer_angle = 0;
    float steer_target = 0;

    MeshInstance car;

    public SpatialMaterial lightsOff;
    public SpatialMaterial lightsOn;

    Spatial lightsGroup;


    public override void _Ready()
    {

        lightsOff = (SpatialMaterial)ResourceLoader.Load("res://materials/FrontLights_OFF.tres");
        lightsOn = (SpatialMaterial)ResourceLoader.Load("res://materials/FrontLights_ON.tres");

        lightsGroup = (Spatial)GetNode("Front_Lights");
        

        car = (MeshInstance) GetNode("CarMesh");

    }
    public override void _PhysicsProcess(float delta)
    {
        // Check for inputs. Steer + speed

        // Need to replace by voice pitch
        if (Input.IsActionPressed("ui_left")) // High Pitch
            steer_target = steerMax; 
        else if (Input.IsActionPressed("ui_right")) // Low Pitch
            steer_target = -steerMax;
        else 
            steer_target = 0;

        if (Input.IsActionPressed("ui_up")) {

            EngineForce = maxEngineForce;

            //Can we turn on the front lights? 
            car.SetSurfaceMaterial(0, lightsOn);
            lightsGroup.Show();
            
            //car.SetMaterialOverride(lightsOn);
        }
        else if (Input.IsActionPressed("ui_down"))
            EngineForce = -maxEngineForce;
        else {
            car.SetSurfaceMaterial(0, lightsOff);
            lightsGroup.Hide();
            EngineForce = 0;

        }
       // EngineForce = Input.IsActionPressed("ui_up")? maxEngineForce : 0;

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

        Steering = steer_angle;
    }
}