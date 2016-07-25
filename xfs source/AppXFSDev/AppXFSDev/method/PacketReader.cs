using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppXFSDev
{
    public class PacketReader
    {
        MemoryStream ms;
        BinaryReader br;

        public PacketReader(byte[] data)
        {
            ms = new MemoryStream(data);
            br = new BinaryReader(ms);
        }
        public byte Byte()
        {
            return br.ReadByte();
        }
        public ushort UInt16()
        {
            return br.ReadUInt16();
        }
        public uint UInt32()
        {
            return br.ReadUInt32();
        }
        public ulong UInt64()
        {
            return br.ReadUInt64();
        }
        public short Int16()
        {
            return br.ReadInt16();
        }
        public int Int32()
        {
            return br.ReadInt32();
        }
        public long Int64()
        {
            return br.ReadInt64();
        }
        public float Single()
        {
            return br.ReadSingle();
        }

        public byte[] Bytes(int cant)
        {
            return br.ReadBytes(cant);
        }

        public string String(int len)
        {
            StringBuilder sb = new StringBuilder();
            char[] chars = br.ReadChars(len);
            foreach (char c in chars)
            {
                sb.Append(c.ToString());
            }

            return sb.ToString();
        }

        public string String(bool fndNull)
        {
            byte[] xdata = new byte[112];
            bool nAum = false;
            int tk = 0;
            for (int co = 0; co < 112 ; co++)
            {
                byte c = br.ReadByte();
                if (c != 0x00 && !nAum) xdata[tk++] = c;
                else nAum = true;
            }
            return Encoding.Default.GetString(xdata.Take(tk).ToArray());
        }

        public string Text()
        {
            short len = Int16();
            return String(len);
        }

        public void Skip(int HowMany)
        {
            for (int x = 1; x <= HowMany; x++)
            {
                br.ReadByte();
            }
        }

        public void Offset(int off)
        {
            ms.Position = off;
        }

        public void Close()
        {
            br.Dispose();
            ms.Dispose();
            br.Close();
            ms.Close();
            GC.Collect(GC.GetGeneration(this));
        }
    }
}
