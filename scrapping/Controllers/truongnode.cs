using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrapping.Controllers
{
    public class NodeTruong
    {
        public NodeTruong()
        {
            nganhs = new List<NodeNganh>();
        }
        public string hreft;
        public string ten;
        public List<NodeNganh> nganhs;
        public string MaTruong;
    }
}