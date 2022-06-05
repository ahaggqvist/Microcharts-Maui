// Copyright (c) Morgan SOULLEZ. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using SkiaSharp;
using SkiaSharp.Views.Windows;

namespace Microcharts.WinUI;

public class ChartView : SKXamlCanvas
{
    public static readonly DependencyProperty ChartProperty =
        DependencyProperty.Register(nameof(Chart), typeof(Chart), typeof(ChartView), new PropertyMetadata(null, OnChartChanged));

    private Chart _chart;

    private InvalidatedWeakEventHandler<ChartView> _handler;

    public ChartView()
    {
        Background = new SolidColorBrush(Colors.Transparent);
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

        if (view?._chart != null)
        {
            view._handler.Dispose();
            view._handler = null;
        }

        if (view == null)
        {
            return;
        }

        view._chart = e.NewValue as Chart;
        view.Invalidate();

        if (view._chart != null)
        {
            view._handler = view._chart.ObserveInvalidate(view, v => v.Invalidate());
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