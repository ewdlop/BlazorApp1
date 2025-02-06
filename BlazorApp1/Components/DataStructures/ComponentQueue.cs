// UserData.cs - Example domain model
using BlazorApp1.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components.DataStructures;

// ComponentQueue.cs - Queue monad for Blazor components
public class ComponentQueue<TComponent> where TComponent : IComponent
{
    private readonly Queue<TComponent> _queue;

    public ComponentQueue()
    {
        _queue = new Queue<TComponent>();
    }

    public ComponentQueue<TComponent> Enqueue(TComponent component)
    {
        _queue.Enqueue(component);
        return this;
    }

    public Maybe<TComponent> Dequeue()
    {
        return _queue.TryDequeue(out var component)
            ? Maybe<TComponent>.Some(component)
            : Maybe<TComponent>.None();
    }

    public ComponentQueue<TComponent> Map(Func<TComponent, TComponent> f)
    {
        var newQueue = new ComponentQueue<TComponent>();
        foreach (var component in _queue)
        {
            newQueue.Enqueue(f(component));
        }
        return newQueue;
    }
}