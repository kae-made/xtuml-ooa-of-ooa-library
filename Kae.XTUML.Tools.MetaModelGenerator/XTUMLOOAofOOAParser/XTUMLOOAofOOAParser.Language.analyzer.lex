%namespace Kae_XTUML_Tools_MetaModelGenerator.XTUMLOOAofOOAParser
%scannertype XTUMLOOAofOOAParserScanner
%visibility internal
%tokentype Token

%option stack, minimize, parser, verbose, persistbuffer, noembedbuffers 

CREATE			CREATE
TABLE			TABLE
ROP				ROP
REFID			REF_ID
PHRASE			PHRASE 
REFNO			R[1-9][0-9]*
REMULT			[1M]C?
FROM			FROM
TO				TO
Eol             (\r\n?|\n|\0)
ELEM			[a-zA-Z][a-zA-Z0-9_]*
NotWh           [^ \t\r\n]
Space           [ \t]
COMMA			,
EOS				;
POpen			\(
PClose			\)
SQT				'

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
{REFNO}			{ Console.WriteLine("fefno: {0}", yytext); GetString(); return (int)Token.REFNO; }
{REMULT}		{ Console.WriteLine("rel edge multi: {0}", yytext); GetString(); return (int)Token.REMULT; }
{FROM}			{ Console.WriteLine("from:"); return (int)Token.FROM; }
{TO}			{ Console.WriteLine("to:"); return (int)Token.TO; }
{PHRASE}		{ Console.WriteLine("phrase"); return (int)Token.PHRASE; }
{POpen}			{ Console.WriteLine("("); return (int)Token.POpen; }
{PClose}		{ Console.WriteLine(")"); return (int)Token.PClose; }
{ELEM}			{ Console.WriteLine("elem: {0}", yytext); GetString(); return (int)Token.ELEM; }
{COMMA}			{ Console.WriteLine("comma: {0}", yytext); return (int)Token.COMMA; }
{SQT}			{ Console.WriteLine("sqt"); return (int)Token.SQT; }
{EOS}			{ Console.WriteLine("eos: {0}", yytext); return (int)Token.EOS; }
{Space}+		/* skip */

%%