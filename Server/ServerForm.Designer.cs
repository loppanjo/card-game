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
			this.btStartServer = new System.Windows.Forms.Button();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btStopServer = new System.Windows.Forms.Button();
			this.rtbConsole = new System.Windows.Forms.RichTextBox();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btStartServer
			// 
			this.btStartServer.Location = new System.Drawing.Point(3, 3);
			this.btStartServer.Name = "btStartServer";
			this.btStartServer.Size = new System.Drawing.Size(130, 23);
			this.btStartServer.TabIndex = 0;
			this.btStartServer.Text = "Start Server";
			this.btStartServer.UseVisualStyleBackColor = true;
			this.btStartServer.Click += new System.EventHandler(this.btStartServer_Click);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanel1.Controls.Add(this.btStartServer);
			this.flowLayoutPanel1.Controls.Add(this.btStopServer);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(260, 29);
			this.flowLayoutPanel1.TabIndex = 1;
			// 
			// btStopServer
			// 
			this.btStopServer.Enabled = false;
			this.btStopServer.Location = new System.Drawing.Point(139, 3);
			this.btStopServer.Name = "btStopServer";
			this.btStopServer.Size = new System.Drawing.Size(75, 23);
			this.btStopServer.TabIndex = 1;
			this.btStopServer.Text = "Stop Server";
			this.btStopServer.UseVisualStyleBackColor = true;
			// 
			// rtbConsole
			// 
			this.rtbConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtbConsole.Location = new System.Drawing.Point(12, 44);
			this.rtbConsole.Name = "rtbConsole";
			this.rtbConsole.ReadOnly = true;
			this.rtbConsole.Size = new System.Drawing.Size(260, 206);
			this.rtbConsole.TabIndex = 2;
			this.rtbConsole.Text = "";
			// 
			// ServerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.rtbConsole);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ServerForm";
			this.Text = "Shithead Server";
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btStartServer;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.RichTextBox rtbConsole;
		private System.Windows.Forms.Button btStopServer;
	}
}

