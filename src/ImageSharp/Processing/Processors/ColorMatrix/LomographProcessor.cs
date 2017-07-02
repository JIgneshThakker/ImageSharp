﻿// <copyright file="LomographProcessor.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Processing.Processors
{
    using System;
    using System.Numerics;

    using ImageSharp.PixelFormats;
    using SixLabors.Primitives;

    /// <summary>
    /// Converts the colors of the image recreating an old Lomograph effect.
    /// </summary>
    internal class LomographProcessor : ColorMatrixProcessor
    {
        private static readonly Color VeryDarkGreen = new Color(0, 10, 0, 255);
        private GraphicsOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="LomographProcessor" /> class.
        /// </summary>
        /// <param name="options">The options effecting blending and composition.</param>
        public LomographProcessor(GraphicsOptions options)
        {
            this.options = options;
        }

        /// <inheritdoc/>
        public override Matrix4x4 Matrix => new Matrix4x4()
        {
            M11 = 1.5F,
            M22 = 1.45F,
            M33 = 1.11F,
            M41 = -.1F,
            M42 = .0F,
            M43 = -.08F,
            M44 = 1
        };

        /// <inheritdoc/>
        protected override void AfterApply<TPixel>(ImageBase<TPixel> source, Rectangle sourceRectangle)
        {
            new VignetteProcessor(VeryDarkGreen, this.options).Apply(source, sourceRectangle);
        }
    }
}