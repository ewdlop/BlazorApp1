using BlazorApp1.Components.DataStructures;
using BlazorApp1.Models;
using BlazorApp1.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components.Services;

// TabManager.cs - Example of managing tabbed components
public class TabManager
{
    private readonly ComponentStack<IComponent> _tabStack;
    private readonly Dictionary<string, ComponentTree<IComponent>> _tabTrees;

    public TabManager()
    {
        _tabStack = new ComponentStack<IComponent>();
        _tabTrees = new Dictionary<string, ComponentTree<IComponent>>();
    }

    public void OpenNewTab(string tabId, IComponent rootComponent)
    {
        var tree = new ComponentTree<IComponent>(rootComponent);
        _tabTrees[tabId] = tree;
        _tabStack.Push(rootComponent);
    }

    public Maybe<IComponent> GetActiveTab()
    {
        return _tabStack.Pop();
    }

    public ComponentTree<IComponent>? GetTabTree(string tabId)
    {
        return _tabTrees.TryGetValue(tabId, out var tree) ? tree : null;
    }
}