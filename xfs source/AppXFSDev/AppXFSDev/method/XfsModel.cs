using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppXFSDev.method
{
    public class XfsModel
    {
        public int offset { get; set; }
        public int zsize { get; set; }
        public int info_size { get; set; }

        public string str_head { get; set; }
        public int version { get; set; }
        public int files_count { get; set; }
        public int validation { get; set; }
        public int offset2 { get; set; }
        public List<XfsFileData> files { get; set; }
    }
}
