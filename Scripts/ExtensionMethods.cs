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
}
