%namespace Kae_XTUML_Tools_MetaModelGenerator.XTUMLOOAofOOAParser
%scannertype XTUMLOOAofOOAParserScanner
%visibility internal
%tokentype Token

%option stack, minimize, parser, verbose, persistbuffer, noembedbuffers 

COMMENT			--.*[\r\n|\n]
CREATE			CREATE
TABLE			TABLE
ROP				ROP
REFID			REF_ID
INSERT			INSERT
INTO			INTO
VALUES			VALUES
PHRASE			PHRASE 
REFNO			R[1-9][0-9]*
REMULT			[1M]C?
FROM			FROM
TO				TO
DSQ				__SQ__
Eol             (\r\n?|\n|\0)
PHRASEC			'[a-zA-z]([ a-zA-z0-9_]|\.|'')*'
ATTRSVAL		\"[^"]*\"
ATTRVAL			'[^']*'
ELEM			[a-zA-Z][a-zA-Z0-9_]*
ATTRIVAL		(0|[1-9][0-9]*)
ATTRRVAL		[0-9]+.[0-9]+
NotWh           [^ \t\r\n]
Space           [ \t]
COMMA			,
EOS				;
POpen			\(
PClose			\)

%{

// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

%}

%%

/* Scanner body */

{CREATE}		{ Console.WriteLine("create: {0}", yytext); return (int)Token.CREATE; }
{TABLE}			{ Console.WriteLine("table: {0}", yytext); return (int)Token.TABLE; }
{ROP}			{ Console.WriteLine("rop: {0}", yytext); return (int)Token.ROP; }
{REFID}			{ Console.WriteLine("refid: {0}", yytext); return (int)Token.REFID; }
{INSERT}		{ Console.WriteLine("insert: "); return (int)Token.INSERT; }
{INTO}			{ Console.WriteLine("into: "); return (int)Token.INTO; }
{VALUES}		{ Console.WriteLine("values: "); return (int)Token.VALUES; }
{ATTRVAL}		{ Console.WriteLine("attrval: {0}", yytext); GetString(); return (int)Token.ATTRVAL; }
{REFNO}			{ Console.WriteLine("fefno: {0}", yytext); GetString(); return (int)Token.REFNO; }
{REMULT}		{ Console.WriteLine("rel edge multi: {0}", yytext); GetString(); return (int)Token.REMULT; }
{FROM}			{ Console.WriteLine("from:"); return (int)Token.FROM; }
{TO}			{ Console.WriteLine("to:"); return (int)Token.TO; }
{DSQ}			{ Console.WriteLine("'' - double single quatation"); return (int)Token.DSQ; }
{PHRASE}		{ Console.WriteLine("phrase"); return (int)Token.PHRASE; }
{POpen}			{ Console.WriteLine("("); return (int)Token.POpen; }
{PClose}		{ Console.WriteLine(")"); return (int)Token.PClose; }
{ELEM}			{ Console.WriteLine("elem: {0}", yytext); GetString(); return (int)Token.ELEM; }
{COMMA}			{ Console.WriteLine("comma: {0}", yytext); return (int)Token.COMMA; }
{PHRASEC}		{ Console.WriteLine("phrase content : {0}", yytext); GetString(); return (int)Token.PHRASEC; }
{ATTRSVAL}		{ Console.WriteLine("attr string value : {0}", yytext); GetString(); return (int)Token.ATTRSVAL; }
{ATTRIVAL}		{ Console.WriteLine("attr integer value : {0}", yytext); GetString(); return (int)Token.ATTRIVAL; }
{ATTRRVAL}		{ Console.WriteLine("attr real value : {0}", yytext); GetString(); return (int)Token.ATTRRVAL; }
{EOS}			{ Console.WriteLine("eos: {0}", yytext); return (int)Token.EOS; }
{COMMENT}		{ Console.WriteLine("comment: {0}", yytext); }
{Space}+		/* skip */

%%