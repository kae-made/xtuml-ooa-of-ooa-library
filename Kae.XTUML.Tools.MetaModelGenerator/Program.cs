// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.XTUML.Tools.MetaModelGenerator.XTUMLOOAofOOA;
using Kae_XTUML_Tools_MetaModelGenerator.XTUMLOOAofOOAParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

                    if (commandLine.GenerateFWLib && !string.IsNullOrEmpty(commandLine.GenFolderPath))
                    {
                        var generator = new COCLibGenerator(builder.Repository, commandLine.GenFolderPath);
                        generator.Generate().Wait();
                    }

                    if (!string.IsNullOrEmpty(commandLine.InstancesFile))
                    {
                        var cimModelBuilder = new Kae.CIM.CIModelRepositoryBuilder();
                        var cimModelRepository = cimModelBuilder.CreateModelRepository();
                        var ciInstanceLoader = new CIInstancesLoader(parser, builder, "OOAofOOA", cimModelRepository);
                        var loadResult = ciInstanceLoader.Load(commandLine.InstancesFile);

                        var importedInstances = loadResult.Values.Where(i => i.IsImported);
                        int importedInstancesCount = 0;
                        Console.WriteLine("CIClasses for imported Instances : ");
                        foreach(var ii in importedInstances)
                        {
                            Console.WriteLine($"  {ii.ClassName} - {ii.Count}");
                            importedInstancesCount += ii.Count;
                        }
                        var unimportedInstances = loadResult.Values.Where(i => i.IsImported == false);
                        int unimportedInstancesCount = 0;
                        Console.WriteLine("CIClasses for unimporeted Instances because the CIClass is undefined : ");
                        foreach(var ui in unimportedInstances)
                        {
                            Console.WriteLine($"  {ui.ClassName} {ui.Count}");
                            unimportedInstancesCount += ui.Count;
                        }

                        Console.WriteLine("");
                        Console.WriteLine($"Imporeted Instances - {importedInstancesCount}");
                        Console.WriteLine($"Unimported Instances - {unimportedInstancesCount}");
                    }
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
        public bool GenerateFWLib { get; set; }
        public string InstancesFile { get; set; }

        public bool Parse(string [] args)
        {
            bool result = true;
            ModelFile = null;
            GenFolderPath = null;
            GenerateFWLib = false;
            InstancesFile = null;
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
                else if (args[index]=="-gf" || args[index] == "--gen-fwlib")
                {
                    if (args[index] == "-gf")
                    {
                        if (++index < args.Length)
                        {
                            if (args[index] == "yes")
                            {
                                GenerateFWLib = true;
                            }
                        }
                    }
                    else
                    {
                        GenerateFWLib = true;
                    }
                }
                else if (args[index] == "-li" || args[index] == "--load-instances")
                {
                    if (++index < args.Length)
                    {
                        InstancesFile = args[index];
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
