using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;

namespace MudBlazor.DM2BDCustom
{
    public partial class MudTd : MudComponentBase
    {

        protected string Classname =>
        new CssBuilder("mud-table-cell") 
            .AddClass("mud-table-cell-hide", HideSmall)
            .AddClass("text-align-right-dm2bd", Align.Equals(Align.Right))
            .AddClass(Class)
        .Build();
        [Parameter] public Align Align { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public string DataLabel { get; set; }

        /// <summary>
        /// Hide cell when breakpoint is smaller than the defined value in table.
        /// </summary>
        [Parameter] public bool HideSmall { get; set; }
    }
}
