using Godot;
using System;

public partial class Transition : Control
{
    private enum State { PreText, Text, PostText }
    public string Display;
    public Action PostAction;
    private Label text;
    private Timer prePostTimer;
    private Timer textTimer;
    private State state = State.PreText;

    public override void _Ready()
    {
        base._Ready();
        text = GetNode<Label>("CenterContainer/Text");
        prePostTimer = GetNode<Timer>("PrePostTimer");
        textTimer = GetNode<Timer>("TextTimer");
        prePostTimer.Start();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        switch (state)
        {
            case State.PreText:
                if (prePostTimer.TimeLeft <= 0)
                {
                    text.Text = Display;
                    // TBA play SFX
                    textTimer.Start();
                    state = State.Text;
                }
                break;
            case State.Text:
                if (textTimer.TimeLeft <= 0)
                {
                    text.Text = "";
                    // TBA play SFX
                    prePostTimer.Start();
                    state = State.PostText;
                }
                break;
            case State.PostText:
                if (prePostTimer.TimeLeft <= 0)
                {
                    PostAction();
                    QueueFree();
                }
                break;
            default:
                break;
        }
    }
}
