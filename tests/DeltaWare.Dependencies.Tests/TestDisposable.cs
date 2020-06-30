using System;

namespace DeltaWare.Dependencies.Tests
{
    public class TestDisposable: IDisposable
    {
        public string StringValue { get; set; }

        public int IntValue { get; set; }

        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}
