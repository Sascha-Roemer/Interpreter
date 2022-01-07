namespace Interpreter;

public class Context
{
    private Dictionary<string, Value> _values = new();

    public Context()
    {
    }

    public Context(Context obj)
    {
        foreach(var e in obj._values) _values.Add(e.Key, e.Value);
    }

    public Value? this[string key]
    {
        get => _values.TryGetValue(key, out var value) ? value : null;
        set
        {
            if (!ReferenceEquals(value, null)) _values[key] = value;
        }
    }
}