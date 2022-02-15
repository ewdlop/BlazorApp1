using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using MudBlazor.Charts;
using MudBlazor.Charts.SVG.Models;

namespace Dm2bdUiExtensions;

public partial class DonutChartExtended : Donut
{
    [Parameter]
    public EventCallback<(MouseEventArgs mouseEventArgs, SvgCircle svgCircle)> 
        OnSectionMouseOverEventCallback { get; set; }

    private List<SvgCircle> _circles = new();
    private List<SvgLegend> _legends = new();

    protected override void OnParametersSet()
    {
        _circles.Clear();
        _legends.Clear();
        double counterClockwiseOffset = 25;
        double totalPercent = 0;
        double offset;

        var counter = 0;
        foreach (var data in GetNormalizedData())
        {
            var percent = data * 100;
            var reversePercent = 100 - percent;
            offset = 100 - totalPercent + counterClockwiseOffset;
            totalPercent += percent;

            var circle = new SvgCircle()
            {
                Index = counter,
                CX = 20,
                CY = 20,
                Radius = 15.91549430918954,
                StrokeDashArray = $"{ToS(percent)} {ToS(reversePercent)}",
                StrokeDashOffset = offset
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
                Data = data.ToString()
            };
            _legends.Add(legend);

            counter += 1;
        }
    }

    protected void FireOnSectionMouseOverEventCallback(MouseEventArgs mouseEventArgs, 
        SvgCircle svgCircle)
    {
        if (OnSectionMouseOverEventCallback.HasDelegate)
            OnSectionMouseOverEventCallback.InvokeAsync((mouseEventArgs, svgCircle));
    }
}