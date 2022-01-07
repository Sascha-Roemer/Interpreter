namespace Interpreter;

[DebuggerDisplay("{Expression.Token}")]
internal class Token
{
    public Token(Expression expression)
    {
        Expression = expression;
    }

    public Expression Expression { get; }

    public Token? Previous { get; private set; }

    public Token? Next { get; private set; }

    public bool IsComplete => (Expression is Assignable a) && a.IsComplete;

    public bool IsClosingBracket => (Expression is Bracket b) && b.IsClosingBracket;

    internal object ChooseAssignable()
    {
        throw new NotImplementedException();
    }

    public bool IsAssignable => Expression is Assignable;

    public int Precedence => Expression.Precedence;

    public bool IsComma => Expression is Comma;

    public void LinkPrevious(Token token)
    {
        Previous = token;
        token.Next = this;
    }

    public void Unlink()
    {
        if (Next != null)
        {
            Next.Previous = Previous;
        }

        if (Previous != null)
        {
            Previous.Next = Next;
        }
    }

    public Token ChooseHigherOrderExpression()
    {
        if (Next?.IsAssignable == false) return this;

        var higher = Expression.GetHigherOrderExpression(Next?.Expression);

        return 
        higher == Expression || Next == null
        ? this
        : Next;
    }

    internal Token Assign(Token? token)
    {
        if (token != null)
        ((Assignable)Expression).Assign(token.Expression);

        return this;
    }
}
