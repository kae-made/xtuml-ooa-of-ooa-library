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
%token CREATE, INSERT
%token TABLE
%token ROP, REFID, FROM, TO, PHRASE, INTO, VALUES
%token ELEM, REFNO, PHRASEC, ATTRVAL, ATTRSVAL, ATTRRVAL
%token REMULT
%token POpen, PClose
%token ATTRIVAL

%%

main   : line_list
       ;
line_list	: line
		| line line_list
	;

line		: statement EOS
	;

statement : CREATE create_statement { Console.WriteLine("Rule => create:"); } | insert_statement
	;

create_statement : table_statement | rop_statememnt
	;

table_statement : TABLE elem attributes_def
		{
			Console.WriteLine("Rule => table_def : {0}", $2.s);
			RegisterObject($2.s);
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

rel_edge_base_def :	rel_edge_base_args_def | rel_edge_base_none_def
	;

rel_edge_base_args_def :
		REMULT ELEM POpen ref_attr_defs PClose
		{
			Console.WriteLine("Rule => rel edge object: {0}, mult:{1}", $2.s, $1.s);
			AddEdgeDef($2.s, $1.s);
		}
	;

rel_edge_base_none_def :
		REMULT ELEM POpen PClose
		{
			Console.WriteLine("Rule => rel edge object: {0}, mult:{1}", $2.s, $1.s);
			AddEdgeDef($2.s, $1.s);
		}
	;

rel_edge_phrase:
		PHRASE ATTRVAL
		{
			Console.WriteLine("Rule => rel phrase: {0}", $2.s);
			RegisterPhrase($2.s);
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

insert_statement :	INSERT INTO ELEM VALUES POpen attrval_defs PClose
		{
			Console.WriteLine("Rule => insert into {0}", $3.s);
			RegisterInsert($3.s);
		}
	;

attrval_defs :		attrval_def | attrval_def COMMA attrval_defs
	;

attrval_def : attrval_string_def | attrval_dstring_def | attrval_integer_def | attrval_remult_def | attrval_real_def | attrval_phrase_def
		
	;

attrval_string_def :	ATTRVAL
		{
			Console.WriteLine("Rule => attr string value : {0}", $1.s);
			string val = $1.s;
			AddAttrbuteValue(val.Substring(1, val.Length - 2));
		}
	;

attrval_dstring_def :	ATTRSVAL
		{
			Console.WriteLine("Rule => attr unique string value : {0}", $1.s);
			string val = $1.s;
			AddAttrbuteValue(val.Substring(1, val.Length - 2));
		}
	;

attrval_integer_def : ATTRIVAL
		{
			Console.WriteLine("Rule => attr integer value : {0}", $1.s);
			AddAttrbuteValue($1.s);
		}
	;

attrval_real_def :	ATTRRVAL
		{
			Console.WriteLine("Rule => attr real value : {0}", $1.s);
			AddAttrbuteValue($1.s);
		}
	;

attrval_remult_def : REMULT
		{
			Console.WriteLine("Rule => attr 1 value : {0}", $1.s);
			AddAttrbuteValue($1.s);
		}
	;
attrval_phrase_def :	PHRASEC
		{
			Console.WriteLine("Rule => attr phrase value : {0}", $1.s);
			AddAttrbuteValue($1.s);
		}
	;
%%