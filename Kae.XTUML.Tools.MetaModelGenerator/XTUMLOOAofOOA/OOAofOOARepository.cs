// Copyright (c) Knowledge & Experience. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.XTUML.Tools.MetaModelGenerator.XTUMLOOAofOOA
{
    public class OOAofOOARepository
    {
        public IDictionary<string, ClassOfOOA> Classes { get; set; }
        public IDictionary<string, RelationshipOfOOA> Relationships { get; set; }

        public IDictionary<string, DataTypeOfOOA> DataTypes { get; set; }
    }

    public enum KindOfClass
    {
        Normal,
        Relationship
    }
    public class ClassOfOOA
    {
        public ClassOfOOA()
        {
            Kind = KindOfClass.Normal;
        }
        public KindOfClass Kind { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string> Attributes { get; set; }
    }

   public  class RelationshipClassOfOOA : ClassOfOOA
    {
        public RelationshipClassOfOOA() : base()
        {
            Kind = KindOfClass.Relationship;
        }
        public RelationshipClassRelationOfOOA Relationship { get; set; }

        public RelationshpEdgeOfOOA OneSideEdge { get; set; }
        public RelationshpEdgeOfOOA OtherSideEdge { get; set; }
    }

    public enum KindOfRelatioship
    {
        Binary,
        SuperSub,
        RelationshipClass
    }

    public  class RelationshipOfOOA
    {
        public RelationshipOfOOA()
        {
            Kind = KindOfRelatioship.Binary;
        }

        public KindOfRelatioship Kind { get; set; }
        public string Ref_Id { get; set; }
        public RelationshpEdgeOfOOA FromEdge { get; set; }
        // public List<RelationshpEdgeOfOOA> FromEdges { get; set; }
        public RelationshpEdgeOfOOA ToEdge { get; set; }
        // public List<RelationshpEdgeOfOOA> ToEdges { get; set; }
    }

    public class SuperSubRelationshipOfOOA : RelationshipOfOOA
    {
        public SuperSubRelationshipOfOOA() : base()
        {
            Kind = KindOfRelatioship.SuperSub;
            SubEdges = new List<RelationshpEdgeOfOOA>();
        }
        public List<RelationshpEdgeOfOOA> SubEdges { get; set; }
    }

    public class RelationshipClassRelationOfOOA : RelationshipOfOOA
    {
        public RelationshipClassRelationOfOOA() : base()
        {
            Kind = KindOfRelatioship.RelationshipClass;
        }
        public RelationshpEdgeOfOOA OneSideEdge { get; set; }
        public RelationshpEdgeOfOOA OtherSideEdge { get; set; }
        public RelationshipClassOfOOA RelationshipClass { get; set; }
    }


    public class RelationshpEdgeOfOOA
    {
        public static Multiplicity ConvertTo(string mult)
        {
            switch (mult)
            {
                case "1":
                    return Multiplicity.MULT_1;
                case "1C":
                    return Multiplicity.MULT_1C;
                case "M":
                    return Multiplicity.MULT_M;
                case "MC":
                    return Multiplicity.MULT_MC;

            }
            throw new ArgumentOutOfRangeException("mult should be 1|1C|M|MC");
        }

        public enum Multiplicity
        {
            MULT_1,
            MULT_1C,
            MULT_M,
            MULT_MC
        };
        public ClassOfOOA Edge { get; set; }
        public Multiplicity Mult { get; set; }
        public string Phrase { get; set; }
        public IEnumerable<string> RefAttributes { get; set; }
    }

   public  class DataTypeOfOOA
    {
        public string Name { get; set; }
        public string CodeTypeName { get; set; }
    }
}
