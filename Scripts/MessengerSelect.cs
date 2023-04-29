using Godot;
using System;
using System.Linq;

public partial class MessengerSelect : PanelContainer
{
    [Export]
    public Godot.Collections.Array<NodePath> Buttons;
    public int SelectedMessenger = -1;
    private Label description;

    public override void _Ready()
    {
        base._Ready();
        description = GetNode<Label>("VBoxContainer/Description");
        Buttons.ToList().ForEach(a => GetNode<MessengerButton>(a).Button.Pressed += () => SetMessenger(GetNode<MessengerButton>(a)));
    }

    public void SetMessenger(MessengerButton messengerButton)
    {
        description.Text = messengerButton.Description;
        SelectedMessenger = messengerButton.PortraitName.MessengerNameToInt();
    }
}
