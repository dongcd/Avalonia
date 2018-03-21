// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;

namespace Avalonia.Animation
{
    /// <summary>
    /// Eases out a <see cref="double"/> value 
    /// using the quarter-wave of sine function
    /// with shifted phase.
    /// </summary>
    public class SineEaseOut : Easing
    {
        /// <inheritdoc/>
        public override double Ease(double progress)
        {
            return Math.Sin(progress * EasingConstants.HALFPI);
        }
    }
}
