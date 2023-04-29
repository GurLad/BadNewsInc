using Godot;
using Godot.Collections;
using System;

public partial class PortraitController : Node
{
    [Export]
    public Dictionary<string, Texture> Portraits;

    public override void _Ready()
    {
        base._Ready();
        Portraits = new Dictionary<string, Texture>();
        using var dir = DirAccess.Open("res://AllPortraits");
        if (dir != null)
        {
            dir.ListDirBegin();
            string fileName = dir.GetNext();
            while (fileName != "")
            {
                if (!dir.CurrentIsDir() && !fileName.Contains(".import"))
                {
                    string name = fileName.Substring(0, fileName.LastIndexOf("."));
                    GD.Print(name);
                    Image image = new Image();
                    image.Load(fileName);
                    ImageTexture texture = ImageTexture.CreateFromImage(image);
                    Portraits.Add(name, texture);
                }
                fileName = dir.GetNext();
            }
        }
        else
        {
            GD.Print("An error occurred when trying to access the path.");
        }
    }
}
