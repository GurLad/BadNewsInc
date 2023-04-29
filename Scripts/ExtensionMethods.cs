using Godot;
using System;

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
}
