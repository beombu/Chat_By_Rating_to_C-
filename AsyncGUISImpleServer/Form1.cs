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

namespace ASyncGuISimpleServer
{
    public partial class MultiChat : Form
    {
        Socket server;
        IPAddress serverIPAddress;
        delegate void AppendTextDelegate(Control ctrl, string s);
        AppendTextDelegate _textAppend;
        Dictionary<string, Socket> connentedClients;
        int clientNum;

        List<string> bronze = new List<string>();//여기 안에는 id를 넣을 것
        List<string> sliver = new List<string>();
        List<string> gold = new List<string>();

        public MultiChat()
        {
            InitializeComponent();
            server = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            _textAppend = new AppendTextDelegate(AppendText);
            connentedClients = new Dictionary<string, Socket>();

            clientNum = 0;
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

        private void btnStart_Click(object sender, EventArgs e)
        {
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
            server.Bind(serverEP);
            server.Listen(10);
            AppendText(txtHistory, string.Format("서버시작: @{0}", serverEP));
            server.BeginAccept(AcceptCallback, null);
        }

        void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = server.EndAccept(ar);

                AppendText(txtHistory, string.Format("클라이언트({0})가 연결되었습니다.",
                    client.RemoteEndPoint));
                server.BeginAccept(AcceptCallback, null);

                //recieve ID
                AsyncObject obj = new AsyncObject(4096);
                obj.workingSocket = client;
                client.BeginReceive(obj.Buffer, 0, obj.BufferSize, 0,
                    DataReceived, obj);
            }
            catch { }

        }

