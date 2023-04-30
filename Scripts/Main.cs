using Godot;
using System;

public partial class Main : Node
{
    [Export]
    public PackedScene PreperationsScene;
    [Export]
    public PackedScene BattleScene;
    [Export]
    public PackedScene Transition;

    public override void _Ready()
    {
        base._Ready();
        PreperationsScene.Instantiate(); // TBA: switch with menu
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
        };
    }

    public void ToPreperations(int letter, string display)
    {
        Transition newTransition = Transition.Instantiate<Transition>();
        newTransition.Display = display;
        newTransition.PostAction = () =>
        {
            Preparations preparations = PreperationsScene.Instantiate<Preparations>();
            preparations.CurrentLetter = letter;
        };
    }
}
