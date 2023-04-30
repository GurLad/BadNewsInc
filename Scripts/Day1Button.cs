using Godot;
using System;

public partial class Day1Button : Button // Terrible code galore!!!! :)))) it's almost 1 am
{
    private const string PATH = "user://finishedGame.save";
    [Export]
    public int LetterNumber = 0;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (!FileAccess.FileExists(PATH))
        {
            QueueFree();
        }
    }

    public void Click()
    {
        Main.Current.ToPreperations(LetterNumber, "Day " + (LetterNumber + 1));
        GetParent().GetParent().GetParent().QueueFree();
    }
}
