using Kae.CIM;
using Kae_XTUML_Tools_MetaModelGenerator.XTUMLOOAofOOAParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.XTUML.Tools.MetaModelGenerator
{
    class CIInstancesLoader
    {
        string domainName;
        XTUMLOOAofOOAParserParser parser;
        OOAofOOAModelBuilder modelBuilder;
        CIModelRepository modelRepository;
        Dictionary<string, ImportStatus> importedClasses = new Dictionary<string, ImportStatus>();

        public CIInstancesLoader(XTUMLOOAofOOAParserParser parser, OOAofOOAModelBuilder builder, string domainName, CIModelRepository repository)
        {
            this.parser = parser;
            this.modelBuilder = builder;
            this.domainName = domainName;
            this.modelRepository = repository;
            builder.RegisterFondInsertHandler(this.RegisterInsert);
        }

        public Dictionary<string, ImportStatus> Load(string instancesPath, string fileExt = ".xtuml", bool clearImportedClasses = false)
        {
            if (clearImportedClasses)
            {
                importedClasses.Clear();
            }
            if (File.Exists(instancesPath) && instancesPath.EndsWith(fileExt))
            {
                Console.WriteLine($"Loading definitions in {instancesPath}");
                using (var stream = File.OpenRead(instancesPath))
                {
                    Load(stream, clearImportedClasses);
                }

            }
            else if (Directory.Exists(instancesPath))
            {
                var di = new DirectoryInfo(instancesPath);
                foreach (var cf in di.GetFiles())
                {
                    Load(cf.FullName, fileExt, clearImportedClasses);
                }
                foreach (var cd in di.GetDirectories())
                {
                    Load(cd.FullName);
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("insntacesPath should direct existing file or directory");
            }
            return importedClasses;
        }

        public Dictionary<string, ImportStatus> Load(Stream instanceDefs, bool clearImportedClasses = false)
        {
            if (clearImportedClasses)
            {
                importedClasses.Clear();
            }
            using (var reader = new StreamReader(instanceDefs))
            {
                modelBuilder.ResetAttributeValues();
                var content = reader.ReadToEnd();
                parser.Parse(content);
            }
            return importedClasses;
        }

        private void RegisterInsert(string objName, IList<string> attrValues)
        {
            bool imported = false;
            var classCandidate = modelBuilder.Repository.Classes.Values.Where(c => c.Name == objName);
            if (classCandidate.Count() > 0)
            {
                var classDef = classCandidate.First();
                var classAttrValues = new Dictionary<string, object>();
                if (attrValues.Count != classDef.Attributes.Count)
                {
                    if (attrValues.Count > classDef.Attributes.Count)
                    {
                        Console.WriteLine("Odd stiation!");
                    }
                }
                for(int i = 0; i < attrValues.Count; i++)
                {
                    var attrName = classDef.Attributes.Keys.ElementAt(i);
                    var attrTypeName = classDef.Attributes[attrName];
                    var codeTypeName = modelBuilder.Repository.DataTypes[attrTypeName].CodeTypeName;
                    switch (codeTypeName)
                    {
                        case "string":
                            classAttrValues.Add(attrName, attrValues.ElementAt(i));
                            break;
                        case "int":
                            classAttrValues.Add(attrName, int.Parse(attrValues.ElementAt(i)));
                            break;
                        case "bool":
                            bool bVal = false;
                            if (bool.TryParse(attrValues.ElementAt(i),out bVal))
                            {
                                classAttrValues.Add(attrName, bVal);
                            }else
                            {
                                if (attrValues.ElementAt(i) == "0")
                                {
                                    classAttrValues.Add(attrName, false);
                                }
                                else
                                {
                                    classAttrValues.Add(attrName, true);
                                }
                            }
                            break;
                        default:
                            classAttrValues.Add(attrName, attrValues.ElementAt(i));
                            break;
                    }
                }
                for (int i = attrValues.Count; i < classDef.Attributes.Count; i++)
                {
                    var attrName = classDef.Attributes.Keys.ElementAt(i);
                    var attrTypeName = classDef.Attributes[attrName];
                    var codeTypeName = modelBuilder.Repository.DataTypes[attrTypeName].CodeTypeName;
                    switch (codeTypeName)
                    {
                        case "string":
                            classAttrValues.Add(attrName, "");
                            break;
                        case "int":
                            classAttrValues.Add(attrName, 0);
                            break;
                        case "bool":
                            classAttrValues.Add(attrName, false);
                            break;
                    }
                }
                CIClassDef newInstance = modelRepository.CreateCIInstance(domainName, objName, classAttrValues);
                if (newInstance != null)
                {
                    imported = true;
                }
            }
            if (!importedClasses.ContainsKey(objName))
            {
                importedClasses.Add(objName, new ImportStatus() { ClassName = objName, IsImported = imported, Count = 0 });
            }
            if (importedClasses[objName].IsImported != imported)
            {
                Console.WriteLine("Odd situation!");
            }
            else
            {
                importedClasses[objName].Count++;
            }
        }

        public class ImportStatus
        {
            public string ClassName { get; set; }
            public bool IsImported { get; set; }
            public int Count { get; set; }
        }
    }
}