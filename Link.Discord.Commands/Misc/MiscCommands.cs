// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Discord.Utility;

namespace Link.Discord.Commands.Misc
{
    internal sealed class MiscCommands : IMiscCommands
    {
        public string Echo(string? message)
        {
            if (string.IsNullOrEmpty(message))
                return $"Where the message? {EmojiUnicode.Thinking}";

            return message;
        }

        public string Say(string? message)
        {
            if (string.IsNullOrEmpty(message))
                return $"What are you trying to say? {EmojiUnicode.Thinking}";

            return message;
        }
    }
}
