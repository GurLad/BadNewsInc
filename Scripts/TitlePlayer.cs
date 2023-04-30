using Godot;
using System;

public partial class TitlePlayer : AudioStreamPlayer // Wheeee it's almost 2 am terrible script time
{
    private static bool heard = false;

    public override void _Ready()
    {
        base._Ready();
        if (heard)
        {
            QueueFree();
        }
        heard = true;
    }
}
