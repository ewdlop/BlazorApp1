using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Components;
using MudBlazor.Extensions;
using MudBlazor.Utilities;

namespace MudBlazor.DM2BDCustom
{
    public abstract class MudChartBase : MudComponentBase
    {
        [Parameter] public double[] InputData { get; set; } = Array.Empty<double>();

        [Parameter] public Dictionary<string, double[]> InputDataDictionary { get; set; } = new();

        [Parameter] public string[] InputLabels { get; set; } = Array.Empty<string>();

        [Parameter] public string[] XAxisLabels { get; set; } = Array.Empty<string>();

        [Parameter] public List<ChartSeries> ChartSeries { get; set; } = new List<ChartSeries>();

        [Parameter] public ChartOptions ChartOptions { get; set; } = new ChartOptions();

        [Parameter] public bool Dense { get; set; }

        [Parameter] public string[] InputDataCategories { get; set; } = Array.Empty<string>();

        [Parameter] public string SelectedInputDataCategory { get; set; } = string.Empty;

        [Parameter] public bool OnMudGrid { get; set; }

        protected string Classname =>
        new CssBuilder("mud-chart")
           .AddClass($"mud-chart-legend-{ConvertLegendPosition(LegendPosition).ToDescriptionString()}")
           .AddClass(Class)
           .Build();

        [CascadingParameter]
        public bool RightToLeft { get; set; }

        /// <summary>
        /// The Type of the chart.
        /// </summary>
        [Parameter] public ChartType ChartType { get; set; }

        /// <summary>
        /// The Width of the chart, end with % or px.
        /// </summary>
        [Parameter] public string Width { get; set; } = "80%";

        /// <summary>
        /// The Height of the chart, end with % or px.
        /// </summary>
        [Parameter] public string Height { get; set; } = "80%";

        /// <summary>
        /// The placement direction of the legend if used.
        /// </summary>
        [Parameter] public Position LegendPosition { get; set; } = Position.Bottom;

        private Position ConvertLegendPosition(Position position)
        {
            return position switch
            {
                Position.Start => RightToLeft ? Position.Right : Position.Left,
                Position.End => RightToLeft ? Position.Left : Position.Right,
                _ => position
            };
        }

        private int _selectedIndex = -1;
        private int _hoveredIndex = -1;

        /// <summary>
        /// Selected index of a portion of the chart.
        /// </summary>
        [Parameter]
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value != _selectedIndex)
                {
                    _selectedIndex = value;
                    SelectedIndexChanged.InvokeAsync(value);
                }
            }
        }

        /// <summary>
        /// Selected index of a portion of the chart.
        /// </summary>
        [Parameter] public EventCallback<int> SelectedIndexChanged { get; set; }

        protected void OnLegendTextSelected(int selectedLegendTextIndex)
        {
            if (SelectedIndex != selectedLegendTextIndex)
            {
                SelectedIndex = selectedLegendTextIndex;
            }
        }

        /// <summary>
        /// Hovered index of a portion of the chart.
        /// </summary>
        public int HoveredIndex
        {
            get => _hoveredIndex;
            set
            {
                if (value != _hoveredIndex)
                {
                    _hoveredIndex = value;

                    if (HoveredIndexChanged.HasDelegate)
                        HoveredIndexChanged.InvokeAsync(value);

                }
            }
        }

        /// <summary>
        /// Hovered index of a portion of the chart.
        /// </summary>
        [Parameter] public EventCallback<int> HoveredIndexChanged { get; set; }
        [Parameter] public NumberFormatInfo NumberFormatInfo { get; set; } = new();
        protected void OnLegendTextHovered(int hoveredLegendTextIndex)
        {
            ShowMudChip = false;
            if (HoveredIndex != hoveredLegendTextIndex)
            {
                HoveredIndex = hoveredLegendTextIndex;
            }
        }
        protected double HoveredInputDataCount { get; set; }

        [Parameter]
        public string LegendSearchText { get; set; }
        [Parameter]
        public int[] FilteredIndexes { get; set; }

        protected double MouseOverX { get; set; }

        protected double MouseOverY { get; set; }

        protected bool ShowMudChip { get; set; }
        
        [Parameter]
        public string[] InputColorsHex { get; set; }

        /// <summary>
        /// Scales the input data to the range between 0 and 1
        /// </summary>
        protected double[] GetNormalizedData()
        {
            if (InputData == null)
                return Array.Empty<double>();
            var total = InputData.Sum();
            return InputData.Select(x => Math.Abs(x) / total).ToArray();
        }

        /// <summary>
        /// Scales the input data to the range between 0 and 1 with indexes included
        /// </summary>
        protected (double, double)[] GetNormalizedDataIncludeOriginal()
        {
            if (InputData == null)
                return Array.Empty<(double, double)>();
            var total = InputData.Sum();
            return InputData.Select(x => (Math.Abs(x) / total, x)).ToArray();
        }

        protected static string ToString(double number, string format = null)
        {
            if (string.IsNullOrEmpty(format))
                return number.ToString(CultureInfo.InvariantCulture);

            return number.ToString(format);
        }
    }

    public enum ChartType
    {
        MiniDonut,
        Donut,
        Line,
        Pie,
        Bar
    }
}
