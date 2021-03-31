using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalJson2Text
{
    public class Model
    {
        public Scene[] scenes { get; set; }
    }

    public class Scene
    {
        public object[][] texts { get; set; }
        public string[] title { get; set; }
    }
}
