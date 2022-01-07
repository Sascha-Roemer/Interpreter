namespace Interpreter;

public class InOperator : Bracket
{
    public InOperator(string value) : base("in")
    {
    }

    private List<Expression> Arguments { get; } = new List<Expression>(3);
    private bool _assignNewArgument = true;

    public override void Assign(Expression e)
    {
        if (e is Bracket b && b.Token == ")"
            || Element == null)
        {
            base.Assign(e);
            return;
        }

        if (e is Comma)
        {
            _assignNewArgument = true;
            return;
        }
        
        if (_assignNewArgument)
        {
            Arguments.Add(e);
            _assignNewArgument = false;
        }

        // Make e the new root of the current argument.
        else
        {
            ((Assignable)e).Assign(Arguments.Last());
            Arguments[Arguments.Count - 1] = e;
        }
    }

    public override Value Evaluate(Context context)
    {
        return 
        Arguments
            .Select(e => e.Evaluate(new Context(context)) == Element.Evaluate(new Context(context)))
            .FirstOrDefault(e => e.BooleanValue)
        ?? new Value();
    }

    public override int Precedence => 5;
}