        void DataReceived(IAsyncResult ar)
        {
            AsyncObject obj = ar.AsyncState as AsyncObject;
            try
            {
                int received = obj.workingSocket.EndReceive(ar);
                if (received <= 0)// 상대방이 종료한 경우
                {
                    foreach (KeyValuePair<string, Socket> clients in connentedClients)
                    {
                        if (obj.workingSocket == clients.Value)
                        {
                            string key = clients.Key;
                            try
                            {
                                connentedClients.Remove(key);
                                AppendText(txtHistory, string.Format("접속해제 완료:{0}", key));
                            }
                            catch { }
                            break;
                        }
                    }
                    obj.workingSocket.Disconnect(false);
                    obj.workingSocket.Close();
                    clientNum--;
                    return;
                }
            }
            catch { }

            string text = Encoding.UTF8.GetString(obj.Buffer);
            //AppendText(txtHistory, text);//필터 없이 요청 프로토콜을 보여줌

            string[] tokens = text.Split(':');
            string myID = null;
            string fromID = null;
            string toID = null;
            string code = tokens[0];
            if (code.Equals("ID"))
            {
                clientNum++;
                fromID = tokens[1].Trim();
                bronze.Add(fromID);
                foreach (string i in bronze)
                {
                    Console.WriteLine("브론즈는 = {0}", i);
                }
                AppendText(txtHistory, string.Format("[유저접속 {0}명]ID:{1},{2}",
                   clientNum, fromID, obj.workingSocket.RemoteEndPoint.ToString()));
                connentedClients.Add(fromID, obj.workingSocket);

                sendAll(obj.workingSocket, obj.Buffer);
            }
            else if (code.Equals("M_ID"))
            {
                clientNum++;
                fromID = tokens[1].Trim();
                AppendText(txtHistory, string.Format("[매니저접속 {0}명]ID:{1},{2}",
                   clientNum, fromID, obj.workingSocket.RemoteEndPoint.ToString()));
                connentedClients.Add(fromID, obj.workingSocket);
                sendAll(obj.workingSocket, obj.Buffer);
            }
            else if ((code.Equals("BR")))
            {
                fromID = tokens[1].Trim();
                string msg = tokens[2].Trim();
                AppendText(txtHistory, string.Format("[{0}유저의 전체메시지]:{1}", fromID, msg));
                sendAll(obj.workingSocket, obj.Buffer);
            }
            else if ((code.Equals("M_BR")))
            {
                fromID = tokens[1].Trim();
                string msg = tokens[2].Trim();
                AppendText(txtHistory, string.Format("[{0}매니저의 전체메시지]:{1}", fromID, msg));
                sendAll(obj.workingSocket, obj.Buffer);
            }
            else if ((code.Equals("TO")))
            {
                fromID = tokens[1].Trim();
                toID = tokens[2].Trim();
                string msg = tokens[3];
                string rMsg = "[From:" + fromID + "][TO:" + toID + "]" + msg;
                AppendText(txtHistory, rMsg);
                connentedClients.TryGetValue(toID, out Socket socket);
                sendTo(socket, obj.Buffer);
            }
            else if ((code.Equals("M_TO")))
            {
                fromID = tokens[1].Trim();
                toID = tokens[2].Trim();
                string msg = tokens[3];
                string rMsg = "[From:" + fromID + "][TO:" + toID + "]" + msg;
                AppendText(txtHistory, rMsg);
                connentedClients.TryGetValue(toID, out Socket socket);
                sendTo(socket, obj.Buffer);
            }
            else if ((code.Equals("LV")))
            {
                myID = tokens[1].Trim();
                string level = tokens[2].Trim();
                if (level == "S")
                {
                    string rMsg = myID + "가 실버레벨업 요청";
                    sliver.Add(myID);
                    AppendText(txtHistory, rMsg);
                    obj.workingSocket.Send(Encoding.UTF8.GetBytes("Success_LevelUP_S:"));
                }
                else if (level == "G")
                {
                    string rMsg = myID + "가 골드레벨업 요청";
                    gold.Add(myID);
                    AppendText(txtHistory, rMsg);
                    obj.workingSocket.Send(Encoding.UTF8.GetBytes("Success_LevelUP_G:"));
                }

            }
            else if ((code.Equals("M_MT")))
            {
                fromID = tokens[1].Trim();
                string level = tokens[2].Trim();
                string msg = tokens[3].Trim();
                if (level == "B")
                {
                    AppendText(txtHistory, string.Format("[{0}매니저의 브론즈등급메시지]:{1}", fromID, msg));
                    sendGroup(bronze, obj.Buffer);
                }
                else if (level == "S")
                {
                    AppendText(txtHistory, string.Format("[{0}매니저의 실버등급메시지]:{1}", fromID, msg));
                    sendGroup(sliver, obj.Buffer);
                }
                else if (level == "G")
                {
                    AppendText(txtHistory, string.Format("[{0}매니저의 골드등급메시지]:{1}", fromID, msg));
                    sendGroup(gold, obj.Buffer);
                }
            }
            else
            {
                MsgBoxHelper.Warn("server DataReceived 오류");
                return;
            }

            obj.clearBuffer();
            obj.workingSocket.BeginReceive(obj.Buffer, 0, obj.BufferSize,
                0, DataReceived, obj);

        }
        void sendTo(Socket socket, byte[] buffer) {
            try
            {
                socket.Send(buffer);

            }
            catch { socket.Dispose(); }
        }

        void sendGroup(List<string> group, byte[] buffer)
        {
            foreach (string groupID in group)
            {
                connentedClients.TryGetValue(groupID, out Socket socket);
                try
                {
                    socket.Send(buffer);

                }
                catch { socket.Dispose(); }
            }
        }


        void sendAll(Socket except, byte[] buffer)
        {
            foreach (Socket socket in connentedClients.Values)
            {
                if (except != socket)
                {
                    try
                    {
                        socket.Send(buffer);
                    }
                    catch
                    {
                        try { socket.Dispose(); } catch { }
                    }
                }
            }


        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!server.IsBound)
            {
                MsgBoxHelper.Warn("서버를 실행하세요");
                return;
            }
            string text = txtSend.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                MsgBoxHelper.Warn("텍스트를 입력하세요");
                return;
            }
            //if (!Client.IsBound) return;
            byte[] bDts = Encoding.UTF8.GetBytes("Server:" + text);
            AppendText(txtHistory, "Server:" + text);
            //Client.Send(bDts);

            sendAll(null, bDts);

            txtSend.Clear();
        }
    } 
}
