global using System;
global using System.Linq;
global using System.Diagnostics;
global using System.Text.RegularExpressions;
global using System.Collections.Generic;

namespace Interpreter;

public class Parser
{
    private Token? _current;
    private Token? _root;
    private Stack<Token> _stack = new();

    public Expression Parse(Expression[] expressions)
    {
        _current = GetLinkedToken(expressions);

        while(_current != null)
        {
            var next = _current.ChooseHigherOrderExpression();

            if (_stack.Count == 0)
            {
                // _root is assigned to next, which becomes the new root.
                _stack.Push(Unlink(next.Assign(_root)));
                _root = next;
                continue;
            }

            if (_current.IsClosingBracket)
            {
                _stack.Pop().Assign(Unlink(_current));
                continue;
            }
            
            var top = _stack.Peek();

            if (_current.IsComma)
            {
                top.Assign(Unlink(_current));
                continue;
            }

            next = next?.Precedence > top.Precedence ? next : _current;

            top.Assign(Unlink(next));
            
            if (top.IsComplete) _stack.Pop();
            
            if (next.IsAssignable) _stack.Push(next);
        }

        return _root?.Expression ?? new EmptyExpression();
    }

    private Token Unlink(Token token)
    {
        if (token == _current)
        {
            _current = token.Next;
        }
        token.Unlink();

        return token;
    }

    private static Token? GetLinkedToken(Expression[] expressions)
    {
        var tokens = expressions.Select(e => new Token(e)).ToArray();

        // Initialize linked list of tokens.
        for(var i = 1; i < tokens.Length; i++)
        {
            tokens[i].LinkPrevious(tokens[i - 1]);
        }

        return tokens[0];
    }
}
