using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace RazorClassLibrary1.DM2BD.New
{
    public partial class TableExtended<T> : MudTable<T>
    {
        [Parameter]
        public TableState TableState { get; set; }
        public override TableContextExtened<T> TableContext { get; }
        public TableContextExtened<T> ExtendedContext { get;} = new();
        protected override void OnParametersSet()
        {
            ExtendedContext.SetSortFieldLabel(TableState.SortLabel);
            ExtendedContext.SetSortDirection(TableState.SortDirection);
            
            base.OnParametersSet();
        }
    }
}