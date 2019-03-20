using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private float X, Y;
        private bool start = false;
        private int startNum = 0;
        private int endNum = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Resize += new EventHandler(Form1_Resize);//执行Form1_Resize方法
            X = this.Width;
            Y = this.Height;
            setTag(this);
        }

        private void Form1_Resize(object sender, EventArgs e) //调用Resize时间
        {
            float newx = (this.Width) / X;//当前宽度与变化前宽度之比
            float newy = this.Height / Y;//当前高度与变化前宽度之比
            setControls(newx, newy, this);
            //this.Text = this.Width.ToString() + " " + this.Height.ToString();  //窗体标题显示长度和宽度
        }

        private void setControls(float newx, float newy, Control cons)//实现控件以及字体的缩放
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * newy;
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);//递归
                }
            }
        }
        
        //获得控件的长度、宽度、位置、字体大小的数据
        private void setTag(Control cons)//Control类，定义控件的基类
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;//获取或设置包含有关控件的数据的对象
                if (con.Controls.Count > 0)
                    setTag(con);//递归算法
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!start)
            {
                if (textBox1.Text == null || textBox1.Text.Length == 0)
                {
                    MessageBox.Show("开始学号错误！请输入开始学号！");
                    return;
                }
                ASCIIEncoding ascii = new ASCIIEncoding();
                byte[] bytestr = ascii.GetBytes(textBox1.Text);

                foreach (byte c in bytestr)
                {
                    if (c < 48 || c > 57)
                    {
                        MessageBox.Show("开始学号错误！请输入正确的学号。如，1，2，3，4...");
                        return;
                    }
                }
                if (textBox2.Text == null || textBox2.Text.Length == 0)
                {
                    MessageBox.Show("结束学号错误！请输入结束学号！");
                    return;
                }
                ascii = new ASCIIEncoding();
                bytestr = ascii.GetBytes(textBox2.Text);

                foreach (byte c in bytestr)
                {
                    if (c < 48 || c > 57)
                    {
                        MessageBox.Show("结束学号错误！请输入正确的学号。如，1，2，3，4...");
                        return;
                    }
                }

                try
                {

                    startNum = int.Parse(textBox1.Text);
                }
                catch
                {
                    MessageBox.Show("开始学号错误！超过最大值 " + int.MaxValue.ToString());
                    return;
                }

                try
                {
                    endNum = int.Parse(textBox2.Text);
                }
                catch
                {
                    MessageBox.Show("结束学号错误！超过最大值 {0}" + int.MaxValue.ToString());
                    return;
                }
                start = true;
                textBox1.Enabled = false;
                textBox2.Enabled = false;

                button1.Text = "停止";
                button1.BackColor = System.Drawing.Color.Red;
                timer1.Enabled = true;
            }
            else
            {
                start = false;
                button1.Text = "开始";
                button1.BackColor = System.Drawing.Color.Green;
                
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                timer1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //start = false;
            //textBox1.Enabled = true;
            //textBox2.Enabled = true;
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            System.Random a = new Random(System.DateTime.Now.Millisecond);
            int RandKey = a.Next(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            textBox3.Text = RandKey.ToString();
        }

    }
}
