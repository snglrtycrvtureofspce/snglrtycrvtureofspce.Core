using System.Collections.Generic;
using System.Threading.Tasks;

namespace snglrtycrvtureofspce.Core.Providers;

public class TestAsyncEnumerator<T>(IEnumerator<T> inner) : IAsyncEnumerator<T>
{
    public T Current => inner.Current;

    public ValueTask DisposeAsync()
    {
        inner.Dispose();
        return ValueTask.CompletedTask;
    }

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(inner.MoveNext());
    }
}
