// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// DateTime: 2022/05/17 11:12:22
// Input file <XTUMLOOAofOOAParser\XTUMLOOAofOOAParser.Language.grammar.y - 2022/05/17 11:12:21>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace Kae_XTUML_Tools_MetaModelGenerator.XTUMLOOAofOOAParser
{
internal enum Token {error=2,EOF=3,NUMBER=4,COMMA=5,EOS=6,
    CREATE=7,TABLE=8,ROP=9,REFID=10,FROM=11,TO=12,
    PHRASE=13,ELEM=14,REFNO=15,PHRASEC=16,REMULT=17,POpen=18,
    PClose=19};

internal partial struct ValueType
{ 
			public int n; 
			public string s; 
	   }
// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
internal abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
internal class ScanObj {
  public int token;
  public ValueType yylval;
  public LexLocation yylloc;
  public ScanObj( int t, ValueType val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
internal partial class XTUMLOOAofOOAParserParser: ShiftReduceParser<ValueType, LexLocation>
{
  // Verbatim content from XTUMLOOAofOOAParser\XTUMLOOAofOOAParser.Language.grammar.y - 2022/05/17 11:12:21
// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
  // End verbatim content from XTUMLOOAofOOAParser\XTUMLOOAofOOAParser.Language.grammar.y - 2022/05/17 11:12:21

#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[30];
  private static State[] states = new State[49];
  private static string[] nonTerms = new string[] {
      "main", "$accept", "line_list", "line", "statement", "create_statement", 
      "table_statement", "rop_statememnt", "elem", "attributes_def", "rel_from_edge_def", 
      "rel_to_edge_def", "attributes", "attribute", "rel_edge_def", "rel_edge_base_def", 
      "rel_edge_phrase", "rel_edge_base_args_def", "rel_edge_base_none_def", 
      "ref_attr_defs", "ref_attr_def", };

  static XTUMLOOAofOOAParserParser() {
    states[0] = new State(new int[]{7,8},new int[]{-1,1,-3,3,-4,4,-5,6});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{7,8,3,-3},new int[]{-3,5,-4,4,-5,6});
    states[5] = new State(-4);
    states[6] = new State(new int[]{6,7});
    states[7] = new State(-5);
    states[8] = new State(new int[]{8,11,9,24},new int[]{-6,9,-7,10,-8,23});
    states[9] = new State(-6);
    states[10] = new State(-7);
    states[11] = new State(new int[]{14,22,18,-16},new int[]{-9,12});
    states[12] = new State(new int[]{18,14},new int[]{-10,13});
    states[13] = new State(-9);
    states[14] = new State(new int[]{14,20,5,-14,19,-14},new int[]{-13,15,-14,17});
    states[15] = new State(new int[]{19,16});
    states[16] = new State(-11);
    states[17] = new State(new int[]{5,18,19,-12});
    states[18] = new State(new int[]{14,20,5,-14,19,-14},new int[]{-13,19,-14,17});
    states[19] = new State(-13);
    states[20] = new State(new int[]{14,21});
    states[21] = new State(-15);
    states[22] = new State(-17);
    states[23] = new State(-8);
    states[24] = new State(new int[]{10,25});
    states[25] = new State(new int[]{15,26});
    states[26] = new State(new int[]{11,47},new int[]{-11,27});
    states[27] = new State(new int[]{12,29},new int[]{-12,28});
    states[28] = new State(-10);
    states[29] = new State(new int[]{17,36},new int[]{-15,30,-16,31,-18,35,-19,46});
    states[30] = new State(-19);
    states[31] = new State(new int[]{13,33,6,-20,12,-20},new int[]{-17,32});
    states[32] = new State(-21);
    states[33] = new State(new int[]{16,34});
    states[34] = new State(-26);
    states[35] = new State(-22);
    states[36] = new State(new int[]{14,37});
    states[37] = new State(new int[]{18,38});
    states[38] = new State(new int[]{19,41,14,45},new int[]{-20,39,-21,42});
    states[39] = new State(new int[]{19,40});
    states[40] = new State(-24);
    states[41] = new State(-25);
    states[42] = new State(new int[]{5,43,19,-27});
    states[43] = new State(new int[]{14,45},new int[]{-20,44,-21,42});
    states[44] = new State(-28);
    states[45] = new State(-29);
    states[46] = new State(-23);
    states[47] = new State(new int[]{17,36},new int[]{-15,48,-16,31,-18,35,-19,46});
    states[48] = new State(-18);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-2, new int[]{-1,3});
    rules[2] = new Rule(-1, new int[]{-3});
    rules[3] = new Rule(-3, new int[]{-4});
    rules[4] = new Rule(-3, new int[]{-4,-3});
    rules[5] = new Rule(-4, new int[]{-5,6});
    rules[6] = new Rule(-5, new int[]{7,-6});
    rules[7] = new Rule(-6, new int[]{-7});
    rules[8] = new Rule(-6, new int[]{-8});
    rules[9] = new Rule(-7, new int[]{8,-9,-10});
    rules[10] = new Rule(-8, new int[]{9,10,15,-11,-12});
    rules[11] = new Rule(-10, new int[]{18,-13,19});
    rules[12] = new Rule(-13, new int[]{-14});
    rules[13] = new Rule(-13, new int[]{-14,5,-13});
    rules[14] = new Rule(-14, new int[]{});
    rules[15] = new Rule(-14, new int[]{14,14});
    rules[16] = new Rule(-9, new int[]{});
    rules[17] = new Rule(-9, new int[]{14});
    rules[18] = new Rule(-11, new int[]{11,-15});
    rules[19] = new Rule(-12, new int[]{12,-15});
    rules[20] = new Rule(-15, new int[]{-16});
    rules[21] = new Rule(-15, new int[]{-16,-17});
    rules[22] = new Rule(-16, new int[]{-18});
    rules[23] = new Rule(-16, new int[]{-19});
    rules[24] = new Rule(-18, new int[]{17,14,18,-20,19});
    rules[25] = new Rule(-19, new int[]{17,14,18,19});
    rules[26] = new Rule(-17, new int[]{13,16});
    rules[27] = new Rule(-20, new int[]{-21});
    rules[28] = new Rule(-20, new int[]{-21,5,-20});
    rules[29] = new Rule(-21, new int[]{14});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Token.error, (int)Token.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 6: // statement -> CREATE, create_statement
{ Console.WriteLine("Rule => create:"); }
        break;
      case 9: // table_statement -> TABLE, elem, attributes_def
{
			Console.WriteLine("Rule => table_def : {0}", ValueStack[ValueStack.Depth-2].s);
			RegisterObject(ValueStack[ValueStack.Depth-2].s);
		}
        break;
      case 10: // rop_statememnt -> ROP, REFID, REFNO, rel_from_edge_def, rel_to_edge_def
{
			Console.WriteLine("Rule => rop_def : {0}", ValueStack[ValueStack.Depth-3].s);
			RegisterRelationship(ValueStack[ValueStack.Depth-3].s);
		}
        break;
      case 15: // attribute -> ELEM, ELEM
{
			Console.WriteLine("Rule -> attribute: {0}:{1}", ValueStack[ValueStack.Depth-2].s, ValueStack[ValueStack.Depth-1].s);
			AddAttribute(ValueStack[ValueStack.Depth-1].s, ValueStack[ValueStack.Depth-2].s);
		}
        break;
      case 17: // elem -> ELEM
{
		Console.WriteLine("Rule -> elem: {0}", ValueStack[ValueStack.Depth-1].s);
		RegisterElement(ValueStack[ValueStack.Depth-1].s);
	}
        break;
      case 18: // rel_from_edge_def -> FROM, rel_edge_def
{
			Console.WriteLine("Rule => from def:");
			RegisterFromEdge();
		}
        break;
      case 19: // rel_to_edge_def -> TO, rel_edge_def
{
			Console.WriteLine("Rule => to def:");
			RegisterToEdge();
		}
        break;
      case 24: // rel_edge_base_args_def -> REMULT, ELEM, POpen, ref_attr_defs, PClose
{
			Console.WriteLine("Rule => rel edge object: {0}, mult:{1}", ValueStack[ValueStack.Depth-4].s, ValueStack[ValueStack.Depth-5].s);
			AddEdgeDef(ValueStack[ValueStack.Depth-4].s, ValueStack[ValueStack.Depth-5].s);
		}
        break;
      case 25: // rel_edge_base_none_def -> REMULT, ELEM, POpen, PClose
{
			Console.WriteLine("Rule => rel edge object: {0}, mult:{1}", ValueStack[ValueStack.Depth-3].s, ValueStack[ValueStack.Depth-4].s);
			AddEdgeDef(ValueStack[ValueStack.Depth-3].s, ValueStack[ValueStack.Depth-4].s);
		}
        break;
      case 26: // rel_edge_phrase -> PHRASE, PHRASEC
{
			Console.WriteLine("Rule => rel phrase: {0}", ValueStack[ValueStack.Depth-1].s);
			RegisterPhrase(ValueStack[ValueStack.Depth-1].s);
		}
        break;
      case 29: // ref_attr_def -> ELEM
{
			Console.WriteLine("Rule => ref attr : {0}", ValueStack[ValueStack.Depth-1].s);
			AddRefAttribute(ValueStack[ValueStack.Depth-1].s);
		}
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Token)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Token)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

}
}
