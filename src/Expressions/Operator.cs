namespace Interpreter;

public class Operator : Assignable
{
    public Operator(string value) : base(value)
    {
    }

    public Expression? Left { get; set; }

    public Expression? Right { get; set; }

    public override bool IsComplete => Left != null && Right != null;

    public override void Assign(Expression e)
    {
        if (IsComplete) throw new InvalidOperationException("Expression is complete.");

        if (Left != null) Right = e;
        else Left = e;
    }

    public override Value Evaluate(Context context) =>
        Token switch 
        {
            "or" => new Value { BooleanValue = Left.Evaluate(new Context(context)).BooleanValue || Right.Evaluate(new Context(context)).BooleanValue },
            "and" => new Value { BooleanValue = Left.Evaluate(new Context(context)).BooleanValue && Right.Evaluate(new Context(context)).BooleanValue },
            "==" => Left.Evaluate(new Context(context)) == Right.Evaluate(new Context(context)),
            "!=" => Left.Evaluate(new Context(context)) != Right.Evaluate(new Context(context)),
            "+" => Left.Evaluate(new Context(context)) + Right.Evaluate(new Context(context)),
            "-" => Left.Evaluate(new Context(context)) - Right.Evaluate(new Context(context)),
            "*" => Left.Evaluate(new Context(context)) * Right.Evaluate(new Context(context)),
            "/" => Left.Evaluate(new Context(context)) / Right.Evaluate(new Context(context)),
            _ => throw new InvalidOperationException($"Unknown operator {Token}.")
        };

    public override int Precedence =>
        Token switch 
        {
            "or" => 2,
            "and" => 3,
            "==" => 4,
            "!=" => 4,
            "+" => 6,
            "-" => 6,
            "*" => 7,
            "/" => 7,
            _ => throw new InvalidOperationException("Unknown operator.")
        };
}
