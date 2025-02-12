using BlazorApp1.Components.DataStructures;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components
{
    public partial class TreeComponent : ComponentBase
    {
        [Parameter]
        public List<TreeNode>? Items { get; set; }
    }
}