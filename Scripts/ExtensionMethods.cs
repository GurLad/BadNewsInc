using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public static class ExtensionMethods
{
    public static int MessengerNameToInt(this string name)
    {
        return name switch
        {
            "Luke" => 0,
            "Flake" => 1,
            "Palla" => 2,
            _ => -1
        };
    }

    public static string IntToMessengerName(this int i)
    {
        return i switch
        {
            0 => "Luke",
            1 => "Flake",
            2 => "Palla",
            _ => "whaterrorerror"
        };
    }

    public static List<string> GetLineParts(this string source, int part)
    {
        string[] mainParts = source.Split('~');
        List<string> finalParts = mainParts[part].Replace("\r", "").Split('\n').ToList();
        finalParts = finalParts.Where(a => a.Length > 0).ToList();
        return finalParts;
    }
}
