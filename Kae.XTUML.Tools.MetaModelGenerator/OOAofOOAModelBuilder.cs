// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Kae.XTUML.Tools.MetaModelGenerator.XTUMLOOAofOOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.XTUML.Tools.MetaModelGenerator
{
    class OOAofOOAModelBuilder
    {
        public OOAofOOARepository Repository { get; set; }

        private string lastElement;
        private IDictionary<string, string> currentAttributes = new Dictionary<string, string>();
        private string lastRefId;
        private IList<string> currentRefAttributes = new List<string>();
        private RelationshpEdgeOfOOA lastEdge;
        private RelationshpEdgeOfOOA lastFromEdge;
        private RelationshpEdgeOfOOA lastToEdge;
        private string lastPhrase;

        public void RegisterElement(string element)
        {
            lastElement = element;
        }
        public void AddAttribute(string dataType, string attrName)
        {
            currentAttributes.Add(attrName, dataType);
        }

        public void RegisterRefId(string refId)
        {
            lastRefId = refId;
        }

        public void RegisterPhrase(string phrase)
        {
            lastPhrase = phrase;
        }

        public void AddRefAttribute(string attrName)
        {
            currentRefAttributes.Add(attrName);
        }

        public void RegisterObject(string objName)
        {
            Repository.Classes.Add(lastElement, new ClassOfOOA() { Name = objName, Attributes = currentAttributes });
            currentAttributes = new Dictionary<string, string>();
        }

        public void RegisterRelEdge(RelationshpEdgeOfOOA.Multiplicity multiplicity, string objName)
        {
            var objs = Repository.Classes.Where(o => o.Key == objName);
            if (objs.Count() > 0)
            {
                lastEdge = new RelationshpEdgeOfOOA() { Mult = multiplicity, Edge = objs.First().Value, RefAttributes = currentRefAttributes };
            }
            else
            {
                Console.WriteLine($"Object:{objName} has not been registered.");
            }
        }

        public void RegisterFromRelEdge()
        {
            CheckRelPhrase();
            lastFromEdge = lastEdge;
            lastEdge = null;
            currentRefAttributes = new List<string>();
        }
        public void RegisterToRelEdge()
        {
            CheckRelPhrase();
            lastToEdge = lastEdge;
            lastEdge = null;
            currentRefAttributes = new List<string>();
        }

        private void CheckRelPhrase()
        {
            if (!string.IsNullOrEmpty(lastPhrase))
            {
                lastEdge.Phrase = lastPhrase;
                lastPhrase = null;
            }
        }

        public void RegisterRelationship(string refId)
        {
            if (Repository.Relationships.ContainsKey(refId))
            {
                var rel = Repository.Relationships[refId];
                bool isValid = false;
                if (rel.ToEdge.Edge.Name == lastToEdge.Edge.Name && rel.ToEdge.Mult == lastToEdge.Mult)
                {
                    SuperSubRelationshipOfOOA superSubRel = null;
                    if (rel is not SuperSubRelationshipOfOOA)
                    {
                        superSubRel = new SuperSubRelationshipOfOOA() { Ref_Id = rel.Ref_Id, ToEdge = rel.ToEdge };
                        superSubRel.SubEdges.Add(rel.FromEdge);
                        superSubRel.SubEdges.Add(lastFromEdge);
                        Repository.Relationships.Remove(rel.Ref_Id);
                        Repository.Relationships.Add(superSubRel.Ref_Id, superSubRel);
                    }
                    else
                    {
                        superSubRel = (SuperSubRelationshipOfOOA)rel;
                        superSubRel.SubEdges.Add(lastFromEdge);
                    }
                    isValid = true;
                }
                else
                {
                    if (rel.FromEdge.Edge.Name == lastFromEdge.Edge.Name)
                    {
                        RelationshipClassRelationOfOOA relClassRel = null;
                        if (rel is not RelationshipClassRelationOfOOA)
                        {
                            relClassRel = new RelationshipClassRelationOfOOA() { Ref_Id = rel.Ref_Id, OneSideEdge = rel.ToEdge, OtherSideEdge=lastToEdge };
                            relClassRel.RelationshipClass = new RelationshipClassOfOOA()
                            {
                                Name = rel.FromEdge.Edge.Name,
                                Attributes = rel.FromEdge.Edge.Attributes,
                                Relationship = rel,
                                OneSiedEdge = rel.FromEdge,
                                OtherSideEdge = lastFromEdge
                            };
                            Repository.Classes.Remove(relClassRel.RelationshipClass.Name);
                            Repository.Classes.Add(relClassRel.RelationshipClass.Name,relClassRel.RelationshipClass);

                            Repository.Relationships.Remove(rel.Ref_Id);
                            Repository.Relationships.Add(relClassRel.Ref_Id, relClassRel);

                            isValid = true;
                        }
                        else
                        {
                            throw new IndexOutOfRangeException("Relationship should have only two side.");
                        }
                    }
                }
                if (isValid == false)
                {
                    throw new IndexOutOfRangeException("To edge should have same multiplicity and class for same REF_ID");
                }
            }
            else
            {
                Repository.Relationships.Add(refId, new RelationshipOfOOA() { Ref_Id = refId, FromEdge = lastFromEdge, ToEdge = lastToEdge });
            }
            lastFromEdge = null;
            lastToEdge = null;
        }

        public void PickupDataType()
        {
            var datatypes = new Dictionary<string, DataTypeOfOOA>();
            foreach (var ck in Repository.Classes.Keys)
            {
                var c = Repository.Classes[ck];
                foreach (var ak in c.Attributes.Keys)
                {
                    var d = c.Attributes[ak];
                    if (!datatypes.ContainsKey(d))
                    {
                        datatypes.Add(d, new DataTypeOfOOA() { Name = d });
                    }
                }
            }
            Repository.DataTypes = datatypes;
        }
    }
}
