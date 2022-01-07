namespace Interpreter;

public class Bracket : Assignable
{
    private bool _isComplete;

    public Bracket(string value) : base(value)
    {
    }

    protected Expression Element { get; set; }

    public override Value Evaluate(Context context) => Element.Evaluate(new Context(context));

    public override void Assign(Expression e)
    {
        if (e is Bracket b && b.IsClosingBracket) _isComplete = true;

        // Make e the new root of Element.
        if (Element != null)
        {
            ((Assignable)e).Assign(Element);
            Element = e;
        }

        else Element = e;
    }

    public override Expression GetHigherOrderExpression(Expression other) => this;

    public override int Precedence => 1;

    public override bool IsComplete => _isComplete;

    public bool IsClosingBracket => Token == ")";
}
