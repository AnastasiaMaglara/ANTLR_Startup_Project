grammar first;

compileUnit
		: (expr ';')+
		;

expr
	: NUMBER
	| expr op=('*'|'/') expr
	| expr op=('+'|'-') expr
	;

NUMBER
	: '0'|[1-9][0-9]*
	;

WS
	: ' '->skip
	;

