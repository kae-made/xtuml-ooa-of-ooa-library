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
            var commandLine = new CommandLine();

            if (commandLine.Parse(args))
            {
                Console.WriteLine($"Model File : {commandLine.ModelFile}");
                Console.WriteLine($"Output : {commandLine.GenFolderPath}");
            }
            else
            {
                Console.WriteLine(commandLine.GetCommandLine());
                return;
            }

            var repository = new OOAofOOARepository()
            {
                Classes = new Dictionary<string, ClassOfOOA>(),
                Relationships = new Dictionary<string, RelationshipOfOOA>(),
                DataTypes = new Dictionary<string, DataTypeOfOOA>()
            };

            var builder = new OOAofOOAModelBuilder() { Repository = repository };
            builder.LoadDataTypeDef("datatype.yaml");

            var scanner = new XTUMLOOAofOOAParserScanner();
            var parser = new XTUMLOOAofOOAParserParser(builder);

            try
            {
                using(var fs = new StreamReader(commandLine.ModelFile))
                {
                    var content = fs.ReadToEnd();
                    parser.Parse(content);

                    builder.PickupDataType();

                    var generator = new COCLibGenerator(builder.Repository, commandLine.GenFolderPath);
                    generator.Generate().Wait();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    class CommandLine
    {
        public string ModelFile { get; set; }
        public string GenFolderPath { get; set; }

        public bool Parse(string [] args)
        {
            bool result = true;
            ModelFile = null;
            GenFolderPath = null;
            int index = 0;
            while (index < args.Length)
            {
                if (args[index]=="-m"|| args[index] == "--model")
                {
                    if (++index < args.Length)
                    {
                        ModelFile = args[index];
                    }
                }
                else if (args[index]=="-o"|| args[index] == "--out")
                {
                    if (++index < args.Length)
                    {
                        GenFolderPath = args[index];
                    }
                }
                index++;
            }
            if (string.IsNullOrEmpty(ModelFile)|| string.IsNullOrEmpty(GenFolderPath))
            {
                result = false;
            }
            return result;
        }

        public string GetCommandLine()
        {
            return "--model model_file_path --out gen_folder_path";
        }
    }
}
