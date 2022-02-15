using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using MudBlazor.DM2BDCustom.Charts.SVG.Models;
using MudBlazor.Utilities;

namespace MudBlazor.DM2BDCustom.Charts
{
    partial class MiniDonut : MudChartBase
    {
        [CascadingParameter] public MudChart MudChartParent { get; set; }
        private List<SvgCircle> _circles = new();
        private List<SvgLegend> _legends = new();

        protected string ParentWidth => MudChartParent?.Width;
        protected string ParentHeight => MudChartParent?.Height;
        private new string Classname =>
            new CssBuilder("mud-chart-donut mini-dm2bd")
            .AddClass("mud-donut-dense-margin", Dense)
            .Build();

        protected override void OnParametersSet()
        {
            _circles.Clear();
            _legends.Clear();
            double counterClockwiseOffset = 25;
            double totalPercent = 0;
            double offset;

            var counter = 0;
            foreach (var data in GetNormalizedDataIncludeOriginal())
            {
                var percent = data.Item1 * 100;
                var reversePercent = 100 - percent;
                offset = 100 - totalPercent + counterClockwiseOffset;
                totalPercent += percent;

                var circle = new SvgCircle()
                {
                    Index = counter,
                    CX = 20,
                    CY = 20,
                    Radius = 15.91549430918954,
                    StrokeDashArray = $"{percent} {(reversePercent)}",
                    StrokeDashOffset = offset,
                    ColorHex = InputColorsHex[counter]
                };
                _circles.Add(circle);


                var labels = "";
                if (counter < InputLabels.Length)
                {
                    labels = InputLabels[counter];
                }
                var legend = new SvgLegend()
                {
                    Index = counter,
                    Labels = labels,
                    Data = data.ToString(),
                    ColorHex = InputColorsHex[counter],
                };
                _legends.Add(legend);

                counter += 1;
            }
        }
    }
}
