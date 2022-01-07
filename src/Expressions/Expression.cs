namespace Interpreter;

public abstract class Expression
{
    public Expression(string? token)
    {
        Token = token;
    }

    public abstract int Precedence { get; }

    public string? Token { get; }

    public abstract Value Evaluate(Context context);

    public virtual Expression GetHigherOrderExpression(Expression? other)
    {
        if (other == null) return this;

        return other.Precedence > Precedence ? other : this;
    }
}
