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
			this.lbServerURI = new System.Windows.Forms.Label();
			this.tbServerURI = new System.Windows.Forms.TextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.gbConnections = new System.Windows.Forms.GroupBox();
			this.lbxConnections = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
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
			this.btToggleServer.Size = new System.Drawing.Size(604, 23);
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
			this.rtbConsole.Location = new System.Drawing.Point(0, 55);
			this.rtbConsole.Name = "rtbConsole";
			this.rtbConsole.ReadOnly = true;
			this.rtbConsole.Size = new System.Drawing.Size(604, 230);
			this.rtbConsole.TabIndex = 2;
			this.rtbConsole.TabStop = false;
			this.rtbConsole.Text = "";
			// 
			// lbServerURI
			// 
			this.lbServerURI.AutoSize = true;
			this.lbServerURI.Location = new System.Drawing.Point(3, 32);
			this.lbServerURI.Name = "lbServerURI";
			this.lbServerURI.Size = new System.Drawing.Size(63, 13);
			this.lbServerURI.TabIndex = 1;
			this.lbServerURI.Text = "Server URI:";
			// 
			// tbServerURI
			// 
			this.tbServerURI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbServerURI.Location = new System.Drawing.Point(72, 29);
			this.tbServerURI.Name = "tbServerURI";
			this.tbServerURI.Size = new System.Drawing.Size(532, 20);
			this.tbServerURI.TabIndex = 1;
			this.tbServerURI.Text = "http://*:3000";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.lbServerURI);
			this.panel2.Controls.Add(this.btToggleServer);
			this.panel2.Controls.Add(this.tbServerURI);
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(604, 52);
			this.panel2.TabIndex = 3;
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.Controls.Add(this.button2);
			this.panel3.Controls.Add(this.button1);
			this.panel3.Controls.Add(this.rtbConsole);
			this.panel3.Controls.Add(this.panel2);
			this.panel3.Location = new System.Drawing.Point(12, 12);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(604, 311);
			this.panel3.TabIndex = 4;
			// 
			// gbConnections
			// 
			this.gbConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbConnections.Controls.Add(this.lbxConnections);
			this.gbConnections.Location = new System.Drawing.Point(622, 9);
			this.gbConnections.Name = "gbConnections";
			this.gbConnections.Size = new System.Drawing.Size(165, 314);
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
			this.lbxConnections.Size = new System.Drawing.Size(159, 295);
			this.lbxConnections.TabIndex = 3;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(0, 288);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(498, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "Start Game";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(504, 288);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(100, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "Set Gamerules";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// ServerForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(799, 335);
			this.Controls.Add(this.gbConnections);
			this.Controls.Add(this.panel3);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(480, 360);
			this.Name = "ServerForm";
			this.Text = "Shithead Server";
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.gbConnections.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btToggleServer;
		private System.Windows.Forms.RichTextBox rtbConsole;
		private System.Windows.Forms.Label lbServerURI;
		private System.Windows.Forms.TextBox tbServerURI;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.GroupBox gbConnections;
		private System.Windows.Forms.ListBox lbxConnections;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
	}
}

