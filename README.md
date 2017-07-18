# Web Surface Plots
# Using D3.js

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

We need the postfix for easy evaluation of the expression. The priority of the operators is no longer relevant. The expression may be evaluated by making a left to right scan, stacking operands, and evaluating operators using the correct number from the stack and finally placing the the result onto the stack.


