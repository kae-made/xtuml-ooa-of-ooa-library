using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kae.CIM.MetaModel.CIMofCIM
{
    #region HAND_CODED
    public interface CIMClassKAE_DOM : CIClassDef
    {
        public string Attr_Domain_ID { get; set; }
        public string Attr_Name { get; set; }
        public string Attr_Descrip { get; set; }

        public IEnumerable<CIMClassKAE_CID> LinkedFromR7001();
    }

    public interface CIMClassKAE_CID : CIClassDef
    {
        public string Attr_Domain_ID { get; set; }
        public string Attr_Obj_ID { get; set; }
        public CIMClassKAE_DOM LinkedToOneSideR7001();
        public CIMClassO_OBJ LinkedToOtherSideR7001();
    }
    #endregion
}
