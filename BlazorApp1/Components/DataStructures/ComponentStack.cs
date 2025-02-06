// UserData.cs - Example domain model
using BlazorApp1.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components.DataStructures;

// ComponentStack.cs - Stack monad for Blazor components
public class ComponentStack<TComponent> where TComponent : IComponent
{
    private readonly Stack<TComponent> _stack;

    public ComponentStack()
    {
        _stack = new Stack<TComponent>();
    }

    public ComponentStack<TComponent> Push(TComponent component)
    {
        return this;
    }

    public Maybe<TComponent> Pop()
    {
        return _stack.TryPop(out var component)
            ? Maybe<TComponent>.Some(component)
            : Maybe<TComponent>.None();
    }

    public ComponentStack<TComponent> Map(Func<TComponent, TComponent> f)
    {
        var newStack = new ComponentStack<TComponent>();
        foreach (var component in _stack.Reverse())
        {
            newStack.Push(f(component));
        }
        return newStack;
    }
}