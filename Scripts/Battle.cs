using Godot;
using System;
using System.Collections.Generic;

public partial class Battle : Node
{
    [Export] // For debug
    public int CurrentLetter = 0;
    [Export] // For debug
    public int CurrentMessenger;
    private SpeakingPortrait messenger;
    private SpeakingPortrait target;
    private List<string> lines;
    private int currentLine = 0;

    public override void _Ready()
    {
        base._Ready();
        messenger = GetNode<SpeakingPortrait>("VBoxContainer/Messenger");
        target = GetNode<SpeakingPortrait>("VBoxContainer/Target");
        target.VASuffix = CurrentMessenger.IntToMessengerName();
        lines = ResourceLoader.Load<string>("res://Letters/Letter" + (CurrentLetter + 1) + ".txt").GetLineParts(2 + CurrentMessenger);
        PlayNextLine();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (messenger.Idle && target.Idle)
        {
            PlayNextLine();
        }
    }

    private void PlayNextLine()
    {
        if (currentLine >= lines.Count)
        {
            // Go to prep TBA
            QueueFree();
        }
        string targetLine = lines[currentLine++];
        if (targetLine == "DEATH")
        {
            // Go to game over TBA
            QueueFree();
        }
        else
        {
            string[] parts = targetLine.Split(":");
            if (parts[0].MessengerNameToInt() >= 0)
            {
                target.PlayNext(parts[0], targetLine.Substring(targetLine.IndexOf(":") + 1));
            }
            else
            {
                messenger.PlayNext(parts[1], targetLine.Substring(targetLine.IndexOf(":") + 1));
            }
        }
    }
}
