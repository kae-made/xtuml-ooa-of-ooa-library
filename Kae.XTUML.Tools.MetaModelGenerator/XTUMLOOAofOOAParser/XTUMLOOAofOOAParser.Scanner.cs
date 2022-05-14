// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Text;

namespace Kae_XTUML_Tools_MetaModelGenerator.XTUMLOOAofOOAParser
{
    internal partial class XTUMLOOAofOOAParserScanner
    {

        void GetNumber()
        {
            yylval.s = yytext;
            yylval.n = int.Parse(yytext);
        }

        void GetString()
        {
            yylval.s = yytext;
        }

        public override void yyerror(string format, params object[] args)
		{
			base.yyerror(format, args);
			Console.WriteLine(format, args);
			Console.WriteLine();
		}
    }
}
