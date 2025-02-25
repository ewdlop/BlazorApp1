using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace BlazorApp1.Components;

/// <summary>
/// <seealso cref="https://learn.microsoft.com/en-us/aspnet/core/blazor/components/virtualization?view=aspnetcore-9.0"/>"/>
/// </summary>
/// <typeparam name="TItem"></typeparam>
public partial class VirtualizedList<TItem, TKey> : ComponentBase where TKey : notnull
{
    protected Virtualize<TItem>? virtualizeComponent = null;

    [Parameter]
    public required int ItemSizeInPixel { get; init; } = 50;

    [Parameter]
    public required int MaxItemCount { get; init; } = 100;

    [Parameter]
    public required int OverscanCount { get; init; } = 10;

    [Parameter]
    public required string SpacerElement { get; init; } = "div";

    [Parameter]
    public required ICollection<TItem> Items { get; init; } = [];

    [Parameter]
    public required RenderFragment? EmptyContent { get; init; } = null;

    [Parameter]
    public required RenderFragment<TItem>? ItemContent { get; init; } = null;

    [Parameter]
    public required RenderFragment<TItem>? Placeholder { get; init; } = null;

    [Parameter]
    public required Func<TItem, bool> ItemFilterFunc { get; init; } = _ => true;

    [Parameter]
    public required Func<TItem, TKey?> ItemSorterFunc { get; init; } = _ => default;

    [Parameter]
    public required Func<ItemsProviderRequest, ValueTask<ItemsProviderResult<TItem>>>? ItemsProviderAsyncFunc { get; set; } = null;

    [Parameter]
    public required EventCallback<ItemsProviderRequest> OnItemsProviderRequested { get; set; }

    [Parameter]
    public required EventCallback<ItemsProviderResult<TItem>> OnItemsProviderResulted { get; set; }

    protected virtual Func<ItemsProviderRequest, ValueTask<ItemsProviderResult<TItem>>> DefaultItemsProviderAsyncFunc => (ItemsProviderRequest request) =>
    {
        int skip = request.StartIndex;
        int take = request.Count;
        IEnumerable<TItem> items = Items.Where(ItemFilterFunc).OrderBy(ItemSorterFunc).Skip(skip).Take(take);
        return new ValueTask<ItemsProviderResult<TItem>>(new ItemsProviderResult<TItem>(items, Items.Count));
    };

    protected virtual async ValueTask<ItemsProviderResult<TItem>> ItemProviderAsync(
        ItemsProviderRequest request)
    {
        if (OnItemsProviderRequested.HasDelegate)
        {
            await OnItemsProviderRequested.InvokeAsync(request);
        }
        ItemsProviderAsyncFunc ??= DefaultItemsProviderAsyncFunc;
        ItemsProviderResult<TItem> result = await ItemsProviderAsyncFunc(request);
        if (OnItemsProviderResulted.HasDelegate)
        {
            await OnItemsProviderResulted.InvokeAsync(result);
        }
        return result;
    }

    public virtual Task RefreshDataAsync()
    {
        return InvokeAsync(async () =>
        {
            if (virtualizeComponent is not null)
            {
                await virtualizeComponent.RefreshDataAsync();
            }
        });
    }
}