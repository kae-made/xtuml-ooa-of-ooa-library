using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.CIM
{
    public interface CIClassDef : IDisposable
    {
        public string DomainName { get; }
        public string ClassName { get; }
        /// <summary>
        /// Check attributes and links are valid or not.
        /// </summary>
        /// <returns></returns>
        bool Validate();     
    }
}
