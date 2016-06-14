using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Duality;
using Duality.Resources;

namespace MFEP.Duality.Plugins.InputPlugin
{
    public class InputMappingRes : Resource
    {
        private Dictionary<string, VirtualButton> virtualButtonDict;

        public void SaveVirtualButtonDict(Dictionary<string, VirtualButton> dict)
        {
            virtualButtonDict = dict;
        }
        public void LoadVirtualButtonDict(out Dictionary<string, VirtualButton> dict)
        {
            dict = virtualButtonDict;
        }
    }
}
