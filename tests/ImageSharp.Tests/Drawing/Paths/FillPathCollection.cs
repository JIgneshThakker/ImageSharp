﻿
namespace ImageSharp.Tests.Drawing.Paths
{
    using System;
    using ImageSharp;
    using ImageSharp.Drawing.Brushes;
    using Xunit;
    using ImageSharp.Drawing;
    using System.Numerics;
    using SixLabors.Shapes;
    using ImageSharp.Drawing.Processors;
    using ImageSharp.PixelFormats;

    public class FillPathCollection : IDisposable
    {
        GraphicsOptions noneDefault = new GraphicsOptions();
        Color color = Color.HotPink;
        SolidBrush brush = Brushes.Solid(Color.HotPink);
        IPath path1 = new SixLabors.Shapes.Path(new LinearLineSegment(new SixLabors.Primitives.PointF[] {
                    new Vector2(10,10),
                    new Vector2(20,10),
                    new Vector2(20,10),
                    new Vector2(30,10),
                }));
        IPath path2 = new SixLabors.Shapes.Path(new LinearLineSegment(new SixLabors.Primitives.PointF[] {
                    new Vector2(10,10),
                    new Vector2(20,10),
                    new Vector2(20,10),
                    new Vector2(30,10),
                }));

        IPathCollection pathCollection;

        private ProcessorWatchingImage img;

        public FillPathCollection()
        {
            this.pathCollection = new PathCollection(path1, path2);
            this.img = new ProcessorWatchingImage(10, 10);
        }

        public void Dispose()
        {
            img.Dispose();
        }

        [Fact]
        public void CorrectlySetsBrushAndPath()
        {
            img.Mutate(x => x.Fill(brush, pathCollection));

            Assert.Equal(2, img.ProcessorApplications.Count);
            for (var i = 0; i < 2; i++)
            {
                FillRegionProcessor processor = Assert.IsType<FillRegionProcessor>(img.ProcessorApplications[i].processor);

                Assert.Equal(GraphicsOptions.Default, processor.Options);

                ShapeRegion region = Assert.IsType<ShapeRegion>(processor.Region);

                // path is converted to a polygon before filling
                Polygon polygon = Assert.IsType<Polygon>(region.Shape);
                LinearLineSegment segments = Assert.IsType<LinearLineSegment>(polygon.LineSegments[0]);

                Assert.Equal(brush, processor.Brush);
            }
        }

        [Fact]
        public void CorrectlySetsBrushPathOptions()
        {
            img.Mutate(x => x.Fill(brush, pathCollection, noneDefault));

            Assert.Equal(2, img.ProcessorApplications.Count);
            for (var i = 0; i < 2; i++)
            {
                FillRegionProcessor processor = Assert.IsType<FillRegionProcessor>(img.ProcessorApplications[i].processor);

                Assert.Equal(noneDefault, processor.Options);

                ShapeRegion region = Assert.IsType<ShapeRegion>(processor.Region);
                Polygon polygon = Assert.IsType<Polygon>(region.Shape);
                LinearLineSegment segments = Assert.IsType<LinearLineSegment>(polygon.LineSegments[0]);

                Assert.Equal(brush, processor.Brush);
            }
        }

        [Fact]
        public void CorrectlySetsColorAndPath()
        {
            img.Mutate(x => x.Fill(color, pathCollection));

            Assert.Equal(2, img.ProcessorApplications.Count);
            for (var i = 0; i < 2; i++)
            {
                FillRegionProcessor processor = Assert.IsType<FillRegionProcessor>(img.ProcessorApplications[i].processor);

                Assert.Equal(GraphicsOptions.Default, processor.Options);

                ShapeRegion region = Assert.IsType<ShapeRegion>(processor.Region);
                Polygon polygon = Assert.IsType<Polygon>(region.Shape);
                LinearLineSegment segments = Assert.IsType<LinearLineSegment>(polygon.LineSegments[0]);

                SolidBrush brush = Assert.IsType<SolidBrush>(processor.Brush);
                Assert.Equal(color, brush.Color);
            }
        }

        [Fact]
        public void CorrectlySetsColorPathAndOptions()
        {
            img.Mutate(x => x.Fill(color, pathCollection, noneDefault));

            Assert.Equal(2, img.ProcessorApplications.Count);
            for (var i = 0; i < 2; i++)
            {
                FillRegionProcessor processor = Assert.IsType<FillRegionProcessor>(img.ProcessorApplications[i].processor);

                Assert.Equal(noneDefault, processor.Options);

                ShapeRegion region = Assert.IsType<ShapeRegion>(processor.Region);
                Polygon polygon = Assert.IsType<Polygon>(region.Shape);
                LinearLineSegment segments = Assert.IsType<LinearLineSegment>(polygon.LineSegments[0]);

                SolidBrush brush = Assert.IsType<SolidBrush>(processor.Brush);
                Assert.Equal(color, brush.Color);
            }
        }
    }
}
