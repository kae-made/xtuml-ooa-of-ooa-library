﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン: 16.0.0.0
//  
//     このファイルへの変更は、正しくない動作の原因になる可能性があり、
//     コードが再生成されると失われます。
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Kae.XTUML.Tools.MetaModelGenerator.template
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class CIMClassBase : CIMClassBaseBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            
            #line 1 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

  // Copyright (c) Knowledge & Experience. All rights reserved.
  // Licensed under the MIT license. See LICENSE file in the project root for full license information.

            
            #line default
            #line hidden
            this.Write("// ------------------------------------------------------------------------------" +
                    "\r\n// <auto-generated>\r\n//     This file is generated by tool.\r\n//     Runtime Ve" +
                    "rsion : ");
            
            #line 13 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(version));
            
            #line default
            #line hidden
            this.Write(@"
//  
//     Updates this file cause incorrect behavior 
//     and will be lost when the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System.Linq;
using System.Collections.Generic;

namespace Kae.CIM.MetaModel.CIMofCIM
{
");
            
            #line 24 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

    var logicIndent = "            ";
    foreach (var ck in repository.Classes.Keys)
    {
        var classDef = repository.Classes[ck];
        var interfaceName = RuleOfNamesForTransfrom.GetCIMInterfaceClassName(classDef);
        var className = RuleOfNamesForTransfrom.GetCIMBaseClassName(classDef);

            
            #line default
            #line hidden
            this.Write("    public class ");
            
            #line 32 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            
            #line default
            #line hidden
            this.Write(" : ");
            
            #line 32 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(cimAbstractClassDefClassName));
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 32 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(interfaceName));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n");
            
            #line 34 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

        
        foreach (var attrName in classDef.Attributes.Keys)
        {
            var attrTypeName = repository.DataTypes[classDef.Attributes[attrName]].CodeTypeName;
            var attrPropertyName = RuleOfNamesForTransfrom.GetAttrPropertyName(attrName);

            
            #line default
            #line hidden
            this.Write("        public ");
            
            #line 41 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attrTypeName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 41 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attrPropertyName));
            
            #line default
            #line hidden
            this.Write(" { get; set; }\r\n");
            
            #line 42 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

        }
        string constructorAttrArgs = BuildConstructorAttrArgs(classDef);
        string attrDictionary = "attrArgs";
        string constructorAttrDictArgs = BuildThisConstructorAttrArgs(classDef, attrDictionary);

            
            #line default
            #line hidden
            this.Write("\r\n        public ");
            
            #line 49 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 49 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ciModelRepsositoryClassName));
            
            #line default
            #line hidden
            this.Write(" repository, ");
            
            #line 49 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(constructorAttrArgs));
            
            #line default
            #line hidden
            this.Write(" ) : base(repository, \"");
            
            #line 49 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(classDef.Name));
            
            #line default
            #line hidden
            this.Write("\")\r\n        {\r\n            ;\r\n        }\r\n\r\n        public ");
            
            #line 54 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(className));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 54 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ciModelRepsositoryClassName));
            
            #line default
            #line hidden
            this.Write(" repository, IDictionary<string, object>");
            
            #line 54 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(attrDictionary));
            
            #line default
            #line hidden
            this.Write(" ) : this(repository, ");
            
            #line 54 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(constructorAttrDictArgs));
            
            #line default
            #line hidden
            this.Write(")\r\n        {\r\n            ;\r\n        }\r\n\r\n        public void Dispose()\r\n        " +
                    "{\r\n            ;\r\n        }\r\n\r\n        public bool Validate()\r\n        {\r\n      " +
                    "      return true;\r\n        }\r\n\r\n");
            
            #line 69 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"


        var binaryRels = repository.Relationships.Values.Where(r => r.Kind == XTUMLOOAofOOA.KindOfRelatioship.Binary);
        var superSubRels = repository.Relationships.Values.Where(r => r.Kind == XTUMLOOAofOOA.KindOfRelatioship.SuperSub);
        var relObjRels = repository.Relationships.Values.Where(r => r.Kind == XTUMLOOAofOOA.KindOfRelatioship.RelationshipClass);

        var binaryRelFrom = binaryRels.Where(r => r.FromEdge.Edge.Name == ck);
        foreach (var brel in binaryRelFrom)
        {
            var linkedToTypeName = RuleOfNamesForTransfrom.GetLinkedMethodReturnTypeName(brel, RuleOfNamesForTransfrom.RelationshipEdgeSide.To);
            var linkedToMethodName = RuleOfNamesForTransfrom.GetLinkedMethodName(brel, RuleOfNamesForTransfrom.RelationshipEdgeSide.To);

            
            #line default
            #line hidden
            this.Write("        public ");
            
            #line 81 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(linkedToTypeName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 81 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(linkedToMethodName));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n");
            
            #line 83 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"


            var logicGenerator = new LinkedItemLogic(logicIndent, brel, brel.FromEdge, brel.ToEdge, null);
            var logic = logicGenerator.TransformText();

            
            #line default
            #line hidden
            
            #line 88 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(logic));
            
            #line default
            #line hidden
            this.Write("\r\n        }\r\n\r\n");
            
            #line 91 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

        }

        var binaryRelTo = binaryRels.Where(r => r.ToEdge.Edge.Name == ck);
        foreach (var brel in binaryRelTo)
        {
            var linkedFromTypeName = RuleOfNamesForTransfrom.GetLinkedMethodReturnTypeName(brel, RuleOfNamesForTransfrom.RelationshipEdgeSide.From);
            var linkedFromMethodName = RuleOfNamesForTransfrom.GetLinkedMethodName(brel, RuleOfNamesForTransfrom.RelationshipEdgeSide.From);

            
            #line default
            #line hidden
            this.Write("        public ");
            
            #line 100 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(linkedFromTypeName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 100 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(linkedFromMethodName));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n");
            
            #line 102 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"


            var logicGenerator = new LinkedItemLogic(logicIndent, brel, brel.ToEdge, brel.FromEdge, null);
            var logic = logicGenerator.TransformText();

            
            #line default
            #line hidden
            
            #line 107 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(logic));
            
            #line default
            #line hidden
            this.Write("\r\n        }\r\n\r\n");
            
            #line 110 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

        }

        var superSubRelSuper = superSubRels.Where(r => r.ToEdge.Edge.Name == ck);
        foreach (var superrel in superSubRelSuper)
        {
            var subClassName = RuleOfNamesForTransfrom.GetCIMSubClassName(superrel);
            var subClassGetMethodName = RuleOfNamesForTransfrom.GetCIMSubClassMethodName(superrel);

            
            #line default
            #line hidden
            this.Write("        public ");
            
            #line 119 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(subClassName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 119 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(subClassGetMethodName));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n");
            
            #line 121 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"


            var ssRel = (XTUMLOOAofOOA.SuperSubRelationshipOfOOA)superrel;

            var logicGenerator = new LinkedItemLogic(logicIndent, superrel, superrel.ToEdge, null, ssRel.SubEdges);
            var logic = logicGenerator.TransformText();

            
            #line default
            #line hidden
            
            #line 128 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(logic));
            
            #line default
            #line hidden
            this.Write("\r\n        }\r\n\r\n");
            
            #line 131 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

        }

        foreach (var ssRel in superSubRels)
        {
            var ssRelC = (XTUMLOOAofOOA.SuperSubRelationshipOfOOA)ssRel;
            var superSubRelSub = ssRelC.SubEdges.Where(r => r.Edge.Name == ck);
            foreach (var subrel in superSubRelSub)
            {
                var superClassName = RuleOfNamesForTransfrom.GetCIMInterfaceClassName(ssRel.ToEdge.Edge);
                var getSuperClassMethodName = RuleOfNamesForTransfrom.GetCIMSuperClassMethodName(ssRel.ToEdge.Edge);

            
            #line default
            #line hidden
            this.Write("        public ");
            
            #line 143 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(superClassName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 143 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(getSuperClassMethodName));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n");
            
            #line 145 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

                var logicGenerator = new LinkedItemLogic(logicIndent, ssRel, subrel, ssRel.ToEdge, null);
                var logic = logicGenerator.TransformText();

            
            #line default
            #line hidden
            
            #line 149 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(logic));
            
            #line default
            #line hidden
            this.Write("\r\n        }\r\n");
            
            #line 151 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

            }
        }

        var relObjRelOne = relObjRels.Where(r => ((XTUMLOOAofOOA.RelationshipClassRelationOfOOA)r).OneSideEdge.Edge.Name == ck);
        foreach (var onerel in relObjRelOne)
        {
            var relobjrel = (XTUMLOOAofOOA.RelationshipClassRelationOfOOA)onerel;
            var linkedOtherTypeName = RuleOfNamesForTransfrom.GetMethodReturnType(relobjrel.RelationshipClass, relobjrel.RelationshipClass.OneSideEdge.Mult);
            var linkedOtherMethodName = RuleOfNamesForTransfrom.GetLinkedOOMethodName(relobjrel, RuleOfNamesForTransfrom.RelationshipEdgeSide.OtherSide);

            
            #line default
            #line hidden
            this.Write("        public ");
            
            #line 162 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(linkedOtherTypeName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 162 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(linkedOtherMethodName));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n");
            
            #line 164 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

            var logicGenerator = new LinkedItemLogic(logicIndent, onerel, relobjrel.OneSideEdge, relobjrel.RelationshipClass.OneSideEdge, null);
            var logic = logicGenerator.TransformText();

            
            #line default
            #line hidden
            
            #line 168 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(logic));
            
            #line default
            #line hidden
            this.Write("\r\n        }\r\n\r\n");
            
            #line 171 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

        }


        var relObjRelOther = relObjRels.Where(r => ((XTUMLOOAofOOA.RelationshipClassRelationOfOOA)r).OtherSideEdge.Edge.Name == ck);
        foreach (var otherrel in relObjRelOther)
        {
            var relobjrel = (XTUMLOOAofOOA.RelationshipClassRelationOfOOA)otherrel;
            var linkedOneTypeName = RuleOfNamesForTransfrom.GetMethodReturnType(relobjrel.RelationshipClass, relobjrel.RelationshipClass.OtherSideEdge.Mult);
            var linkedOneMethodName = RuleOfNamesForTransfrom.GetLinkedOOMethodName(relobjrel, RuleOfNamesForTransfrom.RelationshipEdgeSide.OneSide);

            
            #line default
            #line hidden
            this.Write("        public ");
            
            #line 182 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(linkedOneTypeName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 182 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(linkedOneMethodName));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n");
            
            #line 184 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

            var logicGenerator = new LinkedItemLogic(logicIndent, relobjrel, relobjrel.OtherSideEdge, relobjrel.RelationshipClass.OtherSideEdge, null);
            var logic = logicGenerator.TransformText();

            
            #line default
            #line hidden
            
            #line 188 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(logic));
            
            #line default
            #line hidden
            this.Write("\r\n        }\r\n");
            
            #line 190 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

        }

        if (classDef is XTUMLOOAofOOA.RelationshipClassOfOOA)
        {
            var relClassDef = (XTUMLOOAofOOA.RelationshipClassOfOOA)classDef;
            var linkedOneSideMethodName = RuleOfNamesForTransfrom.GetLinkedOOMethodName(relClassDef, RuleOfNamesForTransfrom.RelationshipEdgeSide.OneSide);
            var linkedOneSideReturnTypeName = RuleOfNamesForTransfrom.GetMethodReturnType(relClassDef.Relationship.OneSideEdge.Edge, XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_1);

            
            #line default
            #line hidden
            this.Write("         public ");
            
            #line 199 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(linkedOneSideReturnTypeName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 199 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(linkedOneSideMethodName));
            
            #line default
            #line hidden
            this.Write("()\r\n         {\r\n");
            
            #line 201 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

            var currentMult = relClassDef.Relationship.OneSideEdge.Mult;
            relClassDef.Relationship.OneSideEdge.Mult = XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_1;
            var logicGeneratorOne = new LinkedItemLogic(logicIndent, relClassDef.Relationship, relClassDef.OneSideEdge, relClassDef.Relationship.OneSideEdge, null);
            var logicOne = logicGeneratorOne.TransformText();
            relClassDef.Relationship.OneSideEdge.Mult = currentMult;

            
            #line default
            #line hidden
            
            #line 208 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(logicOne));
            
            #line default
            #line hidden
            this.Write("\r\n        }\r\n\r\n");
            
            #line 211 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"


            var linkedOtherSideMethodName = RuleOfNamesForTransfrom.GetLinkedOOMethodName(relClassDef, RuleOfNamesForTransfrom.RelationshipEdgeSide.OtherSide);
            var linkedOtherSideReturnTypeName = RuleOfNamesForTransfrom.GetMethodReturnType(relClassDef.Relationship.OtherSideEdge.Edge, XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_1);

            
            #line default
            #line hidden
            this.Write("        public ");
            
            #line 216 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(linkedOtherSideReturnTypeName));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 216 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(linkedOtherSideMethodName));
            
            #line default
            #line hidden
            this.Write("()\r\n        {\r\n");
            
            #line 218 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

            currentMult = relClassDef.Relationship.OtherSideEdge.Mult;
            relClassDef.Relationship.OtherSideEdge.Mult = XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_1;
            var logicGeneratorOther = new LinkedItemLogic(logicIndent, relClassDef.Relationship, relClassDef.OtherSideEdge, relClassDef.Relationship.OtherSideEdge, null);
            var logicOther = logicGeneratorOther.TransformText();
            relClassDef.Relationship.OtherSideEdge.Mult = currentMult;

            
            #line default
            #line hidden
            
            #line 225 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(logicOther));
            
            #line default
            #line hidden
            this.Write("\r\n        }\r\n\r\n");
            
            #line 228 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

        }

            
            #line default
            #line hidden
            this.Write("    }\r\n");
            
            #line 232 "C:\Users\kae-m\source\repos\xtMULMetaModelProjects\Kae.XTUML.Tools.MetaModelGenerator\template\CIMClassBase.tt"

    }

            
            #line default
            #line hidden
            this.Write("}\r\n");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class CIMClassBaseBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}