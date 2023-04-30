using Godot;
using System;

public partial class MainMenu : Control
{
    public void Start()
    {
        Main.Current.ToIntro();
        QueueFree();
    }

    public void Fullscreen()
    {
        DisplayServer.WindowSetMode(DisplayServer.WindowGetMode() == DisplayServer.WindowMode.ExclusiveFullscreen ? DisplayServer.WindowMode.Windowed : DisplayServer.WindowMode.ExclusiveFullscreen);
    }

    public void Quit()
    {
        GetTree().Quit();
    }
}
