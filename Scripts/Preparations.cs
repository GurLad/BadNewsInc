using Godot;
using System;
using System.Collections.Generic;

public partial class Preparations : Control
{
    [Export] // For debugging
    public int CurrentLetter = 0;
    private AudioStreamPlayer player;
    private MessengerSelect messengerSelect;
    private Timer timer;
    private Label letterText;
    private Label targetName;
    private Label targetDescription;
    private Portrait targetPortrait;
    private bool beganDelay = false;
    private int currentPlaying = -1;

    public override void _Ready()
    {
        base._Ready();
        player = GetNode<AudioStreamPlayer>("PreviewPlayer");
        messengerSelect = GetNode<MessengerSelect>("HBoxContainer/VBoxContainer/MessengerPanel");
        timer = GetNode<Timer>("VADelay");
        letterText = GetNode<Label>("HBoxContainer/PanelContainer/Letter");
        targetName = GetNode<Label>("HBoxContainer/VBoxContainer/TargetPanel/VBoxContainer/HBoxContainer/Name");
        targetDescription = GetNode<Label>("HBoxContainer/VBoxContainer/TargetPanel/VBoxContainer/Description");
        targetPortrait = GetNode<Portrait>("HBoxContainer/VBoxContainer/TargetPanel/VBoxContainer/HBoxContainer/Portrait");
        // Copy paste again!
        using var file = FileAccess.Open("res://Letters/Letter" + (CurrentLetter + 1) + ".txt", FileAccess.ModeFlags.Read);
        string content = file.GetAsText();
        letterText.Text = string.Join("\n", content.GetLineParts(0));
        List<string> targetLines = content.GetLineParts(1);
        targetPortrait.Load(targetName.Text = targetLines[0]);
        targetLines.RemoveAt(0);
        GD.Print(string.Join("\n", targetLines));
        targetDescription.Text = string.Join("\n", targetLines);
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

    public void GoToBattle()
    {
        Main.Current.ToBattle(CurrentLetter, messengerSelect.SelectedMessenger);
        QueueFree();
    }
}
