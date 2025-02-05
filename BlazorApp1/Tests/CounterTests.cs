using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Xunit;

namespace BlazorTests
{
    // 📌 Blazor Component (Counter)
    public partial class Counter : ComponentBase
    {
        private int count = 0;
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

    // 📌 Unit Test for Counter Component
    public class CounterTests : TestContext
    {
        [Fact]
        public void Counter_ShouldIncrement_WhenButtonClicked()
        {
            // Arrange: Render component
            var cut = RenderComponent<Counter>();

            // Act: Click button
            cut.Find("button").Click();

            // Assert: Verify count updates
            cut.MarkupMatches("<button>Increment</button><p>Count: 1</p>");
        }
    }
}
