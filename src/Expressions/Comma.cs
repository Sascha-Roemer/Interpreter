namespace Interpreter;

public class Comma : Expression
{
    public Comma(string value) : base(",")
    {
    }

    public override int Precedence => 1;

    public override Value Evaluate(Context context) => Value.Empty;
}
