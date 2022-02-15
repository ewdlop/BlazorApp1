using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor.DM2BDCustom.Charts.SVG.Models;
using MudBlazor.Utilities;

namespace MudBlazor.DM2BDCustom.Charts
{
    public partial class Legend : MudChartBase
    {
        [CascadingParameter] public MudChart MudChartParent { get; set; }
        [Parameter] public bool FlexBox { get; set; }
        [Parameter] public List<SvgLegend> Data { get; set; } = new List<SvgLegend>();
        [Parameter] public EventCallback<int> OnLegendTextSelectedCallback { get; set; }
        [Parameter] public EventCallback<int> OnLegendTextHoveredCallback { get; set; }
        private int LegendHoveredIndex { get; set; } = -1;
        private List<SvgLegend> _displayedData;
        private List<SvgLegend> _previousData;
        private new string Classname => 
            new CssBuilder("mud-chart-legend")
            .AddClass("flexbox", FlexBox)
            .AddClass(Class).Build();

        protected override void OnParametersSet()
        {
            if (_previousData is null || Data.GetHashCode() != _previousData.GetHashCode())
            {
                _displayedData = Data;
            }

            _previousData = Data;

            base.OnParametersSet();
        }

        private List<SvgLegend> GetFilteredData()
        {
            return _displayedData = Data
                .Where(data => !FilteredIndexes.Any(filteredindex => filteredindex == data.Index))
                .ToList();
        }

        private Task OnTextSelected(int legendIndex)
        {
            LegendHoveredIndex = legendIndex;
            return OnLegendTextSelectedCallback.InvokeAsync(legendIndex);
        }

        private Task OnTextHovered(int legendIndex)
        {
            if (LegendHoveredIndex != legendIndex)
            {
                LegendHoveredIndex = legendIndex;
                return OnLegendTextHoveredCallback.InvokeAsync(legendIndex);
            }
            return Task.CompletedTask;
        }
    }
}
