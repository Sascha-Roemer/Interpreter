namespace Interpreter;

public class Value
{
    public int IntValue { get; set; }

    public bool BooleanValue { get; set; }

    public string TextValue { get; set; }

    public static Value Empty { get; } = new Value();

    public static explicit operator Value(string value) =>
        value != null ? new Value { TextValue = value } : Value.Empty;

    public static Value operator +(Value a, Value b) =>
        new Value{ IntValue = a.IntValue + b.IntValue };

    public static Value operator -(Value a, Value b) =>
        new Value{ IntValue = a.IntValue - b.IntValue };

    public static Value operator *(Value a, Value b) =>
        new Value{ IntValue = a.IntValue * b.IntValue };

    public static Value operator /(Value a, Value b) =>
        new Value{ IntValue = a.IntValue / b.IntValue };

    public static Value operator ==(Value a, Value b) =>
        new Value{ BooleanValue = a.IntValue == b.IntValue && a.TextValue == b.TextValue };

    public static Value operator !=(Value a, Value b) =>
        new Value{ BooleanValue = a.IntValue != b.IntValue || a.TextValue != b.TextValue };
}