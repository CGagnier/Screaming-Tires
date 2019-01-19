using Godot;
using System;

public class Menu : Node
{
    public override void _Ready()
    {
        // Menu animation that navigate through the map
        AnimationPlayer trac = GetNode<AnimationPlayer>("Track/Anim");
        trac.Play("Travel");
    }
}
