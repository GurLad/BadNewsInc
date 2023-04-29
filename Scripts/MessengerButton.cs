using Godot;
using System;

public partial class MessengerButton : VBoxContainer
{
    [Export]
    public string PortraitName;
    [Export(PropertyHint.MultilineText)]
    public string Description;
    public TextureButton Button;
    private TextureRect holder;
    private Label nameLabel;

    public override void _Ready()
    {
        base._Ready();
        Button = GetNode<TextureButton>("Button");
        holder = GetNode<TextureRect>("Button/Holder");
        nameLabel = GetNode<Label>("Name");
        holder.Texture = PortraitController.Portraits[PortraitName];
        nameLabel.Text = PortraitName;
    }
}
