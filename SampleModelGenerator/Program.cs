using Kae.XTUML.Tools.CIModelResolver;
using System;

namespace SampleModelGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var commandLine = new CommandLine();

            if (commandLine.Parse(args))
            {
                Console.WriteLine($"Meta Model File : {commandLine.MetaModelFilePath}");
                Console.WriteLine($"Data Type Def File : {commandLine.DataTypeDefFilePath}");
                if (commandLine.GenerateFWLib)
                {
                    Console.WriteLine("Meta Model Framework Generation : yes");
                    Console.WriteLine($"  Generation Folder : {commandLine.GenFolderPath}");
                }
                if (!string.IsNullOrEmpty(commandLine.InstancesFile))
                {
                    Console.WriteLine($"Instances File Path : {commandLine.InstancesFile}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(commandLine.GetCommandLine());
                return;
            }

            var recognizer = new ConceptualInformationModelResolver(commandLine.MetaModelFilePath);
            try
            {
                Console.WriteLine($"Loading OOA of OOA model... @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                recognizer.LoadOOAofOOA(commandLine.DataTypeDefFilePath);
                Console.WriteLine($"Loaded.  @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");

                if (commandLine.GenerateFWLib && !string.IsNullOrEmpty(commandLine.GenFolderPath))
                {
                    Console.WriteLine($"Generating Meta Model Framework Library...  @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                    recognizer.GenerateCIMFramework(commandLine.GenFolderPath).Wait();
                    Console.WriteLine($"Generated.   @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                }
                if (!string.IsNullOrEmpty(commandLine.InstancesFile))
                {
                    Console.WriteLine($"Loading instances...   @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                    recognizer.LoadCIInstances(commandLine.InstancesFile, true);
                    Console.WriteLine($"Loaded.   @{DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss")}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            var ciinstances = recognizer.ModelRepository.GetDomainCIClasses("OOAofOOA");
            Console.WriteLine($"Count - {ciinstances.Keys.Count}");
        }
    }

    class CommandLine
    {
        public string MetaModelFilePath { get; set; }
        public string GenFolderPath { get; set; }
        public bool GenerateFWLib { get; set; }
        public string InstancesFile { get; set; }
        public bool BuildFWLib { get; set; }
        public string DataTypeDefFilePath { get; set; }

        public bool Parse(string[] args)
        {
            bool result = true;
            MetaModelFilePath = null;
            GenFolderPath = null;
            GenerateFWLib = false;
            InstancesFile = null;
            BuildFWLib = false;
            int index = 0;
            while (index < args.Length)
            {
                if (args[index] == "-m" || args[index] == "--model")
                {
                    if (++index < args.Length)
                    {
                        MetaModelFilePath = args[index];
                    }
                }
                else if (args[index] == "-o" || args[index] == "--out")
                {
                    if (++index < args.Length)
                    {
                        GenFolderPath = args[index];
                    }
                }
                else if (args[index] == "-gf" || args[index] == "--gen-fwlib")
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
                else if (args[index] == "-b" || args[index] == "--build")
                {
                    BuildFWLib = true;
                }
                else if (args[index] == "-li" || args[index] == "--load-instances")
                {
                    if (++index < args.Length)
                    {
                        InstancesFile = args[index];
                    }
                }
                else if (args[index] == "-dt" || args[index] == "--data-type")
                {
                    if (++index < args.Length)
                    {
                        DataTypeDefFilePath = args[index];
                    }
                }
                index++;
            }
            if (string.IsNullOrEmpty(MetaModelFilePath) || string.IsNullOrEmpty(DataTypeDefFilePath))
            {
                result = false;
            }
            if (GenerateFWLib && string.IsNullOrEmpty(GenFolderPath))
            {
                result = false;
            }
            if (GenerateFWLib == false)
            {
                BuildFWLib = false;
            }
            return result;
        }

        public string GetCommandLine()
        {
            return "--model model_file_path --data-type datatype_def_file_path [--gen-fwlib|--gf (yes|no) --out gen_folder_path [-b|--build]] [-li|--looad-instanes instances_file_or_directory_path]";
        }
    }

}
