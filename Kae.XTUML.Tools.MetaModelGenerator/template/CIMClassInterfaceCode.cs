// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.XTUML.Tools.CIModelResolver.template
{
    partial class CIMClassInterface
    {
        string version;
        XTUMLOOAofOOA.OOAofOOARepository repository;

        public CIMClassInterface(string version, XTUMLOOAofOOA.OOAofOOARepository repository)
        {
            this.version = version;
            this.repository = repository;
        }

        public void Prototype()
        {
            foreach (var ck in repository.Classes.Keys)
            {
                var classDef = repository.Classes[ck];
                var className = RuleOfNamesForTransfrom.GetCIMInterfaceClassName(classDef);
                
                foreach(var attrName in classDef.Attributes.Keys)
                {
                    var attrTypeName = repository.DataTypes[classDef.Attributes[attrName]].CodeTypeName;
                }

                var binaryRels = repository.Relationships.Values.Where(r => r.Kind == XTUMLOOAofOOA.KindOfRelatioship.Binary);
                var superSubRels = repository.Relationships.Values.Where(r => r.Kind == XTUMLOOAofOOA.KindOfRelatioship.SuperSub);
                var relObjRels = repository.Relationships.Values.Where(r => r.Kind == XTUMLOOAofOOA.KindOfRelatioship.RelationshipClass);

                var binaryRelFrom = binaryRels.Where(r => r.FromEdge.Edge.Name == ck);
                foreach( var brel in binaryRelFrom)
                {
                    var linkedToTypeName = RuleOfNamesForTransfrom.GetLinkedMethodReturnTypeName(brel,  RuleOfNamesForTransfrom.RelationshipEdgeSide.To);
                    var linkedToMethodName = RuleOfNamesForTransfrom.GetLinkedMethodName(brel, RuleOfNamesForTransfrom.RelationshipEdgeSide.To);

                }

                var binaryRelTo = binaryRels.Where(r => r.ToEdge.Edge.Name == ck);
                foreach(var brel in binaryRelTo)
                {
                    var linkedFromTypeName = RuleOfNamesForTransfrom.GetLinkedMethodReturnTypeName(brel, RuleOfNamesForTransfrom.RelationshipEdgeSide.From);
                    var linkedFromMethodName = RuleOfNamesForTransfrom.GetLinkedMethodName(brel, RuleOfNamesForTransfrom.RelationshipEdgeSide.From);
                }

                var superSubRelSuper = superSubRels.Where(r => r.ToEdge.Edge.Name == ck);
                foreach (var superrel in superSubRelSuper)
                {
                    var subClassName = RuleOfNamesForTransfrom.GetCIMSubClassName(superrel);
                    var subClassGetMethodName = RuleOfNamesForTransfrom.GetCIMSubClassMethodName(superrel);
                }

                foreach (var ssRel in superSubRels)
                {
                    var ssRelC = (XTUMLOOAofOOA.SuperSubRelationshipOfOOA)ssRel;
                    var superSubRelSub = ssRelC.SubEdges.Where(r => r.Edge.Name == ck);
                    foreach(var subrel in superSubRelSub)
                    {
                        var superClassName = RuleOfNamesForTransfrom.GetCIMInterfaceClassName(ssRel.ToEdge.Edge);
                        var getSuperClassMethodName = RuleOfNamesForTransfrom.GetCIMSuperClassMethodName(ssRel.ToEdge.Edge);
                    }
                }

                var relObjRelOne = relObjRels.Where(r => ((XTUMLOOAofOOA.RelationshipClassRelationOfOOA)r).OneSideEdge.Edge.Name == ck);
                foreach(var onerel in relObjRelOne)
                {
                    var relobjrel = (XTUMLOOAofOOA.RelationshipClassRelationOfOOA)onerel;
                    var linkedOtherTypeName = RuleOfNamesForTransfrom.GetMethodReturnType(relobjrel.RelationshipClass, relobjrel.OtherSideEdge.Mult);
                    var linkedOtherMethodName = RuleOfNamesForTransfrom.GetLinkedOOMethodName(relobjrel,  RuleOfNamesForTransfrom.RelationshipEdgeSide.OtherSide);
                }


                var relObjRelOther = relObjRels.Where(r => ((XTUMLOOAofOOA.RelationshipClassRelationOfOOA)r).OtherSideEdge.Edge.Name == ck);
                foreach(var otherrel in relObjRelOther)
                {
                    var relobjrel = (XTUMLOOAofOOA.RelationshipClassRelationOfOOA)otherrel;
                    var linkedOneTypeName = RuleOfNamesForTransfrom.GetMethodReturnType(relobjrel.RelationshipClass, relobjrel.OneSideEdge.Mult);
                    var linkedOneMethodName = RuleOfNamesForTransfrom.GetLinkedOOMethodName(relobjrel,  RuleOfNamesForTransfrom.RelationshipEdgeSide.OneSide);
                }

                if (classDef is XTUMLOOAofOOA.RelationshipClassOfOOA)
                {
                    var relClassDef = (XTUMLOOAofOOA.RelationshipClassOfOOA)classDef;
                    var linkedOneSideMethodName = RuleOfNamesForTransfrom.GetLinkedOOMethodName(relClassDef, RuleOfNamesForTransfrom.RelationshipEdgeSide.OneSide);
                    var linkedOneSideReturnTypeName = RuleOfNamesForTransfrom.GetMethodReturnType(relClassDef.Relationship.OneSideEdge.Edge, XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_1);

                    var linkedOtherSideMethodName = RuleOfNamesForTransfrom.GetLinkedOOMethodName(relClassDef, RuleOfNamesForTransfrom.RelationshipEdgeSide.OtherSide);
                    var linkedOtherSideReturnTypeName = RuleOfNamesForTransfrom.GetMethodReturnType(relClassDef.Relationship.OtherSideEdge.Edge, XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_1);
                }
            }

            foreach (var rk in repository.Relationships.Keys)
            {
                var relationship = repository.Relationships[rk];
                if (relationship is XTUMLOOAofOOA.SuperSubRelationshipOfOOA)
                {
                    var className = RuleOfNamesForTransfrom.GetCIMSubClassName(relationship);
                    var superClassName = RuleOfNamesForTransfrom.GetCIMInterfaceClassName(relationship.ToEdge.Edge);
                }
            }
        }
    }
}
