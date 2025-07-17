using Microsoft.AspNetCore.Components;

namespace StoreApp.Client.Components.ComponentFiles.Counter;

public partial class Counter : ComponentBase
{
    [Parameter] public int Min { get; set; } = 0;

    [Parameter] public int Max { get; set; } = 10;
    
    [Parameter] public int Value { get; set; } = 0;

    [Parameter] public EventCallback<int> ValueChanged { get; set; }
    
    private void Increment()
    {
        if (Value < Max)
        {
            Value++;
            ValueChanged.InvokeAsync(Value);
        }
    }
    
    private void Decrement()
    {
        if (Value > Min)
        {
            Value--;
            ValueChanged.InvokeAsync(Value);
        }
    }
}