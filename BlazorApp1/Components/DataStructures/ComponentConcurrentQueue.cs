// UserData.cs - Example domain model
using BlazorApp1.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Concurrent;

namespace BlazorApp1.Components.DataStructures;

public class ComponentConcurrentQueue<TComponent> where TComponent : IComponent
{
    private readonly ConcurrentQueue<TComponent> _queue;
    public ComponentConcurrentQueue()
    {
        _queue = new ConcurrentQueue<TComponent>();
    }
    public ComponentConcurrentQueue<TComponent> Enqueue(TComponent component)
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
    public ComponentConcurrentQueue<TComponent> Map(Func<TComponent, TComponent> f)
    {
        var newQueue = new ComponentConcurrentQueue<TComponent>();
        foreach (var component in _queue)
        {
            newQueue.Enqueue(f(component));
        }
        return newQueue;
    }
}