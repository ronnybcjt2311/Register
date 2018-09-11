using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void accept_Click(object sender, EventArgs e)
        {
            
                MongoClient client = new MongoClient("mongodb://admin:a123456@ds141902.mlab.com:41902/ox");
                MongoServer server = client.GetServer();
                MongoDatabase database = server.GetDatabase("ox");
                MongoCollection symbolcollection = database.GetCollection<Register2>("User");
                Register2 register = new Register2();
                BindingList<Register2> doclist = new BindingList<Register2>();
                var registerDB = database.GetCollection<Register2>("User");
                var registerDB2 = registerDB.AsQueryable().Where(pd => pd.Username == userName.Text);
                foreach (var p in registerDB2)
                {
                    doclist.Add(p);
                    Application.DoEvents();
                }
                dataGridView1.DataSource = doclist;
                if (dataGridView1.Rows.Count == 0)
                {
                    if (!string.IsNullOrEmpty(userName.Text.Trim()))
                    {
                        register.Username = userName.Text;
                    }else
                    {
                        MessageBox.Show("ใส่ข้อมูลให้สมบูรณ์");
                    }
                    if (!string.IsNullOrEmpty(password.Text.Trim()))
                    {
                        register.Password = password.Text;
                    }
                    else
                    {
                        MessageBox.Show("ใส่ข้อมูลให้สมบูรณ์");
                    }
                    if (!string.IsNullOrEmpty(idName.Text.Trim()))
                    {
                        register.Name = idName.Text;
                    }
                    else
                    {
                        MessageBox.Show("ใส่ข้อมูลให้สมบูรณ์");
                    }
                    register.Avatar = null;
                    register.Win = 0;
                    register.Draw = 0;
                    register.Lose = 0;
                if(!string.IsNullOrEmpty(userName.Text.Trim()) && !string.IsNullOrEmpty(password.Text.Trim()) && !string.IsNullOrEmpty(idName.Text.Trim()))
                {
                    symbolcollection.Insert(register);
                    MessageBox.Show("Success");
                    userName.Text = "";
                    password.Text = "";
                    cfpassword.Text = "";
                    idName.Text = "";
                }
                else
                {
                    MessageBox.Show("ใส่ข้อมูลให้สมบูรณ์");
                }
            }
        }

        public class Register2
        {
            public ObjectId _id { get; set; }
            public string Username
            {
                get; set;
            }
            public string Name
            {
                get; set;
            }
            public string Password
            {
                get; set;
            }
            public string Avatar
            {
                get; set;
            }
            public int Win
            {
                get; set;
            }
            public int Draw
            {
                get; set;
            }
            public int Lose
            {
                get; set;
            }

        }
    }
}
