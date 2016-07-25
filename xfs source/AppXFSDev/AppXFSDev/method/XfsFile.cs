using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zlib;

namespace AppXFSDev.method
{
    public class XfsFile : XfsModel
    {
        public string fileName { get; set; }
        public PacketReader reader { get; set; }
        public XfsFile(string fileName)
        {
            this.fileName = fileName;
            this.Init();
        }

        public void Init()
        {
            byte[] _data = File.ReadAllBytes(this.fileName);
            reader = new PacketReader(_data);
            files = new List<XfsFileData>();

            #region HeaderInfo
            offset = reader.Int32();
            reader.Offset(offset);
            zsize = reader.Byte();
            info_size = zsize * 0x80;

            byte[] head_data_com = reader.Bytes(zsize);
            //Console.WriteLine(Utils.HexDump(head_data_com));
            byte[] head_data_des = ZlibStream.UncompressBuffer(head_data_com);
            //Console.WriteLine(Utils.HexDump(head_data_des));
            PacketReader head_data = new PacketReader(head_data_des);

            str_head = head_data.String(4);
            version = head_data.Int32();
            files_count = head_data.Int32();
            validation = head_data.Int32();
            offset2 = head_data.Int32();
            #endregion

            #region data_info
            byte[] size_24bts = reader.Bytes(3);
            int info_size_data = size_24bts[0] + (size_24bts[1] << 8) + (size_24bts[2] << 16);
            //Console.WriteLine("info_size_data: {0}", info_size_data);
            byte[] info_data_com = reader.Bytes(info_size_data);
            byte[] info_data = ZlibStream.UncompressBuffer(info_data_com);
            //Console.WriteLine(Utils.HexDump(info_data));
            PacketReader reader_info = new PacketReader(info_data);

            for (int cx = 0; cx < files_count; cx++)
            {
                XfsFileData fdata = new XfsFileData();
                fdata.fileName = reader_info.String(true);
                fdata.offset = reader_info.Int32();
                fdata.compresed = reader_info.Int32();
                fdata.UCSize = reader_info.Int32();
                fdata.CSize = reader_info.Int32();
                //Console.WriteLine("fileName: '{0}' offset_file: 0x{1:X} compresed: {2} UCSize: {3} CSize: {4}", fdata.fileName, fdata.offset, fdata.compresed, fdata.UCSize, fdata.CSize);
                files.Add(fdata);
            }

            #endregion
        }
    }
}
