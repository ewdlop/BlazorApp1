using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;


namespace Dm2bdUiExtensions
{
    public partial class MudTableSortLabelExtened<T> : MudBlazor.MudTableSortLabel<T>
    {
        [Parameter] public SortIconPlacement SortIconPlacement { get; set; } = SortIconPlacement.After;
        public void Dispose()
        {
            Context?.SortLabels.Remove(this);
        }
    }
}
