using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components
{
    public partial class UtcNowComponent : IDisposable
    {
        protected readonly CancellationTokenSource _cancellationTokenSource = new();
        protected CancellationTokenSource _additionalCancellationTokenSource = new();
        protected bool _disposed = false;

        [Parameter]
        public TimeSpan TimeSpan { get; set; } = TimeSpan.FromSeconds(1);
#if false
        protected bool _isReRendering = false;
#endif

        protected override void OnInitialized()
        {
            _additionalCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(_cancellationTokenSource.Token);
            base.OnInitialized();
        }

        protected virtual void Dispose(bool dispose)
        {
            if (!_disposed)
            {
                if (dispose)
                {
                    _cancellationTokenSource.Cancel();
                    _cancellationTokenSource.Dispose();
                    if(!_additionalCancellationTokenSource.IsCancellationRequested)
                    {
                        _additionalCancellationTokenSource.Cancel();
                    }
                    _additionalCancellationTokenSource.Dispose();
                }
                Interlocked.Exchange(ref _disposed, true);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _ = Task.Run(async () =>
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(TimeSpan, _cancellationTokenSource.Token); // Adjust interval for refresh rate
                        await InvokeAsync(StateHasChanged);
                    }
                    catch (OperationCanceledException)
                    {
                        break; // Stop loop on cancellation
                    }
                }
            }, _additionalCancellationTokenSource.Token);
            await base.OnAfterRenderAsync(firstRender);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }
    }
}