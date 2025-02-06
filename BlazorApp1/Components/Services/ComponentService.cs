using BlazorApp1.Components.DataStructures;
using BlazorApp1.Models;
using BlazorApp1.Services;
using BlazorApp1.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components.Services;

// ComponentService.cs - Service for managing component lifecycle
public class ComponentService
{
    private ComponentQueue<IComponent> _renderQueue;
    private ComponentStack<IComponent> _navigationStack;
    private ComponentTree<IComponent> _componentTree;

    public ComponentService()
    {
        _renderQueue = new ComponentQueue<IComponent>();
        _navigationStack = new ComponentStack<IComponent>();
        _componentTree = null!;
    }

    public void InitializeMainLayout(MainLayout mainLayout)
    {
        _componentTree = new ComponentTree<IComponent>(mainLayout);
    }

    public void QueueForRender(IComponent component)
    {
        _renderQueue.Enqueue(component);
    }

    public void PushToNavigationStack(IComponent component)
    {
        _navigationStack.Push(component);
    }
}