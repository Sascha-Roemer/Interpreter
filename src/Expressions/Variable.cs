namespace Interpreter;

public class Variable : Expression
{
    public Variable(string token) : base(token)
    {
        if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));
    }

    public override int Precedence => 1;

    public override Value Evaluate(Context context) =>
        (Token != null ? context[Token] : null) ?? Value.Empty;
}