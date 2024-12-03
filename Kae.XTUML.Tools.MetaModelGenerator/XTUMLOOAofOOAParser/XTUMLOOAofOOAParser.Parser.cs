// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.CIM;
using Kae.Utility.Logging;
using Kae.XTUML.Tools.CIModelResolver;
using Kae.XTUML.Tools.CIModelResolver.XTUMLOOAofOOA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Kae_XTUML_Tools_MetaModelGenerator.XTUMLOOAofOOAParser
{
    internal partial class XTUMLOOAofOOAParserParser
    {
        private Logger logger;

        public XTUMLOOAofOOAParserParser(OOAofOOAModelBuilder builder, Logger logger) : base(null)
        {
            this.logger = logger;
            modelBuilder = builder;
        }

        public void Parse(string s, Encoding currentEncoding)
        {
            byte[] inputBuffer = currentEncoding.GetBytes(s);
            //string suc = System.Text.Encoding.Unicode.GetString(System.Text.Encoding.Convert(currentEncoding,System.Text.Encoding.Unicode,inputBuffer));
            //inputBuffer = System.Text.Encoding.Unicode.GetBytes(suc);
            MemoryStream stream = new MemoryStream(inputBuffer);
            try
            {
                this.Scanner = new XTUMLOOAofOOAParserScanner(stream);
                this.Parse();
            }
            catch (Exception ex)
            {
                if (logger != null)
                {
                    logger.LogError($"{ex.Message}");
                    if (ex is AggregateException)
                    {
                        foreach (var innerEx in ((AggregateException)ex).InnerExceptions)
                        {
                            logger.LogError($"{innerEx.Message}");
                        }
                    }
                }
            }
        }

        private OOAofOOAModelBuilder modelBuilder;

        private void RegisterElement(string element)
        {
            modelBuilder.RegisterElement(element);
        }

        private void AddAttribute(string datatype, string attrName)
        {
            modelBuilder.AddAttribute(datatype, attrName);
        }

        private void RegisterObject(string objName)
        {
            modelBuilder.RegisterObject(objName);
        }

        private void RegisterRelationship(string relId)
        {
            modelBuilder.RegisterRelationship(relId);
        }

        private void AddRefAttribute(string refAttrName)
        {
            modelBuilder.AddRefAttribute(refAttrName);
        }

        private void RegisterPhrase(string phrase)
        {
            if (phrase.StartsWith("'") && phrase.EndsWith("'"))
            {
                phrase = phrase.Substring(1, phrase.Length - 2);
            }
            var regex = new Regex("''");
            phrase = regex.Replace(phrase, "'");
            modelBuilder.RegisterPhrase(phrase);
        }

        private void AddEdgeDef(string objName, string multi)
        {
            var emulti = RelationshpEdgeOfOOA.ConvertTo(multi);
            modelBuilder.RegisterRelEdge(emulti, objName);
        }

        private void RegisterFromEdge()
        {
            modelBuilder.RegisterFromRelEdge();
        }

        private void RegisterToEdge()
        {
            modelBuilder.RegisterToRelEdge();
        }

        // for Loading Conceptual Instance
        private void RegisterInsert(string objName)
        {
            modelBuilder.RegisterInsert(objName);
        }

        private void AddAttrbuteValue(string attrValue)
        {
            modelBuilder.AddAttributeValue(attrValue);
        }

        private void AddAttrPhrase(string phrase)
        {
            modelBuilder.AddAttrPhrase(phrase);
        }
    }
}
