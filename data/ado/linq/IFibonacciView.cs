using System;

namespace linq
{
    interface IFibonacciView
    {
        void InvokeAsync(Action a);
        void SetWaitPointer();
        void SetNormalPointer();
    }
}
