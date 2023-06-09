﻿using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Diagram
{
    public class DynamicPolyline : Shape
    {
        public static readonly DirectProperty<DynamicPolyline, AvaloniaList<Point>> PointsProperty =
            AvaloniaProperty.RegisterDirect<DynamicPolyline, AvaloniaList<Point>>(nameof(Points),
                (e) => e.Points);

        public static readonly DirectProperty<DynamicPolyline, Guid> PointsGuidProperty =
            AvaloniaProperty.RegisterDirect<DynamicPolyline, Guid>(nameof(PointsGuid),
                (e) => e.PointsGuid,
                (e, v) => e.PointsGuid = v);

        private Guid pointsGuid = Guid.Empty;

        public AvaloniaList<Point> Points { get; } = new();

        public Guid PointsGuid
        {
            get => pointsGuid;
            set => SetAndRaise(PointsGuidProperty, ref pointsGuid, value);
        }

        static DynamicPolyline()
        {
            StrokeThicknessProperty.OverrideDefaultValue<DynamicPolyline>(1);
            AffectsGeometry<DynamicPolyline>(PointsGuidProperty);
        }

        public DynamicPolyline()
        {
            Points.CollectionChanged += OnPointsChanged;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            // Fix the bug when stroke thickness > 1, the width of lines on edge is cutted
            // make a confusing visual
            Size baseSize = base.MeasureOverride(availableSize);
            return baseSize.Inflate(new Thickness(StrokeThickness));
        }

        private void OnPointsChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Update the points guid to let the geometry renderer update
            PointsGuid = Guid.NewGuid();
        }

        protected override Geometry CreateDefiningGeometry()
            => new PolylineGeometry(Points, false);
    }
}
