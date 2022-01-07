namespace Interpreter;

public class EmptyExpression : Expression
{
    public EmptyExpression() : base(null)
    {
    }

    public override int Precedence => 0;

    public override Value Evaluate(Context context) => Value.Empty;
}
