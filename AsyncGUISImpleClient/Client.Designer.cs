namespace AsyncGUISImpleClient
{
    partial class Client
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNameID = new System.Windows.Forms.TextBox();
            this.LevelUp = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lvLabel = new System.Windows.Forms.Label();
            this.txtHistory = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "서버주소";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "포트";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "보낼 텍스트";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(71, 19);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(129, 21);
            this.txtAddress.TabIndex = 3;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(250, 19);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 21);
            this.txtPort.TabIndex = 4;
            this.txtPort.Text = "5000";
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(87, 319);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(231, 21);
            this.txtSend.TabIndex = 6;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(360, 19);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 48);
            this.btnConnect.TabIndex = 7;
            this.btnConnect.Text = "연결";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(360, 319);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(73, 27);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "전송";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(215, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "ID";
            // 
            // txtNameID
            // 
            this.txtNameID.Location = new System.Drawing.Point(250, 46);
            this.txtNameID.Name = "txtNameID";
            this.txtNameID.Size = new System.Drawing.Size(100, 21);
            this.txtNameID.TabIndex = 10;
            // 
            // LevelUp
            // 
            this.LevelUp.Location = new System.Drawing.Point(148, 364);
            this.LevelUp.Name = "LevelUp";
            this.LevelUp.Size = new System.Drawing.Size(73, 34);
            this.LevelUp.TabIndex = 11;
            this.LevelUp.Text = "레벨업";
            this.LevelUp.UseVisualStyleBackColor = true;
            this.LevelUp.Click += new System.EventHandler(this.LevelUp_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 374);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "현재레벨 :";
            // 
            // lvLabel
            // 
            this.lvLabel.AutoSize = true;
            this.lvLabel.Location = new System.Drawing.Point(85, 375);
            this.lvLabel.Name = "lvLabel";
            this.lvLabel.Size = new System.Drawing.Size(41, 12);
            this.lvLabel.TabIndex = 13;
            this.lvLabel.Text = "브론즈";
            // 
            // txtHistory
            // 
            this.txtHistory.Location = new System.Drawing.Point(14, 73);
            this.txtHistory.Name = "txtHistory";
            this.txtHistory.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtHistory.Size = new System.Drawing.Size(421, 236);
            this.txtHistory.TabIndex = 14;
            this.txtHistory.Text = "";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 421);
            this.Controls.Add(this.txtHistory);
            this.Controls.Add(this.lvLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LevelUp);
            this.Controls.Add(this.txtNameID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Client";
            this.Text = "UserClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            this.Load += new System.EventHandler(this.Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNameID;
        private System.Windows.Forms.Button LevelUp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lvLabel;
        private System.Windows.Forms.RichTextBox txtHistory;
    }
}

