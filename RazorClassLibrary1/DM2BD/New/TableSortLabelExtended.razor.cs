using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace RazorClassLibrary1.DM2BD.New
{
    public partial class TableSortLabelExtened<T> : MudBlazor.MudTableSortLabel<T>
    {
        [Parameter] 
        public SortIconPlacement SortIconPlacementValue { get; set; } = SortIconPlacement.After;
        public void Dispose()
        {
            Context?.SortLabels.Remove(this);
        }

        public enum SortIconPlacement
        {
            [Description("before")]
            Before,
            [Description("after")]
            After
        }
        protected string GetSortIconClass()
        {
            if (SortDirection == MudBlazor.SortDirection.Descending)
            {
                return $"mud-table-sort-label-icon mud-direction-desc";
            }
            else if (SortDirection == MudBlazor.SortDirection.Ascending)
            {
                return $"mud-table-sort-label-icon mud-direction-asc";
            }
            else
            {
                return $"mud-table-sort-label-icon";
            }
        }
    }
}