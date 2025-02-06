// UserData.cs - Example domain model
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components.DataStructures;

// ComponentTree.cs - Tree monad for Blazor components
public class ComponentTree<TComponent> where TComponent : IComponent
{
    private readonly TComponent _component;
    private readonly List<ComponentTree<TComponent>> _children;

    public ComponentTree(TComponent component)
    {
        _component = component;
        _children = new List<ComponentTree<TComponent>>();
    }

    public ComponentTree<TComponent> AddChild(ComponentTree<TComponent> child)
    {
        _children.Add(child);
        return this;
    }

    public ComponentTree<TComponent> Map(Func<TComponent, TComponent> f)
    {
        var newTree = new ComponentTree<TComponent>(f(_component));
        foreach (var child in _children)
        {
            newTree.AddChild(child.Map(f));
        }
        return newTree;
    }

    public TComponent Component => _component;
    public IReadOnlyList<ComponentTree<TComponent>> Children => _children;
}