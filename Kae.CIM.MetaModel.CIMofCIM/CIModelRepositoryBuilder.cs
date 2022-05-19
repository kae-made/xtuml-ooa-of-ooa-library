using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.CIM.MetaModel.CIMofCIM
{
    public class CIModelRepositoryBuilder
    {
        public CIModelRepository CreateModelRepository()
        {
            return new CIModeRepsoitoryImpl();
        }
    }
}
