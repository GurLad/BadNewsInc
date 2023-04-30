using Godot;
using System;

public partial class SpeakingPortrait : PanelContainer // Tons of copy-past from preparation...
{
    [Export]
    public bool Left;
    public string VASuffix = "";
    public int CurrentLetter;
    public bool Idle => !player.Playing && postSpeakDelay.TimeLeft <= 0 && beganDelay;
    private string portraitName = "Palla";
    public string PortraitName
    {
        get => portraitName;
        set
        {
            portraitName = value;
            portrait.Load(FullName);
        }
    }
    private string FullName => PortraitName.Replace("|", "");
    private string VAName => PortraitName.Contains("|") ? PortraitName.Substring(0, PortraitName.IndexOf("|")) : PortraitName;
    private Portrait portrait;
    private Label text;
    private AudioStreamPlayer player;
    private Timer postSpeakDelay;
    private Timer midSpeakAnimator;
    private bool beganDelay = true;
    private int currentPlaying = -1;
    private bool currentMouthMode1;

    public override void _Ready()
    {
        base._Ready();
        portrait = GetNode<Portrait>("HBoxContainer/Portrait" + (Left ? "Left" : "Right"));
        text = GetNode<Label>("HBoxContainer/Text");
        player = GetNode<AudioStreamPlayer>("AudioPlayer");
        postSpeakDelay = GetNode<Timer>("Post");
        midSpeakAnimator = GetNode<Timer>("Mid");
        portrait.Visible = true;
        portrait.Load(FullName);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (!player.Playing && currentPlaying >= 0)
        {
            if (!beganDelay)
            {
                beganDelay = true;
                portrait.Load(FullName);
                midSpeakAnimator.Stop();
                postSpeakDelay.Start();
                return;
            }
        }
        else if (currentPlaying >= 0)
        {
            if (midSpeakAnimator.TimeLeft <= 0)
            {
                currentMouthMode1 = !currentMouthMode1;
                portrait.Load(FullName + (currentMouthMode1 ? "1" : "2"));
                midSpeakAnimator.Start();
            }
        }
    }

    public void PlayNext(string newPortrait, string newText)
    {
        PortraitName = newPortrait;
        text.Text = newText;
        currentPlaying = Mathf.Max(currentPlaying, 0);
        player.Stream = VoiceActingController.GetVA(CurrentLetter, VAName + VASuffix + ++currentPlaying);
        GD.Print("Getting " + (VAName + VASuffix + currentPlaying));
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

    public void Stop()
    {
        player.Stop();
        midSpeakAnimator.Stop();
        beganDelay = true;
        portrait.Load(FullName);
    }
}
