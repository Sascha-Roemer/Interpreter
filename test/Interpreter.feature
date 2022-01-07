Feature: Interpreter

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