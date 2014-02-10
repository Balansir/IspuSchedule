namespace TestProject
{
	partial class MainForm
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
			this.butBeginTest = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.grdGroups = new System.Windows.Forms.DataGridView();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.grdSubjects = new System.Windows.Forms.DataGridView();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdGroups)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdSubjects)).BeginInit();
			this.SuspendLayout();
			// 
			// butBeginTest
			// 
			this.butBeginTest.Location = new System.Drawing.Point(12, 12);
			this.butBeginTest.Name = "butBeginTest";
			this.butBeginTest.Size = new System.Drawing.Size(75, 23);
			this.butBeginTest.TabIndex = 0;
			this.butBeginTest.Text = "Begin";
			this.butBeginTest.UseVisualStyleBackColor = true;
			this.butBeginTest.Click += new System.EventHandler(this.ButBeginTestClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.grdGroups);
			this.groupBox1.Location = new System.Drawing.Point(12, 41);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(928, 182);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Faculties and groups";
			// 
			// grdGroups
			// 
			this.grdGroups.AllowUserToAddRows = false;
			this.grdGroups.AllowUserToDeleteRows = false;
			this.grdGroups.AllowUserToResizeRows = false;
			this.grdGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.grdGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdGroups.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdGroups.Location = new System.Drawing.Point(3, 16);
			this.grdGroups.Name = "grdGroups";
			this.grdGroups.ReadOnly = true;
			this.grdGroups.RowHeadersVisible = false;
			this.grdGroups.Size = new System.Drawing.Size(922, 163);
			this.grdGroups.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.grdSubjects);
			this.groupBox2.Location = new System.Drawing.Point(12, 229);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(928, 349);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Subjects";
			// 
			// grdSubjects
			// 
			this.grdSubjects.AllowUserToAddRows = false;
			this.grdSubjects.AllowUserToDeleteRows = false;
			this.grdSubjects.AllowUserToResizeRows = false;
			this.grdSubjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.grdSubjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdSubjects.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdSubjects.Location = new System.Drawing.Point(3, 16);
			this.grdSubjects.Name = "grdSubjects";
			this.grdSubjects.ReadOnly = true;
			this.grdSubjects.RowHeadersVisible = false;
			this.grdSubjects.Size = new System.Drawing.Size(922, 330);
			this.grdSubjects.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(952, 590);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butBeginTest);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MainForm";
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdGroups)).EndInit();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdSubjects)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button butBeginTest;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.DataGridView grdGroups;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.DataGridView grdSubjects;
	}
}