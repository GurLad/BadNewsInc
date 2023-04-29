using Godot;
using Godot.Collections;
using System;

public partial class PortraitController : Node
{
    public static Dictionary<string, Texture2D> Portraits { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        Portraits = new Dictionary<string, Texture2D>();
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
                    Texture2D texture = ResourceLoader.Load<CompressedTexture2D>("res://AllPortraits/" + fileName);
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
