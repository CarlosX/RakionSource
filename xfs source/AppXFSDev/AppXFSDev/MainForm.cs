using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppXFSDev.method;

namespace AppXFSDev
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Console.Title = "";
            string _pathexe = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString();
            string _direxe = Path.GetDirectoryName(_pathexe).ToString();

            XfsFile xfs = new XfsFile(_direxe + "/DataSetup.xfs");
            StringBuilder strbuild = new StringBuilder();
            //strbuild.AppendFormat("Offset: 0x{0:X} zsize: {1} info_size: {2}", xfs.offset, xfs.zsize, xfs.info_size);
            //strbuild.AppendLine();
            strbuild.AppendFormat("Type: {0} version: {1} files_count: {2} validation: {3} offset2: 0x{4:X}", xfs.str_head, xfs.version, xfs.files_count, xfs.validation, xfs.offset2);
            label1.Text = strbuild.ToString();

            StringBuilder strbuild2 = new StringBuilder();
            List<string> pathx = new List<string>();
            foreach (XfsFileData fil in xfs.files)
            {
                strbuild2.AppendFormat("fileName: '{0}' offset_file: 0x{1:X} compresed: {2} UCSize: {3} CSize: {4}", fil.fileName, fil.offset, fil.compresed, fil.UCSize, fil.CSize);
                strbuild2.AppendLine();
                pathx.Add(fil.fileName);
            }
            textBox1.Text = strbuild2.ToString();


            TreeNode node = MakeTreeFromPaths(pathx, "ROOT", '\\');
            treeView1.Nodes.Add(node);
        }

        public TreeNode MakeTreeFromPaths(List<string> paths, string rootNodeName = "", char separator = '/')
        {
            var rootNode = new TreeNode(rootNodeName) { ImageIndex = 0 };
            foreach (var path in paths.Where(x => !string.IsNullOrEmpty(x.Trim())))
            {
                var currentNode = rootNode;
                var pathItems = path.Split(separator);
                foreach (var item in pathItems)
                {
                    var tmp = currentNode.Nodes.Cast<TreeNode>().Where(x => x.Text.Equals(item));
                    currentNode = tmp.Count() > 0 ? tmp.Single() : currentNode.Nodes.Add(item);
                }
            }
            return rootNode;
        }
    }
}
