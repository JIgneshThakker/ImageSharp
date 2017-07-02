// <copyright file="EntropyCrop.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;

    using ImageSharp.PixelFormats;

    using Processing.Processors;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Crops an image to the area of greatest entropy.
        /// </summary>
        /// <param name="source">The image to crop.</param>
        /// <param name="threshold">The threshold for entropic density.</param>
        /// <returns>The <see cref="Image{TPixel}"/></returns>
        public static IImageOperations EntropyCrop(this IImageOperations source, float threshold = .5f)
        => source.ApplyProcessor(new EntropyCropProcessor(threshold));
    }
}