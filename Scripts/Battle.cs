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
        messenger = GetNode<SpeakingPortrait>("Control/VBoxContainer/Messenger");
        target = GetNode<SpeakingPortrait>("Control/VBoxContainer/Target");
        messenger.PortraitName = CurrentMessenger.IntToMessengerName();
        target.PortraitName = CurrentLetter switch // Hardcoding FTW
        {
            0 => "Esther",
            _ => "Palla"
        };
        target.VASuffix = CurrentMessenger.IntToMessengerName();
        using var file = FileAccess.Open("res://Letters/Letter" + (CurrentLetter + 1) + ".txt", FileAccess.ModeFlags.Read);
        string content = file.GetAsText();
        lines = content.GetLineParts(2 + CurrentMessenger);
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
        GD.Print("Line is " + targetLine);
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
                messenger.PlayNext(parts[0], targetLine.Substring(targetLine.IndexOf(":") + 1));
            }
            else
            {
                target.PlayNext(parts[0], targetLine.Substring(targetLine.IndexOf(":") + 1));
            }
        }
    }
}
