using BlazorApp1.Components.DataStructures;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    public partial class TreeExample : ComponentBase
    {
        private List<TreeNode> treeData = new()
        {
            new TreeNode
            {
                Text = "Root Node 1",
                Children = new List<TreeNode>
                {
                    new TreeNode { Text = "Child 1.1" },
                    new TreeNode
                    {
                        Text = "Child 1.2",
                        Children = new List<TreeNode>
                        {
                            new TreeNode { Text = "Grandchild 1.2.1" },
                            new TreeNode { Text = "Grandchild 1.2.2" }
                        }
                    }
                }
            },
            new TreeNode
            {
                Text = "Root Node 2",
                Children = new List<TreeNode>
                {
                    new TreeNode { Text = "Child 2.1" }
                }
            }
        };
    }
}