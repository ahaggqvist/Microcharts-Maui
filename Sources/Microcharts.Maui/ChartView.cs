// Copyright (c) Morgan SOULLEZ. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace Microcharts.Maui;

public class ChartView : SKCanvasView
{
    public static readonly BindableProperty ChartProperty = BindableProperty.Create(nameof(Chart), typeof(Chart), typeof(ChartView), null, propertyChanged: OnChartChanged);

    private Chart _chart;

    private InvalidatedWeakEventHandler<ChartView> _handler;

    public ChartView()
    {
        BackgroundColor = Colors.Transparent;
        PaintSurface += OnPaintCanvas;
    }

    public Chart Chart
    {
        get => (Chart)GetValue(ChartProperty);
        set => SetValue(ChartProperty, value);
    }

    private static void OnChartChanged(BindableObject d, object oldValue, object value)
    {
        var view = d as ChartView;

        if (view?._chart != null)
        {
            view._handler.Dispose();
            view._handler = null;
        }

        if (view == null)
        {
            return;
        }

        view._chart = value as Chart;
        view.InvalidateSurface();

        if (view._chart != null)
        {
            view._handler = view._chart.ObserveInvalidate(view, v => v.InvalidateSurface());
        }
    }

    private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
    {
        if (_chart != null)
        {
            _chart.Draw(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        }
        else
        {
            e.Surface.Canvas.Clear(SKColors.Transparent);
        }
    }
}