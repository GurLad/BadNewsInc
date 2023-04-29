using Godot;
using System;

public partial class Preparations : Control
{
    [Export] // For debugging
    public int CurrentLetter = 0;
    private AudioStreamPlayer player;
    private MessengerSelect messengerSelect;
    private int currentPlaying = -1;

    public override void _Ready()
    {
        base._Ready();
        player = GetNode<AudioStreamPlayer>("PreviewPlayer");
        messengerSelect = GetNode<MessengerSelect>("HBoxContainer/VBoxContainer/MessengerPanel");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (currentPlaying >= 0 && !player.Playing)
        {
            player.Stream = VoiceActingController.GetVA(CurrentLetter, messengerSelect.SelectedMessenger.IntToMessengerName() + ++currentPlaying);
            if (player.Stream == null)
            {
                currentPlaying = -1;
            }
            else
            {
                player.Play();
            }
        }
    }

    public void Preview()
    {
        player.Stop();
        currentPlaying = 0;
    }
}
