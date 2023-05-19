using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace halcon引擎测试
{
    public partial class Form1 : Form
    {
        ZKHwindows M_ZKHwindows = new ZKHwindows();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            M_ZKHwindows.Dock = DockStyle.Fill; 
            panel1.Controls.Add(M_ZKHwindows);  

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string imgpath = choose_file();
            HObject img;
            HOperatorSet.ReadImage(out img, imgpath);
            M_ZKHwindows.NowImage = img;
        }
        public string choose_file()
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                // 设置对话框的标题和筛选条件
                openFileDialog1.Title = "选择图像文件";
                openFileDialog1.Filter = "图像文件 (*.bmp, *.jpg, *.jpeg, *.png) | *.bmp; *.jpg; *.jpeg; *.png";

                // 打开文件对话框并检查用户是否选择了文件
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // 获取所选文件的完整路径
                    string selectedImagePath = openFileDialog1.FileName;

                    // 获取所选文件的名称
                    return selectedImagePath;

                    // 在控制台输出所选文件的名称
                    // Console.WriteLine("所选文件的名称是: " + selectedImageName);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public string choose_Halcon_file()
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                // 设置对话框的标题和筛选条件
                openFileDialog1.Title = "选择halcon文件";
                openFileDialog1.Filter = "halcon文件 (*.hdvp, *.hdev) | *.hdvp; *.hdev; ";

                // 打开文件对话框并检查用户是否选择了文件
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // 获取所选文件的完整路径
                    string selectedImagePath = openFileDialog1.FileName;

                    // 获取所选文件的名称
                    return selectedImagePath;

                    // 在控制台输出所选文件的名称
                    // Console.WriteLine("所选文件的名称是: " + selectedImageName);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           string  halconfilepath = choose_Halcon_file();
           try
           {
               if (halconfilepath.Contains(".hdev"))
               {
                     string   hdevpath = halconfilepath;
                     HDevProgram  hprogram = new HDevProgram(hdevpath);
                     HDevProgramCall  hprocall = new HDevProgramCall(hprogram);
                     hprocall.Execute();
                    //获得hdev程序里SelectedRegions1变量内容并显示
                    HObject region = hprocall.GetIconicVarRegion("Regions");
                   HOperatorSet.DispObj(region, M_ZKHwindows.hWindowControl.HalconWindow);
                 //  M_ZKHwindows.hWindowControl.HalconWindow.DispObj(region);
                   //执行hdev程序并获取参数
                  
                //   var files = hprocall.GetCtrlVarTuple("ImageFiles");

               }
               else if (halconfilepath.Contains(".hdvp"))
               {

               }
               else
               {
                   MessageBox.Show("崽种，这可不是我要的");
               }
            }
           catch (Exception exception)
           {
               MessageBox.Show("崽种，这可不是我要的");
            }
         
        }
    }
}
