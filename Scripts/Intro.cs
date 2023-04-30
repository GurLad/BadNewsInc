using Godot;
using System;
using System.Collections.Generic;

public partial class Intro : Control
{
    private SpeakingPortrait vance;
    private List<string> lines;
    private int currentLine = 0;

    public override void _Ready()
    {
        base._Ready();
        vance = GetNode<SpeakingPortrait>("VBoxContainer/CenterContainer");
        vance.PortraitName = "Vance";
        vance.CurrentLetter = 0;
        using var file = FileAccess.Open("res://Letters/Intro.txt", FileAccess.ModeFlags.Read);
        string content = file.GetAsText();
        lines = content.GetLineParts(0);
        PlayNextLine();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (vance.Idle)
        {
            PlayNextLine();
        }
    }

    private void PlayNextLine()
    {
        if (currentLine >= lines.Count)
        {
            Skip();
            return;
        }
        string targetLine = lines[currentLine++];
        GD.Print("Line is " + targetLine);
        string[] parts = targetLine.Split(":");
        vance.PlayNext(parts[0], targetLine.Substring(targetLine.IndexOf(":") + 1).Trim());
    }

    public void Skip()
    {
        Main.Current.ToPreperations(0, "Day 1");
        QueueFree();
    }
}
