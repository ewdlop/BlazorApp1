using System;
using System.Threading.Tasks;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BlazorTests
{
    // 📌 Mock Service Interface
    public interface ICounterService
    {
        Task<int> GetInitialCountAsync();
    }

    // 📌 Mock Implementation of the Service
    public class MockCounterService : ICounterService
    {
        public Task<int> GetInitialCountAsync() => Task.FromResult(10);
    }

    // 📌 Blazor Component (Counter with DI)
    public partial class Counter : ComponentBase
    {
        [Inject] public ICounterService CounterService { get; set; } = default!;

        private int count = 0;

        protected override async Task OnInitializedAsync()
        {
            count = await CounterService.GetInitialCountAsync();
        }

        void Increment() => count++;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "button");
            builder.AddAttribute(1, "onclick", EventCallback.Factory.Create(this, Increment));
            builder.AddContent(2, "Increment");
            builder.CloseElement();
            builder.OpenElement(3, "p");
            builder.AddContent(4, $"Count: {count}");
            builder.CloseElement();
        }
    }

    // 📌 Unit Test for Counter Component with DI
    public class CounterTests : TestContext
    {
        public CounterTests()
        {
            // ✅ Add Mock Service to DI Container
            Services.AddSingleton<ICounterService, MockCounterService>();
        }

        [Fact]
        public async Task Counter_ShouldStartWithServiceValue()
        {
            // Arrange: Render component
            var cut = RenderComponent<Counter>();

            // Act: Wait for async initialization
            await cut.InvokeAsync(() => cut.Render());

            // Assert: Verify initial value from service
            cut.MarkupMatches("<button>Increment</button><p>Count: 10</p>");
        }

        [Fact]
        public void Counter_ShouldIncrement_WhenButtonClicked()
        {
            // Arrange: Render component
            var cut = RenderComponent<Counter>();

            // Act: Click button
            cut.Find("button").Click();

            // Assert: Verify count updates
            cut.MarkupMatches("<button>Increment</button><p>Count: 11</p>");
        }
    }
}
