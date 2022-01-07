namespace Interpreter;

public class Text : Expression
{
    private Value _value;

    public Text(string token) : base(token)
    {
        _value = new Value{ TextValue = token };
    }

    public override Value Evaluate(Context context) => _value;

    public override int Precedence => 1;
}
