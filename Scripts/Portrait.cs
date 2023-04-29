using Godot;
using System;

public partial class Portrait : TextureRect
{
    [Export]
    public string DefaultName;
    private TextureRect holder;

    public override void _Ready()
    {
        base._Ready();
        holder = GetNode<TextureRect>("Holder");
        Load(DefaultName);
    }

    public void Load(string name)
    {
        holder.Texture = PortraitController.Portraits[name];
    }
}
