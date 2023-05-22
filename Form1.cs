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
using static System.Net.Mime.MediaTypeNames;

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
            ReadLog("初始化完成");

        }

        public void ReadLog(string log)
        {
            richTextBox1.AppendText(DateTime.Now.ToString("yyyy/mm/dd HH:mm:ss;fff：") + "\r\n" + log + "\r\n");
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

            string halconfilepath = choose_Halcon_file();

            if (halconfilepath.Contains(".hdev"))
            {
                string hdevpath = halconfilepath;
                HDevProgram hprogram = new HDevProgram(hdevpath);
                HDevProgramCall hprocall = new HDevProgramCall(hprogram);
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
                 HDevEngine MyEngine = new HDevEngine();

                // Halcon程序
                 HDevProcedure hprocedure;

                // 定义hdvp程序执行实例
                 HDevProcedureCall ProcedureCall;

                // 定义hdvp程序显示窗口
                 HWindow Window;

             //   var procedurePath = AppDomain.CurrentDomain.BaseDirectory;//路径为项目文件下的Debug目录
              

                // 定义存放hdvp程序路径的字符串
                string ProgramPathString = halconfilepath;

                // 程序引擎处理路径
                string ProcedurePath = @"C:\Users\Administrator\Desktop";

                // halcon固定显示窗口
                 HDevOpFixedWindowImpl myHDevOpFixedWindowImpl;

                 MyEngine.SetProcedurePath(ProcedurePath);
                 // 窗口对象赋值
                 Window = M_ZKHwindows.hWindowControl.HalconWindow;
                 // 设置窗口显示模式
                 Window.SetDraw("margin");
                 // 设置字体显示颜色
                 Window.SetColor("red");
                 // 设置显示轮廓线宽
                 Window.SetLineWidth(2);

                 // 赋值固定显示窗口
                 myHDevOpFixedWindowImpl = new HDevOpFixedWindowImpl(Window);
                 // 设置固定显示窗口
                 MyEngine.SetHDevOperators(myHDevOpFixedWindowImpl);
                 try
                 {
                     ReadLog("开始创建halcon程序对象");
                    // 创建halcon程序对象
                    hprocedure = new HDevProcedure("test");
                    ReadLog("创建halcon程序对象结束");

                    ReadLog("开始创建halcon程序调用对象");
                    // 创建halcon程序调用对象
                    ProcedureCall = new HDevProcedureCall(hprocedure);
                    ReadLog("创建halcon程序调用对象结束");
                    MessageBox.Show("OK");

                    // 确定图像路径
                    // HTuple img_path = "D:/1.png";
                    // 设置halcon程序输入
                    //ProcedureCall.SetInputCtrlParamTuple("img_path", img_path);
                    // 执行halcon
                    ReadLog("开始执行");
                    ProcedureCall.Execute(); //SaveData
                    ReadLog("执行结束");
                    // 获取输出对象
                    HObject Image1 = ProcedureCall.GetOutputIconicParamObject("Image1"); //获取灰度后的图像
                    // 显示输出对象
                    HObject Regions = ProcedureCall.GetOutputIconicParamObject("Regions"); //获取灰度后的图像
                    
                     M_ZKHwindows.NowImage= Image1;
                     HOperatorSet.DispObj(Regions, M_ZKHwindows.hWindowControl.HalconWindow);
                 }




                 catch (Exception exp)
                 {
                     MessageBox.Show(exp.Message);
                 }



            }

        }









        private void button3_Click(object sender, EventArgs e)
        {
            if (M_ZKHwindows.NowImage != null)
            {
                HTuple C, X, R;
                HObject circleww;
                HOperatorSet.DrawCircle(M_ZKHwindows.hWindowControl.HalconWindow, out C ,out X ,out R);
                HOperatorSet.GenCircle(out circleww, C, X, R);
                HOperatorSet.DispObj(circleww, M_ZKHwindows.hWindowControl.HalconWindow);
            }
        }
    }
}
