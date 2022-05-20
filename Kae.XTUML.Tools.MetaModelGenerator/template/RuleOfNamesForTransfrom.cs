// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.XTUML.Tools.CIModelResolver.template
{
    public class RuleOfNamesForTransfrom
    {
        public static readonly string CIMDomainName = "OOAofOOA";
        public static readonly string CIMAbstractClassDefClassName = "CIMOOAofOOAClass";
        public static readonly string CIModelRepositoryClassName = "CIModelRepository";
        public static readonly string CIModelRepositoryMemberName = "repository";

        public static string ToCapital(string frag)
        {
            var result = "";
            if (!string.IsNullOrEmpty(frag))
            {
                var dsfrag = frag.Split(new char[] { ' ' });
                foreach (var f in dsfrag)
                {
                    result += f.Substring(0, 1).ToUpper() + f.Substring(1);
                }
            }
            return result;
        }

        public static string GetCIMInterfaceClassName(XTUMLOOAofOOA.ClassOfOOA classDef)
        {
            return $"CIMClass{classDef.Name}";
        }

        public static string GetCIMBaseClassName(XTUMLOOAofOOA.ClassOfOOA classDef)
        {
            return $"{GetCIMInterfaceClassName(classDef)}Base";
        }

        public static string GetCIMSubClassName(XTUMLOOAofOOA.RelationshipOfOOA relationship)
        {
            return $"CIMSubClass{relationship.Ref_Id}";
        }

        public static string GetAttrPropertyName(string attrName)
        {
            return $"Attr_{attrName}";
        }

        public static string GetCIMSubClassMethodName(XTUMLOOAofOOA.RelationshipOfOOA relationship)
        {
            return $"SubClass{relationship.Ref_Id}";
        }

        public static string GetCIMSuperClassMethodName(XTUMLOOAofOOA.ClassOfOOA classDef)
        {
            return $"CIMSuperClass{classDef.Name}";
        }

        public static string GetLinkedMethodName(XTUMLOOAofOOA.RelationshipOfOOA relationship, RelationshipEdgeSide side)
        {
            string phrasePart = "";
            switch (side)
            {
                case RelationshipEdgeSide.From:
                    phrasePart = ToCapital(relationship.FromEdge.Phrase);
                    break;
                case RelationshipEdgeSide.To:
                    phrasePart = ToCapital(relationship.ToEdge.Phrase);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("side should be From or To");
            }
            return $"Linked{side.ToString()}{relationship.Ref_Id}{phrasePart}";
        }
        public static string GetLinkedMethodReturnTypeName(XTUMLOOAofOOA.RelationshipOfOOA relationship, RelationshipEdgeSide side)
        {
            XTUMLOOAofOOA.RelationshpEdgeOfOOA relEdge = null;
            string typeName = "";
            bool isMult = false;
            switch (side)
            {
                case  RelationshipEdgeSide.From:
                    relEdge = relationship.FromEdge;
                    break;
                case RelationshipEdgeSide.To:
                    relEdge = relationship.ToEdge;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("side should be From or To");
            }
            switch (relEdge.Mult)
            {
                case XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_M:
                case XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_MC:
                    isMult = true;
                    break;
            }
            typeName = GetCIMInterfaceClassName(relEdge.Edge);
            if (isMult)
            {
                typeName = $"IEnumerable<{typeName}>";
            }
            return typeName;
        }
        public static string GetLinkedOOMethodName(XTUMLOOAofOOA.RelationshipClassRelationOfOOA relationship, RelationshipEdgeSide side)
        {
            string phrasePart = "";
            switch (side)
            {
                case  RelationshipEdgeSide.OneSide:
                    phrasePart = ToCapital(relationship.OneSideEdge.Phrase);
                    break;
                case  RelationshipEdgeSide.OtherSide:
                    phrasePart = ToCapital(relationship.OtherSideEdge.Phrase);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("side should be OneSide or OtherSide");
            }
            return $"Linked{side.ToString()}{relationship.Ref_Id}{phrasePart}";
        }

        public enum RelationshipEdgeSide
        {
            From,
            To,
            OneSide,
            OtherSide            
        }
        public static string GetMethodReturnType(XTUMLOOAofOOA.ClassOfOOA classDef, XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity mult)
        {
            var typeName = GetCIMInterfaceClassName(classDef);
            switch (mult)
            {
                case XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_M:
                case XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_MC:
                    typeName = $"IEnumerable<{typeName}>";
                    break;
            }
            return typeName;
        }
        public static string GetLinkedOOMethodName(XTUMLOOAofOOA.RelationshipClassOfOOA relClassDef, RelationshipEdgeSide side)
        {
            string phrasePart = "";
            string sidePhrase = side.ToString();
            switch (side)
            {
                case  RelationshipEdgeSide.OneSide:
                    phrasePart = ToCapital(relClassDef.OneSideEdge.Phrase);
                    break;
                case  RelationshipEdgeSide.OtherSide:
                    phrasePart = ToCapital(relClassDef.OtherSideEdge.Phrase);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("side should be OneSide or OtherSide");
            }
            return $"Linked{sidePhrase}{relClassDef.Relationship.Ref_Id}{phrasePart}";
        }

        public static bool CheckPreservedWord(string word)
        {
            string pattern = "^(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|overreide|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while)$";
            return System.Text.RegularExpressions.Regex.IsMatch(word, pattern);
        }
    }
}
