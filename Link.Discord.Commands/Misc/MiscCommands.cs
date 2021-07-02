// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using Link.Discord.Commands.Helpers;
using Link.Discord.Utility;

namespace Link.Discord.Commands.Misc
{
    internal sealed class MiscCommands : IMiscCommands
    {
        private readonly IRandomHelper randomHelper;

        public MiscCommands(IRandomHelper randomHelper)
            => this.randomHelper = randomHelper;

        public string Echo(string? message)
        {
            if (string.IsNullOrEmpty(message))
                return $"Where the message? {EmojiUnicode.Thinking}";

            return $"{Unicode.ZeroWidthSpace}{message}";
        }

        public string Say(string? message)
        {
            if (string.IsNullOrEmpty(message))
                return $"What are you trying to say? {EmojiUnicode.Thinking}";

            return $"{Unicode.ZeroWidthSpace}{message}";
        }

        public string Flip()
        {
            var flippedCoin = this.randomHelper.Generate(0, 2) == 0
                ? "heads"
                : "tails";

            return $"You flipped a coin: {flippedCoin}";
        }

        public string Lenny() => "( ͡° ͜ʖ ͡°)";

        public string Running() => "ᕕ( ᐛ )ᕗ";
    }
}
