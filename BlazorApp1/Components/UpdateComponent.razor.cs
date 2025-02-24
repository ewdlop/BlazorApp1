using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components
{
    public partial class UpdateComponent : IDisposable
    {
        protected readonly CancellationTokenSource _cancellationTokenSource = new();
        protected bool _disposed;

        [Parameter]
        public TimeSpan TimeSpan { get; set; } = TimeSpan.FromMicroseconds(1);

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _ = UpdatePeriodicallyAsync(_cancellationTokenSource.Token);
        }

        protected async Task UpdatePeriodicallyAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan, cancellationToken);
                    await Task.Run(() => InvokeAsync(StateHasChanged), cancellationToken);
                }
            }
            catch (TaskCanceledException)
            {
                // 正常處理取消請求
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _cancellationTokenSource.Cancel();
                    _cancellationTokenSource.Dispose();
                }
                Interlocked.Exchange(ref _disposed, true);
            }
        }
    }
}