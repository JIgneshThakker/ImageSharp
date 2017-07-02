﻿// <copyright file="AutoOrient.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using ImageSharp.PixelFormats;

    using ImageSharp.Processing;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Adjusts an image so that its orientation is suitable for viewing. Adjustments are based on EXIF metadata embedded in the image.
        /// </summary>
        /// <param name="source">The image to auto rotate.</param>
        /// <returns>The <see cref="Image{TPixel}"/></returns>
        public static IImageOperations AutoOrient(this IImageOperations source)
            => source.ApplyProcessor(new Processing.Processors.AutoRotateProcessor());
    }
}