using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppXFSDev.method
{
    public class XfsFileData
    {
        public string fileName { get; set; }
        public int offset { get; set; }
        public int compresed { get; set; }
        public int UCSize { get; set; }
        public int CSize { get; set; }
        public byte[] data { get; set; }
    }
}
