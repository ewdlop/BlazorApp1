namespace BlazorApp1.Components.DataStructures;

public class TreeNode
{
    public string Text { get; set; } = string.Empty;
    public List<TreeNode> Children { get; set; } = new();
    public bool IsExpanded { get; set; }
}