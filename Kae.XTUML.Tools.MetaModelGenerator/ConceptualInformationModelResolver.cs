﻿// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.CIM;
using Kae.Utility.Logging;
using Kae.XTUML.Tools.CIModelResolver;
using Kae.XTUML.Tools.CIModelResolver.XTUMLOOAofOOA;
using Kae_XTUML_Tools_MetaModelGenerator.XTUMLOOAofOOAParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kae.XTUML.Tools.CIModelResolver.CIInstancesLoader;

namespace Kae.XTUML.Tools.CIModelResolver
{
    public class ConceptualInformationModelResolver
    {
        private OOAofOOARepository metaModelRepository;
        private CIModelRepository ciModelRepository;
        private string sqlOOAofOOAFilePath;
        private OOAofOOAModelBuilder modelBuilder;
        private XTUMLOOAofOOAParserParser modelParser;
        private Dictionary<string, ImportStatus> importResult;
        private Logger logger;

        public OOAofOOARepository MetaModelRepository { get { return metaModelRepository; } }
        public CIModelRepository ModelRepository { get { return ciModelRepository; } }

        public IDictionary<string,  ImportStatus> ImportResult => importResult;

        public ConceptualInformationModelResolver(Logger logger)
        {
            this.logger = logger;
            metaModelRepository = new OOAofOOARepository()
            {
                Classes = new Dictionary<string, ClassOfOOA>(),
                Relationships = new Dictionary<string, RelationshipOfOOA>(),
                DataTypes = new Dictionary<string, DataTypeOfOOA>()
            };
            modelBuilder = new OOAofOOAModelBuilder(logger) { Repository = metaModelRepository };
            modelParser = new XTUMLOOAofOOAParserParser(modelBuilder, logger);
        }

        public void LoadOOAofOOA(string datatypeDefFilePath, string metaModelFilePath)
        {
            this.sqlOOAofOOAFilePath = metaModelFilePath;
            modelBuilder.LoadDataTypeDef(datatypeDefFilePath);
            using (var reader = new StreamReader(sqlOOAofOOAFilePath))
            {
                var content = reader.ReadToEnd();
                modelParser.Parse(content, reader.CurrentEncoding);

                modelBuilder.PickupDataType();
            }
        }

        public async Task GenerateCIMFramework(string generateFolderPath, bool isBuild = false)
        {
            var generator = new COCLibGenerator(modelBuilder.Repository, generateFolderPath);
            await generator.Generate();

            // TODO : Build generated Framework Library
        }

        public void LoadCIInstances(string[] instancesModelPaths, bool isShowDetail = false)
        {
            var ciModelBuilder = new Kae.CIM.CIModelRepositoryBuilder();
            ciModelRepository = ciModelBuilder.CreateModelRepository();
            var ciInstanceLoader = new CIInstancesLoader(modelParser, modelBuilder, "OOAofOOA", ciModelRepository);

            foreach (var instancesModelPath in instancesModelPaths)
            {
                importResult = ciInstanceLoader.Load(instancesModelPath);

                var importedInstances = importResult.Values.Where(i => i.IsImported);
                int importedInstancesCount = 0;
                Console.WriteLine("CIClasses for imported Instances : ");
                if (logger != null)
                {
                    logger.LogInfo("CIClasses for imported Instances : ");
                }
                foreach (var ii in importedInstances)
                {
                    if (isShowDetail)
                        Console.WriteLine($"  {ii.ClassName} - {ii.Count}");
                    importedInstancesCount += ii.Count;
                }
                var unimportedInstances = importResult.Values.Where(i => i.IsImported == false);
                int unimportedInstancesCount = 0;
                if (unimportedInstances != null && unimportedInstances.Count() > 0) {
                    Console.WriteLine("CIClasses for unimporeted Instances because the CIClass is undefined : ");
                    if (logger != null)
                    {
                        logger.LogInfo("CIClasses for unimporeted Instances because the CIClass is undefined : ");
                    }
                    foreach (var ui in unimportedInstances)
                    {
                        if (isShowDetail)
                            Console.WriteLine($"  {ui.ClassName} {ui.Count}");
                        unimportedInstancesCount += ui.Count;
                    }

                    Console.WriteLine("");
                    Console.WriteLine($"Imporeted Instances - {importedInstancesCount}");
                    Console.WriteLine($"Unimported Instances - {unimportedInstancesCount}");
                    if (logger != null)
                    {
                        logger.LogInfo($"Imporeted Instances - {importedInstancesCount}");
                        logger.LogInfo($"Unimported Instances - {unimportedInstancesCount}");
                    }
                }
            }
        }
    }
}
