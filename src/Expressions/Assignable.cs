namespace Interpreter;

public abstract class Assignable : Expression
{
    protected Assignable(string token)
    : base(token)
    {
    }

    public abstract bool IsComplete { get; }

    public abstract void Assign(Expression e);
}
