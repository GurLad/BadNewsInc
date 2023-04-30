using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class MessengerSelect : PanelContainer
{
    [Export]
    public Godot.Collections.Array<NodePath> ButtonsPath;
    public int SelectedMessenger = -1;
    private List<MessengerButton> buttons;
    private Label description;
    private Button sendButton;

    public override void _Ready()
    {
        base._Ready();
        description = GetNode<Label>("VBoxContainer/Description");
        sendButton = GetNode<Button>("../SendPanel/HBoxContainer/Send");
        buttons = ButtonsPath.ToList().ConvertAll(a => GetNode<MessengerButton>(a));
        buttons.ForEach(a => a.Button.Pressed += () => SetMessenger(a));
    }

    public void SetMessenger(MessengerButton messengerButton)
    {
        description.Text = messengerButton.Description;
        SelectedMessenger = messengerButton.PortraitName.MessengerNameToInt();
        buttons.ForEach(a => a.Button.Disabled = false);
        messengerButton.Button.Disabled = true;
        sendButton.Disabled = false;
    }
}
