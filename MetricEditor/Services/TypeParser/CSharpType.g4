grammar CSharpType;

/*
 * Parser Rules
 */

compileUnit
	: tp=type EOF
	;

type
	: (namespace=ns DOT)? name=NAME extension=ext?
	;

qualifiedType
	: LBRACKET tpe=type (COMMA (~RBRACKET)+)? RBRACKET
	;

qualifiedTypes
	: tp=qualifiedType COMMA tps=qualifiedTypes #typesMultiple
	| tpe=qualifiedType                         #typesSingle
	;

ns
	: next=ns DOT name1=NAME #namespaceMultiple
	| name2=NAME             #namespaceSingle
	;

ext
	: args      #extensionArguments
	| arrays    #extensionArrays
	;

args
	:  LBRACKET tps=qualifiedTypes RBRACKET arrs=arrays?
	;

arrays
	: array arrs=arrays #arrayMultiple
	| array             #arraySingle
	;

array
	: LBRACKET RBRACKET
	;

/*
 * Lexer Rules
 */

NUMBER: [0-9]+;
DOT: '.';
LBRACKET: '[';
RBRACKET: ']';
COMMA: ',';
STRESS: '`';
NAME:  [a-zA-Z_][a-zA-Z_0-9]* (STRESS NUMBER)?;
ERROR: .;