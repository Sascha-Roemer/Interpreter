namespace Interpreter;

public class Tokanizer
{
    private static List<(Regex Pattern, Func<string, Expression> Activator)> _expressions =
        new()
        {
            (Pattern: new Regex("^([0-9]+)"), Activator: e => new Number(e)),
            (Pattern: new Regex("^\"([^\"]*)\""), Activator: e => new Text(e)),
            (Pattern: new Regex("^(\\+|\\-|\\*|/|==|!=|or|and)"), Activator: e => new Operator(e)),
            (Pattern: new Regex("^(\\(|\\))"), Activator: e => new Bracket(e)),
            (Pattern: new Regex("^(,)"), Activator: e => new Comma(e)),
            (Pattern: new Regex("^(in *\\()"), Activator: e => new InOperator(e)),
            (Pattern: new Regex("^([a-zA-Z_][a-zA-Z0-9_]*)"), Activator: e => new Variable(e)),
        };

    private string? _initialValue;
    private string? _value;

    public Tokanizer(string value)
    {
        _initialValue = value?.Trim();
        _value = _initialValue;
    }

    public IEnumerable<Expression> Tokanize()
    {
        _value = _initialValue;

        if ((_value?.Length ?? 0) == 0) yield return new EmptyExpression();

        else
        while (_value?.Length > 0)
        {
            Match? match = null;
            foreach(var e in _expressions)
            {
                match = e.Pattern.Match(_value);
                if (match.Success)
                {
                    var start = match.Groups[0].Length;
                    var length = _value.Length - start;
                    _value = _value.Substring(start, length).Trim();

                    yield return e.Activator(match.Groups[1].Value);
                    break;
                }
            }

            if (match?.Success != true)
            {
                throw new InvalidOperationException($"Unknown token \"{_value}\"");
            }
        }
    }
}
