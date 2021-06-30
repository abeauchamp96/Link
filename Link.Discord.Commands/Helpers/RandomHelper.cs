// Copyright (c) Alexandre Beauchamp. All rights reserved.
// Licensed under the MIT license.

using System;

namespace Link.Discord.Commands.Helpers
{
    internal sealed class RandomHelper : IRandomHelper
    {
        private readonly Random random;

        public RandomHelper() => this.random = new Random();

        public int Generate(int min, int max)
            => this.random.Next(min, max);
    }
}
