// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.Utility.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.XTUML.Tools.CIModelResolver
{
    public class COCLibGenerator
    {
        protected static readonly string version = "0.1.0";
        protected Logger logger;
        protected XTUMLOOAofOOA.OOAofOOARepository modelRepository;
        protected string GenFolderPath;

        protected static readonly string ciClassDefsFileName="CIClassDefs.cs";
        protected static readonly string ciClassBasesFileName = "CIClassBases.cs";
        protected static readonly string ciModelRepositoryImplFileName = "CIModelRepositoryImpl.cs";
        protected static readonly string ciOOAofOOAClassFileName = "CIMOOAofOOAClass.cs";

        protected static readonly string ciDomainName = "OOAofOOA";

        public COCLibGenerator(XTUMLOOAofOOA.OOAofOOARepository repository, string genFolderPath)
        {
            this.modelRepository = repository;
            this.GenFolderPath = genFolderPath;
        }

        public async Task Generate()
        {
            await GenerateCIClassDef();
        }

        public async Task GenerateCIClassDef()
        {
            var ooaGenerator = new template.CIMOOAofOOAClass(version, ciDomainName);
            var ooaContent = ooaGenerator.TransformText();
            await WriteToFileAsync(GenFolderPath, ciOOAofOOAClassFileName, ooaContent);
            Console.WriteLine($"Generated - {ciOOAofOOAClassFileName}");

            var ifGenerator = new template.CIMClassInterface(version, modelRepository);
            //generator.Prototype();
            var content = ifGenerator.TransformText();
            await WriteToFileAsync(GenFolderPath, ciClassDefsFileName, content);
            Console.WriteLine($"Generated - {ciClassDefsFileName}");

            var baseGenerator = new template.CIMClassBase(version, modelRepository, template.RuleOfNamesForTransfrom.CIMDomainName, template.RuleOfNamesForTransfrom.CIModelRepositoryMemberName);
            // baseGenerator.prototype();
            var baseContent = baseGenerator.TransformText();
            await WriteToFileAsync(GenFolderPath, ciClassBasesFileName, baseContent);
            Console.WriteLine($"Generated - {ciClassBasesFileName}");

        }

        protected async Task WriteToFileAsync(string genFolderPath, string fileName, string content, bool overwrite = true)
        {
            string fileAbsolutePath = Path.Join(genFolderPath, fileName);
            bool isUpdate = true;
            if (overwrite == false)
            {
                if (File.Exists(fileAbsolutePath))
                {
                    isUpdate = false;
                }
            }
            if (isUpdate)
            {
                using (var writer = new StreamWriter(fileAbsolutePath))
                {
                    await writer.WriteAsync(content);
                    await writer.FlushAsync();
                }
                logger?.LogInfo($"generated {fileName}");
            }
        }

    }
}
