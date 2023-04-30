using Godot;
using System;

public partial class Main : Node
{
    private const string PATH = "user://finishedGame.save";
    public static Main Current;
    [Export]
    public PackedScene PreperationsScene;
    [Export]
    public PackedScene BattleScene;
    [Export]
    public PackedScene MenuScene;
    [Export]
    public PackedScene IntroScene;
    [Export]
    public PackedScene Transition;
    [Export]
    public Godot.Collections.Dictionary<string, AudioStream> SFX;
    [Export]
    public Godot.Collections.Dictionary<string, AudioStream> Musics;
    private AudioStreamPlayer sfxPlayer;
    private AudioStreamPlayer musicPlayer;

    public override void _Ready()
    {
        base._Ready();
        sfxPlayer = GetNode<AudioStreamPlayer>("SFXPlayer");
        musicPlayer = GetNode<AudioStreamPlayer>("MusicPlayer");
        Current = this;
        ToMenu();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey eventKey)
        {
            if (eventKey.Pressed && eventKey.Keycode == Key.F11)
            {
                DisplayServer.WindowSetMode(DisplayServer.WindowGetMode() == DisplayServer.WindowMode.ExclusiveFullscreen ? DisplayServer.WindowMode.Windowed : DisplayServer.WindowMode.ExclusiveFullscreen);
            }
        }
    }

    public void ToBattle(int letter, int messenger)
    {
        Transition newTransition = Transition.Instantiate<Transition>();
        newTransition.Display = messenger.IntToMessengerName() + " arrives";
        newTransition.PostAction = () =>
        {
            Battle battle = BattleScene.Instantiate<Battle>();
            battle.CurrentLetter = letter;
            battle.CurrentMessenger = messenger;
            AddChild(battle);
        };
        AddChild(newTransition);
    }

    public void ToPreperations(int letter, string display)
    {
        Transition newTransition = Transition.Instantiate<Transition>();
        newTransition.Display = display;
        newTransition.PostAction = () =>
        {
            Preparations preparations = PreperationsScene.Instantiate<Preparations>();
            preparations.CurrentLetter = letter;
            AddChild(preparations);
        };
        AddChild(newTransition);
    }

    public void ToMenu()
    {
        AddChild(MenuScene.Instantiate<MainMenu>());
    }

    public void ToIntro()
    {
        Transition newTransition = Transition.Instantiate<Transition>();
        newTransition.Display = "You arrive";
        newTransition.PostAction = () =>
        {
            AddChild(IntroScene.Instantiate<Intro>());
        };
        AddChild(newTransition);
    }

    public void ToFinishGame()
    {
        // Save
        var file = FileAccess.Open(PATH, FileAccess.ModeFlags.Write);
        file.StoreString("Yay random person beat the game, congrats");
        // Transition
        Transition newTransition = Transition.Instantiate<Transition>();
        newTransition.Display = "Fin";
        newTransition.PostAction = () =>
        {
            AddChild(MenuScene.Instantiate<MainMenu>());
        };
        AddChild(newTransition);
    }

    public void PlaySFX(string name)
    {
        //GD.Print(name + ", there are " + string.Join(",", SFX.Keys));
        sfxPlayer.Stream = SFX[name];
        sfxPlayer.Play();
    }

    public bool PlayingSFX()
    {
        return sfxPlayer.Playing;
    }

    public void PlayMusic(string name)
    {
        musicPlayer.Stream = Musics[name];
        musicPlayer.Play();
    }
}
