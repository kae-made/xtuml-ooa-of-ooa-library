// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.XTUML.Tools.MetaModelGenerator.XTUMLOOAofOOA;
using Kae_XTUML_Tools_MetaModelGenerator.XTUMLOOAofOOAParser;
using System;
using System.Collections.Generic;
using System.IO;

namespace Kae.XTUML.Tools.MetaModelGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("command filename.sql");
                return;
            }

            var repository = new OOAofOOARepository()
            {
                Classes = new Dictionary<string, ClassOfOOA>(),
                Relationships = new Dictionary<string, RelationshipOfOOA>(),
                DataTypes = new Dictionary<string, DataTypeOfOOA>()
            };

            var builder = new OOAofOOAModelBuilder() { Repository = repository };

            var scanner = new XTUMLOOAofOOAParserScanner();
            var parser = new XTUMLOOAofOOAParserParser(builder);

            try
            {
                using(var fs = new StreamReader(args[0]))
                {
                    var content = fs.ReadToEnd();
                    parser.Parse(content);

                    builder.PickupDataType();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
