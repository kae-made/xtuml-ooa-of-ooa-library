// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.XTUML.Tools.CIModelResolver.template
{
    partial class LinkedItemLogic
    {
        private string indent;
        private XTUMLOOAofOOA.RelationshipOfOOA relationship;
        private XTUMLOOAofOOA.RelationshpEdgeOfOOA current;
        private XTUMLOOAofOOA.RelationshpEdgeOfOOA opponent;
        private IEnumerable<XTUMLOOAofOOA.RelationshpEdgeOfOOA> subclasses;

        private static readonly string itemVarName = "i";

        public LinkedItemLogic(string indent, XTUMLOOAofOOA.RelationshipOfOOA relationship, XTUMLOOAofOOA.RelationshpEdgeOfOOA current, XTUMLOOAofOOA.RelationshpEdgeOfOOA opponent, IEnumerable<XTUMLOOAofOOA.RelationshpEdgeOfOOA> subclasses)
        {
            this.indent = indent;
            this.relationship = relationship;
            this.current = current;
            this.opponent = opponent;
            this.subclasses = subclasses;
        }

        private string BuildConditionRelAttributes(XTUMLOOAofOOA.RelationshpEdgeOfOOA current, XTUMLOOAofOOA.RelationshpEdgeOfOOA opponent, string vName)
        {
            string result = "";
            string oppClassName = RuleOfNamesForTransfrom.GetCIMInterfaceClassName(opponent.Edge);

            for (int i = 0; i < current.RefAttributes.Count(); i++)
            {
                var attrPropertyNameCurrent = RuleOfNamesForTransfrom.GetAttrPropertyName(current.RefAttributes.ElementAt(i));
                var attrPropertyNameOpponent = RuleOfNamesForTransfrom.GetAttrPropertyName(opponent.RefAttributes.ElementAt(i));

                string fragment = $"(({oppClassName}){vName}).{attrPropertyNameOpponent} == this.{attrPropertyNameCurrent}";
                if (string.IsNullOrEmpty(result))
                {
                    result = fragment;
                }
                else
                {
                    result = $"{result} && {fragment}";
                }
            }

            return result;
        }

        private string BuildConditionSubAttributes(XTUMLOOAofOOA.RelationshipOfOOA relationship, XTUMLOOAofOOA.RelationshpEdgeOfOOA superClass, string vName)
        {
            string result = "";
            string subRelClassName = RuleOfNamesForTransfrom.GetCIMSubClassName(relationship);
            string getSubRelClassMethodName = RuleOfNamesForTransfrom.GetCIMSuperClassMethodName(superClass.Edge);

            for (int i = 0; i < current.RefAttributes.Count(); i++)
            {
                string attrPropertyName = RuleOfNamesForTransfrom.GetAttrPropertyName(superClass.RefAttributes.ElementAt(i));
                string fragment = $"(({subRelClassName}){vName}).{getSubRelClassMethodName}().{attrPropertyName} == this.{attrPropertyName}";
                if (string.IsNullOrEmpty(result))
                {
                    result = fragment;
                }
                else
                {
                    result = $"{result} && {fragment}";
                }
            }

            return result;
        }

        public void Prototype()
        {
            if (this.subclasses == null && this.opponent != null)
            {
                var condition = BuildConditionRelAttributes(current, opponent, itemVarName);
                if (!string.IsNullOrEmpty(condition)) { }
                    string oppClassName = RuleOfNamesForTransfrom.GetCIMInterfaceClassName(opponent.Edge);
                if (opponent.Mult == XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_M || opponent.Mult == XTUMLOOAofOOA.RelationshpEdgeOfOOA.Multiplicity.MULT_MC)
                {

                }
                else
                {

                }
            }
            else if (this.subclasses!=null && this.opponent == null)
            {
                string subClassNames = "";
                foreach(var s in subclasses)
                {
                    if (string.IsNullOrEmpty(subClassNames))
                    {
                        subClassNames = s.Edge.Name;
                    }
                    else
                    {
                        subClassNames = $"{subClassNames}, {s.Edge.Name}";
                    }
                }
                string subRelClassName = RuleOfNamesForTransfrom.GetCIMSubClassName(relationship);
                string superClassGetMethodName = RuleOfNamesForTransfrom.GetCIMSuperClassMethodName(current.Edge);
                string condition = BuildConditionSubAttributes(relationship, relationship.ToEdge, itemVarName);
            }
        }

    }
}
