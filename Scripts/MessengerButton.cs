using Godot;
using System;

public partial class MessengerButton : VBoxContainer
{
    [Export]
    public string PortraitName;
    [Export(PropertyHint.MultilineText)]
    public string Description;
    private TextureRect holder;
    private Label nameLabel;

    public override void _Ready()
    {
        base._Ready();
        holder = GetNode<TextureRect>("Button/Holder");
        nameLabel = GetNode<Label>("Name");
        holder.Texture = PortraitController.Portraits[PortraitName];
        nameLabel.Text = PortraitName;
    }
}
