using BlazorApp1.Components.DataStructures;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components
{
    public partial class TreeItemComponent
    {
        [Parameter]
        public TreeNode Node { get; set; } = new();

        private void ToggleExpand()
        {
            Node.IsExpanded = !Node.IsExpanded;
        }
    }
}