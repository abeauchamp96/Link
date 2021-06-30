// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using FluentAssertions;
using Link.Discord.Utility;
using Xunit;

namespace Link.Discord.Commands.Misc
{
    public class MiscCommandsTests
    {
        private readonly IMiscCommands miscCommands;

        public MiscCommandsTests()
        {
            this.miscCommands = new MiscCommands();
        }

        [Fact]
        public void Echo_ShouldReturnAnErrorMessage_WhenMessageIsEmpty()
        {
            var repeatedMessage = this.miscCommands.Echo(string.Empty);

            repeatedMessage.Should().Be($"Where the message? {EmojiUnicode.Thinking}");
        }

        [Fact]
        public void Echo_ShouldReturnTheMessage_WhenTheMessageIsNotEmpty()
        {
            const string message = "Hola, World!";

            var repeatedMessage = this.miscCommands.Echo(message);

            repeatedMessage.Should().Be($"{Unicode.ZeroWidthSpace}{message}");
        }

        [Fact]
        public void Say_ShouldReturnAnErrorMessage_WhenMessageIsEmpty()
        {
            var repeatedMessage = this.miscCommands.Say(string.Empty);

            repeatedMessage.Should().Be($"What are you trying to say? {EmojiUnicode.Thinking}");
        }

        [Fact]
        public void Say_ShouldReturnTheMessage_WhenTheMessageIsNotEmpty()
        {
            const string message = "Hola, World!";

            var repeatedMessage = this.miscCommands.Say(message);

            repeatedMessage.Should().Be($"{Unicode.ZeroWidthSpace}{message}");
        }
    }
}
