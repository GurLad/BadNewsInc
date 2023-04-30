using Godot;
using System;

public partial class MainMenu : Control
{
    public void Start()
    {
        Main.Current.ToPreperations(0, "Day 1");
        QueueFree();
    }

    public void Fullscreen()
    {
        DisplayServer.WindowSetMode(DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen ? DisplayServer.WindowMode.Windowed : DisplayServer.WindowMode.Fullscreen);
    }

    public void Quit()
    {
        GetTree().Quit();
    }
}
