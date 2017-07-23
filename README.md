# Surface Plots in web using D3.js

Surface plots are diagrams of three-dimensional data. We focus o those that come from functions of 2 variables z=f(x,y)
For example f(x,y) = cos(x / 15 * 3.14) *cos(y / 15 * 3.14) * 60 + cos(x / 8 * 3.14) *cos(y / 10 * 3.14) * 40 is an expression of x,y.

A binary expression tree is a specific kind of a binary tree used to represent expressions. Two common types of expressions that a binary expression tree can represent are algebraic and boolean. These trees can represent expressions that contain both unary and binary operators.
[ https://en.wikipedia.org/wiki/Binary_expression_tree ]

After constructing an expression tree we need its postfix notation.

	Algorithm postfix (tree)	
	 if (tree not empty)
	    postfix (tree left subtree)
	    postfix (tree right subtree)
	    print (tree token)
	 end if
	end postfix

For example the expression tree that represents the expression
	(a+b)*c*(d+e) 
returns a postfix fnotation
	a b + c d e + * * 

We need the postfix for easy evaluation of the expression. The priority of the operators is no longer relevant. The expression may be evaluated by making a left to right scan, stacking operands, and evaluating operators using the correct number from the stack and finally placing the result onto the stack.

# Postfix algorithm

Before we use the D3.js we must generate the data. So there is a postfix calculator.
The algorithm for evaluating any postfix expression is fairly straightforward:

    While there are input tokens left
        Read the next token from input.
        If the token is a value
            Push it onto the stack.
        Otherwise, the token is an operator (operator here includes both operators and functions).
            It is already known that the operator takes n arguments.
            If there are fewer than n values on the stack
                (Error) The user has not input sufficient values in the expression.
            Else, Pop the top n values from the stack.
            Evaluate the operator, with the values as arguments.
            Push the returned results, if any, back onto the stack.
    If there is only one value in the stack
        That value is the result of the calculation.
    Otherwise, there are more values in the stack
        (Error) The user input has too many values.

