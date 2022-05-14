%namespace Kae_XTUML_Tools_MetaModelGenerator.XTUMLOOAofOOAParser
%partial
%parsertype XTUMLOOAofOOAParserParser
%visibility internal
%tokentype Token
%{
// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
%}

%union { 
			public int n; 
			public string s; 
	   }

%start main

%token NUMBER
%token COMMA
%token EOS
%token CREATE
%token TABLE
%token ROP, REFID, FROM, TO, PHRASE, SQT
%token ELEM, REFNO
%token REMULT
%token POpen, PClose

%%

main   : line_list
       ;
line_list	: line
		| line line_list
	;

line		: statement EOS
	;

statement : CREATE create_statement { Console.WriteLine("Rule => create:"); }
	;

create_statement : table_statement | rop_statememnt
	;

table_statement : TABLE elem attributes_def
		{
			Console.WriteLine("Rule => table_def : {0}", $1.s);
			RegisterObject($1.s);
		}
	;

rop_statememnt : ROP REFID REFNO rel_from_edge_def rel_to_edge_def
		{
			Console.WriteLine("Rule => rop_def : {0}", $3.s);
			RegisterRelationship($3.s);
		}
	;

attributes_def:	POpen attributes PClose
	;

attributes:	attribute | attribute COMMA attributes
	;

attribute : 
       | ELEM ELEM
		{
			Console.WriteLine("Rule -> attribute: {0}:{1}", $1.s, $2.s);
			AddAttribute($2.s, $1.s);
		}
       ;

elem :
	| ELEM
	{
		Console.WriteLine("Rule -> elem: {0}", $1.s);
		RegisterElement($1.s);
	}
	;

rel_from_edge_def :
		FROM rel_edge_def
		{
			Console.WriteLine("Rule => from def:");
			RegisterFromEdge();
		}
	;

rel_to_edge_def :
		TO rel_edge_def
		{
			Console.WriteLine("Rule => to def:");
			RegisterToEdge();
		}
	;



rel_edge_def :	rel_edge_base_def | rel_edge_base_def rel_edge_phrase
	;

rel_edge_base_def :
		REMULT ELEM POpen ref_attr_defs PClose
		{
			Console.WriteLine("Rule => rel edge object: {0}, mult:{1}", $2.s, $1.s);
			AddEdgeDef($2.s, $1.s);
		}
	;

rel_edge_phrase:
		PHRASE SQT ELEM SQT
		{
			Console.WriteLine("Rule => rel phrase: {0}", $3.s);
			RegisterPhrase($3.s);
		}
	;

ref_attr_defs :	ref_attr_def | ref_attr_def COMMA ref_attr_defs
	;

ref_attr_def :
		ELEM
		{
			Console.WriteLine("Rule => ref attr : {0}", $1.s);
			AddRefAttribute($1.s);
		}
	;
%%