// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

namespace Link.Discord.Commands.Misc
{
    public interface IMiscCommands
    {
        string Echo(string? message);
        string Say(string? message);
        string Flip();
        string Lenny();
    }
}
