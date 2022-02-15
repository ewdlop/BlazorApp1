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
            ExtendedContext.SortFieldLabel = TableState.SortLabel;
            ExtendedContext.SortDirection = TableState.SortDirection;
            base.OnParametersSet();
        }
    }

    public partial class TableContextExtened<T> : TableContext<T>
    {
        public override string SortFieldLabel
        {
            get;
            public set;
        }

    }
}