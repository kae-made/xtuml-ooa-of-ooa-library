// ------------------------------------------------------------------------------
// <auto-generated>
//     This file is generated by tool.
//     Runtime Version : 0.0.1
//  
//     Updates this file cause incorrect behavior 
//     and will be lost when the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

namespace Kae.CIM.MetaModel.CIMofCIM
{
    public abstract class CIMOOAofOOAClass
    {
        protected string className;
        protected readonly static string domainName = "OOAofOOA";
        protected CIModelRepository repository;
        public CIMOOAofOOAClass(CIModelRepository repository, string className)
        {
            this.repository = repository;
            this.className = className;
        }

        public string DomainName { get { return domainName; } }

        public string ClassName { get { return className; } }
    }
}