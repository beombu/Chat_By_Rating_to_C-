using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace AsyncGUISImpleClient
{
    public partial class Client : Form
    {
        Socket server;
        IPAddress serverIPAddress;

        delegate void AppendTextDelegate(Control ctrl, string s);
        AppendTextDelegate _textAppend;

        string nameID;



        public Client()
        {
            InitializeComponent();
            server = new Socket(AddressFamily.InterNetwork,
    SocketType.Stream, ProtocolType.Tcp);
            _textAppend = new AppendTextDelegate(AppendText);
        }

        void AppendText(Control ctrl, string s)
        {
            if (ctrl.InvokeRequired)
                ctrl.Invoke(_textAppend, ctrl, s);
            else
            {
                string source = ctrl.Text;
                ctrl.Text = source + Environment.NewLine + s;
            }
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (server.Connected)
            {
                MsgBoxHelper.Error("이미 연결되어 있습니다.");
                return;
            }
            int port;
            if (!int.TryParse(txtPort.Text, out port))
            {  // port 입력 안 함
                MsgBoxHelper.Warn("포트를 입력하세요");
                txtPort.Focus();
                txtPort.SelectAll();
                return;
            }

            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                serverIPAddress = IPAddress.Loopback;
                txtAddress.Text = serverIPAddress.ToString();
            }
            else
            {
                serverIPAddress = IPAddress.Parse(txtAddress.Text);
            }
            IPEndPoint serverEP = new IPEndPoint(serverIPAddress, port);

            if (string.IsNullOrEmpty(txtNameID.Text))
            {
                MsgBoxHelper.Warn("ID를 입력하세요");
                return;
            }

            nameID = txtNameID.Text;

            try
            {
                server.Connect(serverEP);
                AppendText(txtHistory, "서버와 연결되었습니다");

            } catch (Exception ex) { 
                MsgBoxHelper.Error("연결에 실패했습니다!\n 오류내용: {0}",
                    MessageBoxButtons.OK, ex.Message);
                return;
            }

            SendID();

            AsyncObject obj = new AsyncObject(4096);
            obj.workingSocket = server;
            server.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0,
                DataReceived, obj);
        }

        void SendID()
        {
            byte[] bDts = Encoding.UTF8.GetBytes("ID:" + nameID + ":");
            server.Send(bDts);
            AppendText(txtHistory, "유저가 서버와 연결되었습니다.");
            AppendText(txtHistory, "특정 사용자에게 보낼 때는 사용자 TO:ID:메시지로 입력하시고\n" + "브로드캐스드하려면 BR:메시지를 입력하세요");
            AppendText(txtHistory, "레벨업을 원하신다면 하단에 레벨업버튼을 10번 누르시오");
        }
        void DataReceived(IAsyncResult ar)
        {
            AsyncObject obj = ar.AsyncState as AsyncObject;
            try
            {
                int received = obj.workingSocket.EndReceive(ar);
                if (received <= 0)
                {
                    obj.workingSocket.Disconnect(false);
                    obj.workingSocket.Close();
                    return;
                }
            }
            catch (SocketException ex)
            {
                //Console.WriteLine(ex.Message);
            }



            string text = Encoding.UTF8.GetString(obj.Buffer);
            //AppendText(txtHistory, text);
            string[] tokens = text.Split(':');
            if (tokens[0].Equals("ID"))
            {
                string id = tokens[1];
                AppendText(txtHistory, string.Format("[유저접속]ID:{0}", id));
            }
            else if (tokens[0].Equals("M_ID"))
            {
                string id = tokens[1];
                AppendText(txtHistory, string.Format("[매니저접속]ID:{0}", id));
            }
            else if (tokens[0].Equals("BR"))
            {
                string fromID = tokens[1];
                string msg = tokens[2];
                AppendText(txtHistory, string.Format("[{0}유저의 전체메시지]:{1}", fromID, msg));
            }
            else if (tokens[0].Equals("M_BR"))
            {
                string fromID = tokens[1];
                string msg = tokens[2];
                AppendText(txtHistory, string.Format("[{0}매니저의 전체메시지]:{1}", fromID, msg));
            }
            else if (tokens[0].Equals("TO"))
            {
                string fromID = tokens[1];
                string toID = tokens[2];
                string msg = tokens[3];
                AppendText(txtHistory, string.Format("[{0}유저의 귓속말]:{1}", fromID, msg));
            }
            else if (tokens[0].Equals("M_TO"))
            {
                string fromID = tokens[1];
                string toID = tokens[2];
                string msg = tokens[3];
                AppendText(txtHistory, string.Format("[{0}매니저의 귓속말]:{1}", fromID, msg));
            }
            else if (tokens[0].Equals("Success_LevelUP_S"))
            {
                AppendText(txtHistory, string.Format("실버로 레벨업을 축하드립니다.\n 또 레벨업을 하시려면 레벨업버튼을 15번 누르세요"));
                lvLabel.Text = "실버";
            }
            else if (tokens[0].Equals("Success_LevelUP_G"))
            {
                AppendText(txtHistory, string.Format("골드로 레벨업을 축하드립니다."));
                lvLabel.Text = "골드";
            }
            else if (tokens[0].Equals("M_MT"))
            {
                string fromID = tokens[1];
                string level = tokens[2];
                string msg = tokens[3];
                if (level == "B")
                {
                    AppendText(txtHistory, string.Format("[{0}매니저의 브론즈등급메시지]:{1}", fromID, msg));
                }
                else if (level == "S")
                {
                    AppendText(txtHistory, string.Format("[{0}매니저의 실버등급메시지]:{1}", fromID, msg));
                }
                else if (level == "G")
                {
                    AppendText(txtHistory, string.Format("[{0}매니저의 골드등급메시지]:{1}", fromID, msg));
                }
            }
            else if (tokens[0].Equals("Server"))
            {
                string msg = tokens[1];
                AppendText(txtHistory, string.Format("[server공지]{0}", msg));
            }
            else
            {
                MsgBoxHelper.Warn("userclient DataReceived 오류");
                return;
            }

            obj.clearBuffer();
            try
            {
                obj.workingSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0,
                DataReceived, obj);
            }
            catch
            {

            }


        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!server.IsBound)
            {
                MsgBoxHelper.Warn("서버 연결을 하세요");
                return;
            }
            string text = txtSend.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                MsgBoxHelper.Warn("텍스트를 입력하세요!");
                return;
            }
            //byte[] bDts = Encoding.UTF8.GetBytes(nameID + " : " + text);
            string[] tokens = text.Split(':');
            byte[] bDts = new byte[4096];

            //AppendText(txtHistory, "Client:" + text);
            if (tokens[0].Equals("BR"))
            {
                bDts = Encoding.UTF8.GetBytes("BR:" + nameID + ':' + tokens[1] + ':');
                AppendText(txtHistory, string.Format("[유저의 전체전송]{0}", tokens[1]));

            }
            else if(tokens[0].Equals("TO"))
            {
                bDts = Encoding.UTF8.GetBytes("TO:" + nameID + ":" + tokens[1] + ':' + tokens[2] + ':');
                AppendText(txtHistory, string.Format("[유저가 {0}에게 전송]:{1}", tokens[1], tokens[2]));

            }
            else
            {
                MsgBoxHelper.Warn("userclient btnSend 오류");
                return;
            }
            try
            {
                server.Send(bDts);
            }
            catch { }
            txtSend.Clear();

        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                server.Disconnect(false);
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }
            catch
            { }
        }

        private void Client_Load(object sender, EventArgs e)
        {

        }
        int count;
        private void LevelUp_Click(object sender, EventArgs e)
        {
            if (!server.IsBound)
            {
                MsgBoxHelper.Warn("서버 연결을 하세요");
                return;
            }
            //byte[] bDts = Encoding.UTF8.GetBytes(nameID + " : " + text);
            byte[] bDts = new byte[4096];



            count++;
            if(count == 10)
            {
                bDts = Encoding.UTF8.GetBytes("LV:" + nameID + ":S:");
                server.Send(bDts);
            }
            if (count == 25)
            {
                bDts = Encoding.UTF8.GetBytes("LV:" + nameID + ":G:");
                server.Send(bDts);
            }
        }
    }
}
