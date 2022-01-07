namespace Interpreter;

public class Number : Expression
{
    private Value _value;

    public Number(string value)
    : base(value)
    {
        _value = new Value { IntValue = int.Parse(value) };
    }

    public override Value Evaluate(Context context) => _value;

    public override int Precedence => 1;
}
