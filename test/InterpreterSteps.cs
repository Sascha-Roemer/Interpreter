global using TechTalk.SpecFlow;
global using TechTalk.SpecFlow.Assist;
global using System.Linq;
global using NUnit.Framework;

namespace Interpreter.Test;

[Binding]
public class InterpreterSteps
{
    private static Expression[] _tokens;
    private static Expression _root;
    private Context _context;

    [Given(@"an expression: (.*)")]
    public void GivenAnExpression(string value)
    {
        _tokens = new Tokanizer(value).Tokanize().ToArray();
        _root = new Parser().Parse(_tokens);

        _context = new();
        _context["Genre"] = (Value)"Pop";
    }

    [Given(@"an expression: (.*)")]
    public void GivenAnExpression(string value, Table table)
    {
        GivenAnExpression(value);

        foreach(var row in table.Rows)
        {
            var key = row["Key"];
            var text = row["Text"];

            _context[key] = (Value)text;
        }
    }

    [Then(@"there are (.*) tokens")]
    public void ThenThereAreTokens(int count) =>
        Assert.AreEqual(count, _tokens.Length);

    [Then(@"token values are")]
    public void ThenTokenValuesAre(Table table)
    {
        foreach(var row in table.Rows)
        {
            var index = row.GetInt32("Index");
            var value = row.GetInt32("Value");

            Assert.AreEqual(value, _tokens[index].Evaluate(_context).IntValue);
        }
    }

    [Then(@"tokens are")]
    public void ThenTokensAre(Table table)
    {
        foreach(var row in table.Rows)
        {
            var index = row.GetInt32("Index");
            var token = row.GetString("Token");

            Assert.AreEqual(token, _tokens[index].Token);
        }
    }

    [Then(@"root token is ""(.*)""")]
    public void ThenTokensAre(string token)
    {
        Assert.AreEqual(token, _root.Token);
    }

    [Then(@"the result is (.*)")]
    public void ThenTheResultIs(string value)
    {
        if (value == "true")
        Assert.IsTrue(_root.Evaluate(_context).BooleanValue);

        else if (value == "false")
        Assert.IsFalse(_root.Evaluate(_context).BooleanValue);

        else
        Assert.AreEqual(int.Parse(value), _root.Evaluate(_context).IntValue);
    }
}