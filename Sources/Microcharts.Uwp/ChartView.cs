// Copyright (c) Aloïs DENIEL. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Windows.UI.Xaml;
using SkiaSharp;
using SkiaSharp.Views.UWP;

namespace Microcharts.Uwp;

public class ChartView : SKXamlCanvas
{
    public static readonly DependencyProperty ChartProperty =
        DependencyProperty.Register(nameof(Chart), typeof(Chart), typeof(ChartView), new PropertyMetadata(null, OnChartChanged));

    private Chart chart;

    private InvalidatedWeakEventHandler<ChartView> handler;

    public ChartView()
    {
        PaintSurface += OnPaintCanvas;
    }

    public Chart Chart
    {
        get => (Chart)GetValue(ChartProperty);
        set => SetValue(ChartProperty, value);
    }

    private static void OnChartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var view = d as ChartView;

        if (view.chart != null)
        {
            view.handler.Dispose();
            view.handler = null;
        }

        view.chart = e.NewValue as Chart;
        view.Invalidate();

        if (view.chart != null)
        {
            view.handler = view.chart.ObserveInvalidate(view, v => v.Invalidate());
        }
    }

    private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
    {
        if (chart != null)
        {
            chart.Draw(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        }
        else
        {
            e.Surface.Canvas.Clear(SKColors.Transparent);
        }
    }
}