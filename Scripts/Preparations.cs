using Godot;
using System;

public partial class Preparations : Control
{
    [Export] // For debugging
    public int CurrentLetter = 0;
    private AudioStreamPlayer player;
    private MessengerSelect messengerSelect;
    private Timer timer;
    private bool beganDelay = false;
    private int currentPlaying = -1;

    public override void _Ready()
    {
        base._Ready();
        player = GetNode<AudioStreamPlayer>("PreviewPlayer");
        messengerSelect = GetNode<MessengerSelect>("HBoxContainer/VBoxContainer/MessengerPanel");
        timer = GetNode<Timer>("VADelay");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (currentPlaying >= 0 && !player.Playing)
        {
            if (!beganDelay)
            {
                beganDelay = true;
                timer.Start();
                return;
            }
            if (timer.TimeLeft <= 0)
            {
                player.Stream = VoiceActingController.GetVA(CurrentLetter, messengerSelect.SelectedMessenger.IntToMessengerName() + ++currentPlaying);
                if (player.Stream == null)
                {
                    currentPlaying = -1;
                }
                else
                {
                    player.Play();
                    beganDelay = false;
                }
            }
        }
    }

    public void Preview()
    {
        player.Stop();
        currentPlaying = 0;
    }
}
