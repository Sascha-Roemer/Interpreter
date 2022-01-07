## A tokanizer, parser and AST interpreter written in C#/.NET6 that understands a simple set of SQL style syntax.

This project originated from some other project where I wanted to be able to parse very basic SQL expressions like:

```
area in ("Marketing", "Sales") and assignee in ("John") and duration > 60
```

The syntax is simple enough to be able to process it with regular expressions alone - but I always had plans to write my own interpreter.

Simplicity was one of the main goals. The library is supposed to give a basic understanding how an AST can be constructed and interpreted. When you look at the test/Parser.feature you will get a impression on what it can do:

```
Scenario: Plus expression
Given an expression: 1 + 2 + 3
Then there are 5 tokens
And token values are
    | Index | Value |
    | 0     | 1     |
    | 2     | 2     |
    | 4     | 3     |
Then the result is 6

Scenario: Plus/multiply expression
Given an expression: 1 + 2 * 3 + 4
Then the result is 11

Scenario: Bracket expression (1)
Given an expression: (1 + 2) * 3
Then the result is 9

Scenario: Bracket expression (2)
Given an expression: 5 * (2 + 3)
Then the result is 25

Scenario: Bracket expression (3)
Given an expression: (1 * 1 + 1 * 1 + 1 * 1 + 1)
Then the result is 4

Scenario: Bracket expression (4)
Given an expression: ((1 + 1) * (1 + 1) * (1 + 1))
Then the result is 8

Scenario: Equality expression
Given an expression: 1 + 1 * 2 == 6 - 1 * 3
Then the result is true

Scenario: In expression
Given an expression: 5 in (1 * 2 + 3, 9)
Then tokens are
    | Index | Token |
    | 1     | in    |
And root token is "in"
And the result is true


Scenario: Text expression
Given an expression: "B" == "A" or "B" == "B"
Then tokens are
    | Index | Token |
    | 0     | B     |
    | 1     | ==    |
    | 2     | A     |
    | 3     | or    |
    | 4     | B     |
    | 5     | ==    |
    | 6     | B     |
And root token is "or"
And the result is true


Scenario: Variables
Given an expression: Genre in ("Pop", "Rock", "Soul") and Genre == "Pop"
    | Key   | Text |
    | Genre | Pop  |
Then the result is true
```

## Final Thoughts
Once a solid bracket handling was in place things started to get swift. From this point on it was easy to have nested expressions, more complex operators and functions. The implementation was really fun but at this point my work here comes to an end. For anything more I would leave this project behind and look for something more powerful - like a parser generator.