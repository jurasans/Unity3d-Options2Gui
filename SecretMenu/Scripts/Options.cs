//-----------------------------------------------------------------------
// company="Visualed ltd">
//     Copyright (c) Visualed ltd All rights reserved.
// Author : ilia gandelman
//-----------------------------------------------------------------------
using System;

public enum FullScreen { Yes, No, Windowed }

[Serializable]
public class Options
{
    public bool Resizable { get; set; }
    public string Name { get; set; }
    public int Number { get; set; }
    public int Number1 { get; set; }
    public int Number2 { get; set; }
    public int Number3 { get; set; }
    public int[] Actors { get; set; }

    [EnableBrowseFile]
    [NeedRestartAfterChange]
    public string ExePath { get; set; }

    public FullScreen DisplayMode { get; set; }
}

public class EnableBrowseFileAttribute : Attribute
{
}

public class NeedRestartAfterChange : Attribute
{
}