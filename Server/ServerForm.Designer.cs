namespace Server
{
	partial class ServerForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
			this.btToggleServer = new System.Windows.Forms.Button();
			this.rtbConsole = new System.Windows.Forms.RichTextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.numPort = new System.Windows.Forms.NumericUpDown();
			this.lbServerURI = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnStartGame = new System.Windows.Forms.Button();
			this.gbConnections = new System.Windows.Forms.GroupBox();
			this.lbxConnections = new System.Windows.Forms.ListBox();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.gbConnections.SuspendLayout();
			this.SuspendLayout();
			// 
			// btToggleServer
			// 
			this.btToggleServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btToggleServer.Location = new System.Drawing.Point(0, 0);
			this.btToggleServer.Margin = new System.Windows.Forms.Padding(0);
			this.btToggleServer.Name = "btToggleServer";
			this.btToggleServer.Size = new System.Drawing.Size(269, 23);
			this.btToggleServer.TabIndex = 0;
			this.btToggleServer.Text = "Start Server";
			this.btToggleServer.UseVisualStyleBackColor = true;
			this.btToggleServer.Click += new System.EventHandler(this.btToggleServer_Click);
			// 
			// rtbConsole
			// 
			this.rtbConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtbConsole.Location = new System.Drawing.Point(0, 58);
			this.rtbConsole.Name = "rtbConsole";
			this.rtbConsole.ReadOnly = true;
			this.rtbConsole.Size = new System.Drawing.Size(269, 210);
			this.rtbConsole.TabIndex = 2;
			this.rtbConsole.TabStop = false;
			this.rtbConsole.Text = "";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.numPort);
			this.panel1.Controls.Add(this.lbServerURI);
			this.panel1.Location = new System.Drawing.Point(0, 24);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(269, 28);
			this.panel1.TabIndex = 1;
			// 
			// numPort
			// 
			this.numPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.numPort.Location = new System.Drawing.Point(38, 4);
			this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.numPort.Name = "numPort";
			this.numPort.Size = new System.Drawing.Size(231, 20);
			this.numPort.TabIndex = 4;
			this.numPort.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
			// 
			// lbServerURI
			// 
			this.lbServerURI.AutoSize = true;
			this.lbServerURI.Location = new System.Drawing.Point(3, 6);
			this.lbServerURI.Name = "lbServerURI";
			this.lbServerURI.Size = new System.Drawing.Size(29, 13);
			this.lbServerURI.TabIndex = 1;
			this.lbServerURI.Text = "Port:";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.btToggleServer);
			this.panel2.Controls.Add(this.panel1);
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(269, 52);
			this.panel2.TabIndex = 3;
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.Controls.Add(this.btnStartGame);
			this.panel3.Controls.Add(this.rtbConsole);
			this.panel3.Controls.Add(this.panel2);
			this.panel3.Location = new System.Drawing.Point(12, 12);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(269, 298);
			this.panel3.TabIndex = 4;
			// 
			// btnStartGame
			// 
			this.btnStartGame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStartGame.Location = new System.Drawing.Point(0, 272);
			this.btnStartGame.Name = "btnStartGame";
			this.btnStartGame.Size = new System.Drawing.Size(269, 23);
			this.btnStartGame.TabIndex = 4;
			this.btnStartGame.Text = "Start Game";
			this.btnStartGame.UseVisualStyleBackColor = true;
			this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
			// 
			// gbConnections
			// 
			this.gbConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbConnections.Controls.Add(this.lbxConnections);
			this.gbConnections.Location = new System.Drawing.Point(287, 9);
			this.gbConnections.Name = "gbConnections";
			this.gbConnections.Size = new System.Drawing.Size(165, 301);
			this.gbConnections.TabIndex = 5;
			this.gbConnections.TabStop = false;
			this.gbConnections.Text = "Connections (0)";
			// 
			// lbxConnections
			// 
			this.lbxConnections.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbxConnections.FormattingEnabled = true;
			this.lbxConnections.Location = new System.Drawing.Point(3, 16);
			this.lbxConnections.Name = "lbxConnections";
			this.lbxConnections.Size = new System.Drawing.Size(159, 282);
			this.lbxConnections.TabIndex = 3;
			// 
			// ServerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(464, 322);
			this.Controls.Add(this.gbConnections);
			this.Controls.Add(this.panel3);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(480, 360);
			this.Name = "ServerForm";
			this.Text = "Shithead Server";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.gbConnections.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btToggleServer;
		private System.Windows.Forms.RichTextBox rtbConsole;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lbServerURI;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.GroupBox gbConnections;
		private System.Windows.Forms.ListBox lbxConnections;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Button btnStartGame;
    }
}

