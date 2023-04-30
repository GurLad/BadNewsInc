using Godot;
using System;
using System.Collections.Generic;

public partial class VoiceActingController : Node
{
    private const int AMOUNT_OF_LEVELS = 4;
    private static List<Dictionary<string, AudioStream>> VAFiles;

    public override void _Ready()
    {
        base._Ready();
        VAFiles = new List<Dictionary<string, AudioStream>>();
        for (int i = 0; i < AMOUNT_OF_LEVELS; i++)
        {
            VAFiles.Add(new Dictionary<string, AudioStream>());
            using var dir = DirAccess.Open("res://VA/Letter" + (i + 1));
            if (dir != null)
            {
                dir.ListDirBegin();
                string fileName = dir.GetNext();
                while (fileName != "")
                {
                    if (dir.CurrentIsDir())
                    {
                        //GD.Print("Entering " + fileName);
                        LoadVAFolder(i, "res://VA/Letter" + (i + 1) + "/" + fileName);
                    }
                    fileName = dir.GetNext();
                }
            }
            else
            {
                GD.Print("An error occurred when trying to access the path.");
            }
        }
        //GD.Print("Success!");
    }

    private void LoadVAFolder(int letter, string path)
    {
        using var dir = DirAccess.Open(path);
        if (dir != null)
        {
            dir.ListDirBegin();
            string fileName = dir.GetNext();
            while (fileName != "")
            {
                if (!dir.CurrentIsDir() && !fileName.Contains(".import"))
                {
                    string name = fileName.Substring(0, fileName.LastIndexOf("."));
                    //GD.Print(name);
                    AudioStream audioStream = ResourceLoader.Load<AudioStream>(path + "/" + fileName);
                    VAFiles[letter].Add(name, audioStream);
                }
                fileName = dir.GetNext();
            }
        }
        else
        {
            GD.Print("An error occurred when trying to access the path.");
        }
    }

    public static AudioStream GetVA(int letter, string id)
    {
        //GD.Print("Got letter " + letter + " and id " + id + ", has " + string.Join(", ", VAFiles[letter].Keys));
        return VAFiles[letter].ContainsKey(id) ? VAFiles[letter][id] : null;
    }
}
