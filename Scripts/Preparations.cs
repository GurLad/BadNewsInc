using Godot;
using System;

public partial class Preparations : Control
{
    [Export] // For debugging
    public int CurrentLetter = 1;
    private AudioStreamPlayer player;

    public override void _Ready()
    {
        base._Ready();
        player = GetNode<AudioStreamPlayer>("PreviewPlayer");
    }

    public void Preview()
    {

    }
}
