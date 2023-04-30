using Godot;
using System;

public partial class Main : Node
{
    public static Main Current;
    [Export]
    public PackedScene PreperationsScene;
    [Export]
    public PackedScene BattleScene;
    [Export]
    public PackedScene Transition;

    public override void _Ready()
    {
        base._Ready();
        Current = this;
        ToPreperations(0, "Day 1");
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
}
