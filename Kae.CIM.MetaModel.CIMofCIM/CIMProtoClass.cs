using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.CIM.MetaModel.CIMofCIM
{
#if false
    internal abstract class CIMOOAofOOAClassProto
    {
        protected string className = "MSG_A";
        protected readonly static string domainName = "xtUMLOOAofOOA";
        protected CIModelRepository repository;
        public CIMOOAofOOAClassProto(CIModelRepository repository)
        {
            this.repository = repository;
        }

        public string DomainName { get { return domainName; } }

        public string ClassName { get { return className; } }

    }

    public abstract class CIMOOAofOOAClass
    {
        protected string className = "MSG_A";
        protected readonly static string domainName = "xtUMLOOAofOOA";
        protected CIModelRepository repository;
        public CIMOOAofOOAClass(CIModelRepository repository, string className)
        {
            this.repository = repository;
            this.className = className;
        }

        public string DomainName { get { return domainName; } }

        public string ClassName { get { return className; } }

    }
    /// <summary>
    /// for MSG_A
    /// CREATE TABLE MSG_A (
    ///     Arg_ID UNIQUE_ID,
    ///     Informal_Msg_ID UNIQUE_ID,
    ///     Formal_Msg_ID UNIQUE_ID,
    ///     Label STRING,
    ///     Value STRING,
    ///     InformalName STRING,
    ///     Descrip STRING,
    ///     isFormal BOOLEAN
    /// );
    /// </summary>
    internal interface CIMProtoClass_MSG_A : CIClassDef
    {
        public string Arg_ID { get; set; }
        public string Informal_Msg_ID { get; set; }
        public string Formal_Msg_ID { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public string InformalName { get; set; }
        public string Descrip { get; set; }
        public bool isFormal { get; set; }

        /// <summary>
        /// CREATE ROP REF_ID R1000 FROM MC MSG_A (Informal_Msg_ID) TO 1C MSG_M (Msg_ID);
        /// </summary>
        /// <returns></returns>
        public CIMProtoClass_MSG_M LinkedR1000To();

        /// <summary>
        /// CREATE ROP REF_ID R1001 FROM MC MSG_A (Formal_Msg_ID) TO 1C MSG_M (Msg_ID);
        /// </summary>
        /// <returns></returns>
        public CIMProtoClass_MSG_M LinkedR1001To();

        public CIMSubClassR1013 SubClassR1013();
    }


    internal class CIMProtoClass_MSG_Abase : CIMOOAofOOAClassProto, CIMProtoClass_MSG_A
    {

        public string Arg_ID { get; set; }
        public string Informal_Msg_ID { get; set; }
        public string Formal_Msg_ID { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public string InformalName { get; set; }
        public string Descrip { get; set; }
        public bool isFormal { get; set; }

        public CIMProtoClass_MSG_Abase(CIModelRepository repository ) : base(repository)
        {
            this.className = "MSG_A";
        }

        public void Dispose()
        {
            
        }

        public CIMProtoClass_MSG_M LinkedR1000To()
        {
            var candidates = repository.GetCIInstances(domainName, "MSG_M").Where(i => ((CIMProtoClass_MSG_M)i).Msg_ID == this.Informal_Msg_ID);
            CIMProtoClass_MSG_M linkedInstance = null;
            if (candidates.Count() > 0)
            {
                linkedInstance = (CIMProtoClass_MSG_M) candidates.First();
            }
            return linkedInstance;
        }

        public CIMProtoClass_MSG_M LinkedR1001To()
        {
            var candidates = repository.GetCIInstances(domainName, "MSG_M").Where(i => ((CIMProtoClass_MSG_M)i).Msg_ID == this.Formal_Msg_ID);
            CIMProtoClass_MSG_M linkedInstance = null;
            if (candidates.Count() > 0)
            {
                linkedInstance = (CIMProtoClass_MSG_M)candidates.First();
            }
            return linkedInstance;
        }

        private static readonly IList<string> subClassNames = new List<string>() { "MSG_BA", "MSG_OA", "MSG_FA", "MSG_EA", "MSG_IA" };
        public CIMSubClassR1013 SubClassR1013()
        {
            var myId = this.Arg_ID;
            CIMSubClassR1013 subClass = null;
            foreach (var scName in subClassNames)
            {
                var candidates = repository.GetCIInstances(domainName, className).Where(i=>((CIMSubClassR1013)i).CIMSuperClass().Arg_ID==this.Arg_ID);
                if (candidates.Count() > 0)
                {
                    subClass = (CIMSubClassR1013)candidates.First();
                    break;
                }
            }
            return subClass;
        }

        public bool Validate()
        {
            return false;
        }
    }
#if false
    internal interface CIMSubClassR1013 : CIClassDef
    {
        public CIMProtoClass_MSG_A CIMSuperClass();

    }
#endif
    internal interface CIMProtoClass_MSG_M : CIClassDef
    {
        public string Msg_ID { get; set; }
        public string Receiver_Part_ID { get; set; }
        public string Sender_Part_ID { get; set; }
        public bool participatesInCommunication { get; set; }

        public IEnumerable<CIMProtoClass_MSG_A> LinkedR1000From();
        public IEnumerable<CIMProtoClass_MSG_A> LinkedR1001From();
    }

    internal class CIMProtoClass_MSG_Mbase : CIMOOAofOOAClassProto, CIMProtoClass_MSG_M
    {

        public string Msg_ID { get; set; }
        public string Receiver_Part_ID { get; set; }
        public string Sender_Part_ID { get; set; }
        public bool participatesInCommunication { get; set; }

        public void Dispose()
        {
            //
        }

        public CIMProtoClass_MSG_Mbase(CIModelRepository repository) : base(repository)
        {

        }

        public IEnumerable<CIMProtoClass_MSG_A> LinkedR1000From()
        {
            var linkedSet = new List<CIMProtoClass_MSG_A>();
            var candidate = repository.GetCIInstances(domainName, "MSG_A").Where(i => ((CIMProtoClass_MSG_A)i).Informal_Msg_ID == this.Msg_ID);
            if (candidate.Count() > 0)
            {
                foreach (var c in candidate)
                {
                    linkedSet.Add((CIMProtoClass_MSG_A)c);
                }
            }
            return linkedSet;
        }

        public IEnumerable<CIMProtoClass_MSG_A> LinkedR1001From()
        {
            var linkedSet = new List<CIMProtoClass_MSG_A>();
            var candidate = repository.GetCIInstances(domainName, "MSG_A").Where(i => ((CIMProtoClass_MSG_A)i).Formal_Msg_ID == this.Msg_ID);
            if (candidate.Count() > 0)
            {
                foreach (var c in candidate)
                {
                    linkedSet.Add((CIMProtoClass_MSG_A) c);
                }
            }
            return linkedSet;
        }

        public bool Validate()
        {
            return false;
        }
    }

    internal interface CIMProtoClass_MSG_BA : CIMSubClassR1013
    {
        public string Arg_ID { get; set; }
        public string BParm_ID { get; set; }

       
    }

    internal class CIMProtoClass_MSG_BAbase : CIMOOAofOOAClassProto, CIMProtoClass_MSG_BA
    {
        public string Arg_ID { get; set; }
        public string BParm_ID { get; set; }

        public CIMProtoClass_MSG_BAbase(CIModelRepository repository) : base(repository)
        {

        }

        public CIMProtoClass_MSG_A CIMSuperClass()
        {
            CIMProtoClass_MSG_A superClass = null;
            var candidate = repository.GetCIInstances(domainName, "MSG_A").Where(i => ((CIMProtoClass_MSG_A)i).Arg_ID == this.Arg_ID);
            if (candidate.Count() > 0)
            {
                superClass = (CIMProtoClass_MSG_A)candidate;
            }
            return superClass;
        }

        public void Dispose()
        {
            //
        }

        public bool Validate()
        {
            return false;
        }
    }

    internal interface CIMProtoClass_MSG_OA : CIMSubClassR1013
    {
        public string Arg_ID { get; set; }
        public string TParm_ID { get; set; }

    }

    internal class CIMProtoClass_MSG_OAbase : CIMOOAofOOAClassProto, CIMProtoClass_MSG_OA
    {
        public string Arg_ID { get; set; }
        public string TParm_ID { get; set; }

        public CIMProtoClass_MSG_OAbase(CIModelRepository repository) : base(repository)
        {

        }

        public CIMProtoClass_MSG_A CIMSuperClass()
        {
            CIMProtoClass_MSG_A superClass = null;
            var candidate = repository.GetCIInstances(domainName, "MSG_A").Where(i => ((CIMProtoClass_MSG_A)i).Arg_ID == this.Arg_ID);
            if (candidate.Count() > 0)
            {
                superClass = (CIMProtoClass_MSG_A)candidate;
            }
            return superClass;
        }

        public void Dispose()
        {
            //
        }

        public bool Validate()
        {
            return false;
        }
    }

    internal interface CIMProtoClass_O_ATTR : CIClassDef
    {
        public string Attr_ID { get; set; }
        public string Obj_ID { get; set; }
        public string PAttr_ID { get; set; }
        public string Name { get; set; }
        public string Descrip { get; set; }
        public string Prefix { get; set; }
        public string Root_Nam { get; set; }
        public string Pfx_Mode { get; set; }
        public string DT_ID { get; set; }
        public string Dimensions { get; set; }
        public string DefaultValue { get; set; }

        public CIMProtoClass_O_ATTR LinkedR103To_precedes();
        public CIMProtoClass_O_ATTR LinkedR103From_succeeds();
        public IEnumerable<CIMProtoClass_O_OIDA> LinkedR105FromOtherSide();
    }

    internal class CIMProtoClass_O_ATTRbase : CIMOOAofOOAClassProto, CIMProtoClass_O_ATTR
    {
        public string Attr_ID { get; set; }
        public string Obj_ID { get; set; }
        public string PAttr_ID { get; set; }
        public string Name { get; set; }
        public string Descrip { get; set; }
        public string Prefix { get; set; }
        public string Root_Nam { get; set; }
        public string Pfx_Mode { get; set; }
        public string DT_ID { get; set; }
        public string Dimensions { get; set; }
        public string DefaultValue { get; set; }


        public void Dispose()
        {
            //
        }

        CIMProtoClass_O_ATTRbase(CIModelRepository repository):base(repository)
        {

        }

        public CIMProtoClass_O_ATTR LinkedR103From_succeeds()
        {
            CIMProtoClass_O_ATTR linked = null;
            var candidate = repository.GetCIInstances(domainName, "O_ATTR").Where(i => ((CIMProtoClass_O_ATTR)i).PAttr_ID == this.Attr_ID && ((CIMProtoClass_O_ATTR)i).Obj_ID == this.Obj_ID);
            if (candidate.Count() > 0)
            {
                linked = (CIMProtoClass_O_ATTR)candidate.First();
            }
            return linked;
        }

        public CIMProtoClass_O_ATTR LinkedR103To_precedes()
        {
            CIMProtoClass_O_ATTR linked = null;
            var candidate = repository.GetCIInstances(domainName, "O_ATTR").Where(i => ((CIMProtoClass_O_ATTR)i).Attr_ID == this.PAttr_ID && ((CIMProtoClass_O_ATTR)i).Obj_ID == this.Obj_ID);
            if (candidate.Count() > 0)
            {
                linked = (CIMProtoClass_O_ATTR)candidate.First();
            }
            return linked;
        }

        public IEnumerable< CIMProtoClass_O_OIDA> LinkedR105FromOtherSide()
        {
            var linked = new List<CIMProtoClass_O_OIDA>();
            var candidates = repository.GetCIInstances(domainName, "O_OIDA").Where(i => ((CIMProtoClass_O_OIDA)i).Attr_ID == this.Attr_ID && ((CIMProtoClass_O_OIDA)i).Obj_ID == this.Obj_ID);
            foreach(var c in candidates)
            {
                linked.Add((CIMProtoClass_O_OIDA)c);
            }
            return linked;
        }

        public bool Validate()
        {
            return false;
        }
    }

    internal interface CIMProtoClass_O_OIDA : CIClassDef
    {
        public string Attr_ID { get; set; }
        public string Obj_ID { get; set; }
        public int Oid_ID { get; set; }
        public string localAttributeName { get; set; }

        public CIMProtoClass_O_ID LinkedR105OneSide();
        public CIMProtoClass_O_ATTR LinkedR105OtherSide();

    }

    internal class CIMProtoClass_O_OIDAbase : CIMOOAofOOAClassProto, CIMProtoClass_O_OIDA
    {
        public string Attr_ID { get; set; }
        public string Obj_ID { get; set; }
        public int Oid_ID { get; set; }
        public string localAttributeName { get; set; }

        public void Dispose()
        {
            ;
        }

        public CIMProtoClass_O_OIDAbase(CIModelRepository repository) : base(repository)
        {

        }

        public CIMProtoClass_O_ID LinkedR105OneSide()
        {
            CIMProtoClass_O_ID linked = null;
            var candidate = repository.GetCIInstances(domainName, "O_ID").Where(i => ((CIMProtoClass_O_ID)i).Oid_ID == this.Oid_ID && ((CIMProtoClass_O_ID)i).Obj_ID == this.Obj_ID);
            if (candidate.Count() > 0)
            {
                linked = (CIMProtoClass_O_ID)candidate.First();
            }
            return linked;
        }

        public CIMProtoClass_O_ATTR LinkedR105OtherSide()
        {
            CIMProtoClass_O_ATTR linked = null;
            var candidate = repository.GetCIInstances(domainName, "O_ATTR").Where(i => ((CIMProtoClass_O_ATTR)i).Attr_ID == this.Attr_ID && ((CIMProtoClass_O_ATTR)i).Obj_ID == this.Obj_ID);
            if (candidate.Count() > 0)
            {
                linked = (CIMProtoClass_O_ATTR)candidate.First();
            }
            return linked;
        }

        public bool Validate()
        {
            return false;
        }
    }

    internal interface CIMProtoClass_O_ID : CIClassDef
    {
        public int Oid_ID { get; set; }
        public string Obj_ID { get; set; }

        public IEnumerable< CIMProtoClass_O_OIDA> LinkedFromR105OneSide();
    }

    internal class CIMProtoClass_O_IDbase : CIMOOAofOOAClassProto, CIMProtoClass_O_ID
    {
        public int Oid_ID { get; set; }
        public string Obj_ID { get; set; }

        public void Dispose()
        {
            //
        }

        public CIMProtoClass_O_IDbase(CIModelRepository repository) : base(repository)
        {

        }

        public IEnumerable< CIMProtoClass_O_OIDA> LinkedFromR105OneSide()
        {
            var linkedSet = new List<CIMProtoClass_O_OIDA>();
            var candidate = repository.GetCIInstances(domainName, "O_OIDA").Where(i => ((CIMProtoClass_O_OIDA)i).Oid_ID == this.Oid_ID && ((CIMProtoClass_O_OIDA)i).Obj_ID == this.Obj_ID);
            foreach (var c in candidate)
            {
                linkedSet.Add((CIMProtoClass_O_OIDA)c);
            }
            return linkedSet;
        }

        public bool Validate()
        {
            return false;
        }
    }
#endif

}
