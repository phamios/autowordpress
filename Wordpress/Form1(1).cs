using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using CookComputing.XmlRpc;
using System.IO;
using System.Threading;

using System.Xml; 
using System.Reflection;
 
 
namespace Wordpress
{
    public struct blogInfo
    {
        public string title;
        public string description;
    }
    

     public interface IgetCatList
    {
        [CookComputing.XmlRpc.XmlRpcMethod("metaWeblog.newPost")]
        string NewPage(int blogId, string strUserName, string strPassword, blogInfo content, int publish);
    }



    public partial class Form1 : Form
    {
        

      public XmlRpcClientProtocol clientProtocol;
      public IgetCatList categories;


        public Form1()
        {
            InitializeComponent();
            initialize();
             
        }

        void initialize()
        {
            //log any unhandled exceptions
            //ModelText.ModelException.LogUnhandledException.enable(true);

            //load the data after the form as finished loading
            this.Load += new EventHandler(Form1_Load);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox2.Visible = false; 
        }
 

        private void btnPost_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            int count_item = listBox1.Items.Count; 
            foreach (string url in listBox1.Items)
            {
                groupBox2.Visible = true;
                groupBox2.Enabled = true;
                Thread.Sleep(int.Parse(textBox1.Text)); 
                ProcessDataWordpress(url);
                //foreach (string spin in listBox2.Items)
                //{
                    
                //}
                
                groupBox2.Enabled = false; 
            }
            groupBox2.Text = "FINISH";
            groupBox2.Visible = false;
        }

        private String MixChangecontent(String content)
        {
            int count_item = listBox1.Items.Count;
            foreach (string url in listBox1.Items)
            {
                String temp = url.Replace("{", "");
                temp = temp.Replace("}", "");
                string[] words = temp.Split(',');

                for (int x = 0; x < words.Length; x++)
                {
                    if (content.IndexOf(words[x]) != -1)
                    {
                        Console.WriteLine("string contains dog!");
                    }
                }

                
            }

            
            return content;
        }

        private void ProcessDataWordpress(String url)
        {
            blogInfo newBlogPost = default(blogInfo);
              
            newBlogPost.title =   Spinner.Spin(txtTitle.Text);
            newBlogPost.description = Spinner.Spin(editor1.BodyHtml);
            categories = (IgetCatList)XmlRpcProxyGen.Create(typeof(IgetCatList));
            clientProtocol = (XmlRpcClientProtocol)categories;

            string linkwordpress = "";
            string username = "";
            string password = "";
            string[] words = url.Split('|');

            linkwordpress = words[0];
            username = words[1];
            password = words[2];

            txtSite.Text = linkwordpress;
            txtUser.Text = username;
            txtPass.Text = password;

            clientProtocol.Url = "https://" + linkwordpress  + "/xmlrpc.php";

            string result = null;
            result = ""; 
            try
            {
                result = categories.NewPage(1, username, password, newBlogPost, 1);
                //MessageBox.Show("Posted to Blog successfullly! Post ID : " + result);
                //txtPost.Text = "";
                //txtTitle.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                int counter = 0;
                string line;
                string filename = theDialog.FileName;
                System.IO.StreamReader file = new System.IO.StreamReader(filename);
                while ((line = file.ReadLine()) != null)
                {
                    listBox1.Items.Add(line);
                    counter++;
                } 
                file.Close();
                groupBox1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Form2 frm2 = new Form2();
            //frm2.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                listBox2.Items.Clear();
                int counter = 0;
                string line;
                string filename = theDialog.FileName;
                System.IO.StreamReader file = new System.IO.StreamReader(filename);
                while ((line = file.ReadLine()) != null)
                {
                    listBox2.Items.Add(line);
                    counter++;
                }
                file.Close();
                groupBox1.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            int index = ListBox.NoMatches;
            int count_item = listBox2.Items.Count;
            foreach (string url in listBox2.Items)
            {
                string new_url = url.Replace("{","");
                new_url = new_url.Replace("}","");
                string[] words = new_url.Split('|');
                foreach(string word in words)
                {
                    var list = new List<string>();
                    list.Add(word); 
                }

               

                //while ((index = listBox2.FindString(editor1.BodyHtml, index)) != ListBox.NoMatches)
                //{
                //    MessageBox.Show("Found the item \"" + editor1.BodyHtml + "\" at index: " + index);
                //}
            }
        }
    }
}
