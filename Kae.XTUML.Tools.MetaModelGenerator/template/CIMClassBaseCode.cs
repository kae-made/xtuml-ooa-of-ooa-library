// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.XTUML.Tools.MetaModelGenerator.template
{
    partial class CIMClassBase
    {
        private string version;
        XTUMLOOAofOOA.OOAofOOARepository repository;
        private string cimAbstractClassDefClassName = RuleOfNamesForTransfrom.CIMAbstractClassDefClassName;
        private string ciModelRepsositoryClassName = RuleOfNamesForTransfrom.CIModelRepositoryClassName;
        private string modelRepositoryMemberName;
        private string domainName;

        public CIMClassBase(string version, XTUMLOOAofOOA.OOAofOOARepository repository, string domainName, string modelRepositoryMemberName)
        {
            this.version = version;
            this.repository = repository;
            this.domainName = domainName;
            this.modelRepositoryMemberName = modelRepositoryMemberName;
        }

        public string BuildConstructorAttrArgs(XTUMLOOAofOOA.ClassOfOOA classDef)
        {
            string args = "";
            foreach(var attrName in classDef.Attributes.Keys)
            {
                var attrTypeName = classDef.Attributes[attrName];
                string attrDataType = repository.DataTypes[attrTypeName].CodeTypeName;
                string argAttrVarName = attrName;
                if (RuleOfNamesForTransfrom.CheckPreservedWord(argAttrVarName))
                {
                    argAttrVarName += "_";
                }
                string frag = $"{attrDataType} {argAttrVarName}";
                if (string.IsNullOrEmpty(args))
                {
                    args = $"{frag}";
                }
                else
                {
                    args = $"{args}, {frag}";
                }
            }
            return args;
        }

        public string BuildThisConstructorAttrArgs(XTUMLOOAofOOA.ClassOfOOA classDef, string argName)
        {
            string args = "";
            foreach (var attrName in classDef.Attributes.Keys)
            {
                var attrTypeName = classDef.Attributes[attrName];
                string attrDataType = repository.DataTypes[attrTypeName].CodeTypeName;
                string frag = $"({attrDataType}){argName}[\"{attrName}\"]";
                if (string.IsNullOrEmpty(args))
                {
                    args = frag;
                }
                else
                {
                    args = $"{args}, {frag}";
                }
            }
            return args;
        }

        public void prototype()
        {
            var logicIndent = "            ";
            foreach (var ck in repository.Classes.Keys)
            {
                var classDef = repository.Classes[ck];
                var interfaceName = RuleOfNamesForTransfrom.GetCIMInterfaceClassName(classDef);
                var className = RuleOfNamesForTransfrom.GetCIMBaseClassName(classDef);
                
                foreach (var attrName in classDef.Attributes.Keys)
                {
                    var attrTypeName = repository.DataTypes[classDef.Attributes[attrName]].CodeTypeName;
                }

                var binaryRels = repository.Relationships.Values.Where(r => r.Kind == XTUMLOOAofOOA.KindOfRelatioship.Binary);
                var superSubRels = repository.Relationships.Values.Where(r => r.Kind == XTUMLOOAofOOA.KindOfRelatioship.SuperSub);
                var relObjRels = repository.Relationships.Values.Where(r => r.Kind == XTUMLOOAofOOA.KindOfRelatioship.RelationshipClass);

                var binaryRelFrom = binaryRels.Where(r => r.FromEdge.Edge.Name == ck);
                foreach (var brel in binaryRelFrom)
                {
                    var linkedToTypeName = RuleOfNamesForTransfrom.GetLinkedMethodReturnTypeName(brel,  RuleOfNamesForTransfrom.RelationshipEdgeSide.To);
                    var linkedToMethodName = RuleOfNamesForTransfrom.GetLinkedMethodName(brel, RuleOfNamesForTransfrom.RelationshipEdgeSide.To);

                    var logicGenerator = new LinkedItemLogic(logicIndent, brel, brel.FromEdge, brel.ToEdge, null);
                    var logic = logicGenerator.TransformText();
                }

                var binaryRelTo = binaryRels.Where(r => r.ToEdge.Edge.Name == ck);
                foreach (var brel in binaryRelTo)
                {
                    var linkedFromTypeName = RuleOfNamesForTransfrom.GetLinkedMethodReturnTypeName(brel, RuleOfNamesForTransfrom.RelationshipEdgeSide.From);
                    var linkedFromMethodName = RuleOfNamesForTransfrom.GetLinkedMethodName(brel,  RuleOfNamesForTransfrom.RelationshipEdgeSide.From);

                    var logicGenerator = new LinkedItemLogic(logicIndent, brel, brel.ToEdge, brel.FromEdge, null);
                    var logic = logicGenerator.TransformText();
                }

                var superSubRelSuper = superSubRels.Where(r => r.ToEdge.Edge.Name == ck);
                foreach (var superrel in superSubRelSuper)
                {
                    var subClassName = RuleOfNamesForTransfrom.GetCIMSubClassName(superrel);
                    var subClassGetMethodName = RuleOfNamesForTransfrom.GetCIMSubClassMethodName(superrel);

                    var ssRel = (XTUMLOOAofOOA.SuperSubRelationshipOfOOA)superrel;

                    var logicGenerator = new LinkedItemLogic(logicIndent, superrel, superrel.ToEdge, null, ssRel.SubEdges);
                    var logic = logicGenerator.TransformText();

                }

                foreach (var ssRel in superSubRels)
                {
                    var ssRelC = (XTUMLOOAofOOA.SuperSubRelationshipOfOOA)ssRel;
                    var superSubRelSub = ssRelC.SubEdges.Where(r => r.Edge.Name == ck);
                    foreach (var subrel in superSubRelSub)
                    {
                        var superClassName = RuleOfNamesForTransfrom.GetCIMInterfaceClassName(ssRel.ToEdge.Edge);
                        var getSuperClassMethodName = RuleOfNamesForTransfrom.GetCIMSuperClassMethodName(ssRel.ToEdge.Edge);

                        var logicGenerator = new LinkedItemLogic(logicIndent, ssRel, subrel, ssRel.ToEdge, null);
                        var logic = logicGenerator.TransformText();
                    }
                }

                var relObjRelOne = relObjRels.Where(r => ((XTUMLOOAofOOA.RelationshipClassRelationOfOOA)r).OneSideEdge.Edge.Name == ck);
                foreach (var onerel in relObjRelOne)
                {
                    var relobjrel = (XTUMLOOAofOOA.RelationshipClassRelationOfOOA)onerel;
                    var linkedOtherTypeName = RuleOfNamesForTransfrom.GetMethodReturnType(relobjrel.RelationshipClass, relobjrel.RelationshipClass.OneSideEdge.Mult);
                    var linkedOtherMethodName = RuleOfNamesForTransfrom.GetLinkedOOMethodName(relobjrel,  RuleOfNamesForTransfrom.RelationshipEdgeSide.OtherSide);

                    var logicGenerator = new LinkedItemLogic(logicIndent, onerel, relobjrel.OneSideEdge, relobjrel.RelationshipClass.OneSideEdge, null);
                    var logic = logicGenerator.TransformText();
                }


                var relObjRelOther = relObjRels.Where(r => ((XTUMLOOAofOOA.RelationshipClassRelationOfOOA)r).OtherSideEdge.Edge.Name == ck);
                foreach (var otherrel in relObjRelOther)
                {
                    var relobjrel = (XTUMLOOAofOOA.RelationshipClassRelationOfOOA)otherrel;
                    var linkedOneTypeName = RuleOfNamesForTransfrom.GetMethodReturnType(relobjrel.RelationshipClass, relobjrel.RelationshipClass.OtherSideEdge.Mult);
                    var linkedOneMethodName = RuleOfNamesForTransfrom.GetLinkedOOMethodName(relobjrel, RuleOfNamesForTransfrom.RelationshipEdgeSide.OneSide);

                    var logicGenerator = new LinkedItemLogic(logicIndent, relobjrel, relobjrel.OtherSideEdge, relobjrel.RelationshipClass.OtherSideEdge, null);
                    var logic = logicGenerator.TransformText();
                }

                if (classDef is XTUMLOOAofOOA.RelationshipClassOfOOA)
                {
                    var relClassDef = (XTUMLOOAofOOA.RelationshipClassOfOOA)classDef;
                    var linkedOneSideMethodName = RuleOfNamesForTransfrom.GetLinkedOOMethodName(relClassDef, RuleOfNamesForTransfrom.RelationshipEdgeSide.OneSide);
                    var linkedOneSideReturnTypeName = RuleOfNamesForTransfrom.GetMethodReturnType(relClassDef.Relationship.OneSideEdge.Edge, XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_1);

                    XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity currentMult = XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_1;
                    if (relClassDef.Relationship.OneSideEdge.Mult!= XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_1)
                    {
                        currentMult = relClassDef.Relationship.OneSideEdge.Mult;
                        relClassDef.Relationship.OneSideEdge.Mult = XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_1;
                    }
                    var logicGeneratorOne = new LinkedItemLogic(logicIndent, relClassDef.Relationship, relClassDef.OneSideEdge, relClassDef.Relationship.OneSideEdge, null);
                    var logicOne = logicGeneratorOne.TransformText();
                    if (currentMult != relClassDef.Relationship.OneSideEdge.Mult)
                    {
                        relClassDef.Relationship.OneSideEdge.Mult = currentMult;
                    }
                    

                    var linkedOtherSideMethodName = RuleOfNamesForTransfrom.GetLinkedOOMethodName(relClassDef,  RuleOfNamesForTransfrom.RelationshipEdgeSide.OtherSide);
                    var linkedOtherSideReturnTypeName = RuleOfNamesForTransfrom.GetMethodReturnType(relClassDef.Relationship.OtherSideEdge.Edge, XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_1);

                    if(relClassDef.Relationship.OtherSideEdge.Mult!= XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_1)
                    {
                        currentMult = relClassDef.OtherSideEdge.Mult;
                    }
                    currentMult = relClassDef.Relationship.OtherSideEdge.Mult;
                    relClassDef.Relationship.OtherSideEdge.Mult = XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_1;
                    var logicGeneratorOther = new LinkedItemLogic(logicIndent, relClassDef.Relationship, relClassDef.OtherSideEdge, relClassDef.Relationship.OtherSideEdge, null);
                    var logicOther = logicGeneratorOther.TransformText();
                    if (currentMult != relClassDef.OtherSideEdge.Mult)
                    {
                        relClassDef.Relationship.OtherSideEdge.Mult = currentMult;
                    }
                }
            }

        }

    }
}

