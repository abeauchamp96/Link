// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using FluentAssertions;
using Link.Discord.Commands.Helpers;
using Link.Discord.Utility;
using Moq;
using Xunit;

namespace Link.Discord.Commands.Misc
{
    public class MiscCommandsTests
    {
        private readonly Mock<IRandomHelper> randomHelperMock = new();

        private readonly IMiscCommands miscCommands;

        public MiscCommandsTests()
        {
            this.miscCommands = new MiscCommands(this.randomHelperMock.Object);
        }

        [Fact]
        public void Echo_ShouldReturnAnErrorMessage_WhenMessageIsEmpty()
        {
            var repeatedMessage = this.miscCommands.Echo(string.Empty);

            repeatedMessage.Should().Be($"Where the message? {EmojiUnicode.Thinking}");
        }

        [Fact]
        public void Echo_ShouldReturnAnErrorMessage_WhenMessageOnlyHaveWhiteSpace()
        {
            var repeatedMessage = this.miscCommands.Echo(" ");

            repeatedMessage.Should().Be($"Where the message? {EmojiUnicode.Thinking}");
        }

        [Fact]
        public void Echo_ShouldReturnTheMessage_WhenTheMessageIsNotEmpty()
        {
            const string message = "Hola, World!";

            var repeatedMessage = this.miscCommands.Echo(message);

            repeatedMessage.Should().Be(message);
        }

        [Fact]
        public void Flip_ShouldReturnHeads_WhenTheGeneratedValueIsZero()
        {
            this.randomHelperMock.Setup(r => r.Generate(0, 2)).Returns(0);

            var message = this.miscCommands.Flip();

            message.Should().Be("You flipped a coin: heads");
        }

        [Fact]
        public void Flip_ShouldReturnTails_WhenTheGeneratedValueIsNotZero()
        {
            this.randomHelperMock.Setup(r => r.Generate(0, 2)).Returns(1);

            var message = this.miscCommands.Flip();

            message.Should().Be("You flipped a coin: tails");
        }
    }
}
