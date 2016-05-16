namespace MapVisualBuilder
{
    partial class Form1
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
            this.CanvasContainer = new System.Windows.Forms.Panel();
            this.Canvas = new System.Windows.Forms.Panel();
            this.OpenGraphButton = new System.Windows.Forms.Button();
            this.ExportButton = new System.Windows.Forms.Button();
            this.CurrentVertexComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.XCoordTextBox = new System.Windows.Forms.TextBox();
            this.YCoordTextBox = new System.Windows.Forms.TextBox();
            this.ProfitTextBox = new System.Windows.Forms.TextBox();
            this.DangerTextBox = new System.Windows.Forms.TextBox();
            this.TagTextBox = new System.Windows.Forms.TextBox();
            this.EdgedVertexComboBox = new System.Windows.Forms.ComboBox();
            this.IsEdgedCheckBox = new System.Windows.Forms.CheckBox();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.DeleteCurrentVertexButton = new System.Windows.Forms.Button();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.CanvasContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // CanvasContainer
            // 
            this.CanvasContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CanvasContainer.AutoScroll = true;
            this.CanvasContainer.Controls.Add(this.Canvas);
            this.CanvasContainer.Location = new System.Drawing.Point(230, 12);
            this.CanvasContainer.Name = "CanvasContainer";
            this.CanvasContainer.Size = new System.Drawing.Size(487, 533);
            this.CanvasContainer.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.White;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(200, 100);
            this.Canvas.TabIndex = 0;
            this.Canvas.Click += new System.EventHandler(this.Canvas_Click);
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            // 
            // OpenGraphButton
            // 
            this.OpenGraphButton.Location = new System.Drawing.Point(12, 12);
            this.OpenGraphButton.Name = "OpenGraphButton";
            this.OpenGraphButton.Size = new System.Drawing.Size(211, 23);
            this.OpenGraphButton.TabIndex = 1;
            this.OpenGraphButton.Text = "Open Graph";
            this.OpenGraphButton.UseVisualStyleBackColor = true;
            this.OpenGraphButton.Click += new System.EventHandler(this.OpenGraphButton_Click);
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(12, 61);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(212, 23);
            this.ExportButton.TabIndex = 2;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // CurrentVertexComboBox
            // 
            this.CurrentVertexComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CurrentVertexComboBox.FormattingEnabled = true;
            this.CurrentVertexComboBox.Location = new System.Drawing.Point(12, 211);
            this.CurrentVertexComboBox.Name = "CurrentVertexComboBox";
            this.CurrentVertexComboBox.Size = new System.Drawing.Size(212, 21);
            this.CurrentVertexComboBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 293);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Profit:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Danger:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 342);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tag:";
            // 
            // XCoordTextBox
            // 
            this.XCoordTextBox.Location = new System.Drawing.Point(61, 240);
            this.XCoordTextBox.Name = "XCoordTextBox";
            this.XCoordTextBox.Size = new System.Drawing.Size(163, 20);
            this.XCoordTextBox.TabIndex = 9;
            // 
            // YCoordTextBox
            // 
            this.YCoordTextBox.Location = new System.Drawing.Point(61, 263);
            this.YCoordTextBox.Name = "YCoordTextBox";
            this.YCoordTextBox.Size = new System.Drawing.Size(163, 20);
            this.YCoordTextBox.TabIndex = 10;
            // 
            // ProfitTextBox
            // 
            this.ProfitTextBox.Location = new System.Drawing.Point(61, 290);
            this.ProfitTextBox.Name = "ProfitTextBox";
            this.ProfitTextBox.Size = new System.Drawing.Size(163, 20);
            this.ProfitTextBox.TabIndex = 11;
            // 
            // DangerTextBox
            // 
            this.DangerTextBox.Location = new System.Drawing.Point(61, 318);
            this.DangerTextBox.Name = "DangerTextBox";
            this.DangerTextBox.Size = new System.Drawing.Size(163, 20);
            this.DangerTextBox.TabIndex = 12;
            // 
            // TagTextBox
            // 
            this.TagTextBox.Location = new System.Drawing.Point(61, 342);
            this.TagTextBox.Name = "TagTextBox";
            this.TagTextBox.Size = new System.Drawing.Size(163, 20);
            this.TagTextBox.TabIndex = 13;
            // 
            // EdgedVertexComboBox
            // 
            this.EdgedVertexComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EdgedVertexComboBox.FormattingEnabled = true;
            this.EdgedVertexComboBox.Location = new System.Drawing.Point(12, 402);
            this.EdgedVertexComboBox.Name = "EdgedVertexComboBox";
            this.EdgedVertexComboBox.Size = new System.Drawing.Size(212, 21);
            this.EdgedVertexComboBox.TabIndex = 14;
            // 
            // IsEdgedCheckBox
            // 
            this.IsEdgedCheckBox.AutoSize = true;
            this.IsEdgedCheckBox.Location = new System.Drawing.Point(12, 429);
            this.IsEdgedCheckBox.Name = "IsEdgedCheckBox";
            this.IsEdgedCheckBox.Size = new System.Drawing.Size(68, 17);
            this.IsEdgedCheckBox.TabIndex = 15;
            this.IsEdgedCheckBox.Text = "Is Edged";
            this.IsEdgedCheckBox.UseVisualStyleBackColor = true;
            this.IsEdgedCheckBox.CheckedChanged += new System.EventHandler(this.IsEdgedCheckBox_CheckedChanged);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "openFileDialog1";
            // 
            // DeleteCurrentVertexButton
            // 
            this.DeleteCurrentVertexButton.Location = new System.Drawing.Point(149, 368);
            this.DeleteCurrentVertexButton.Name = "DeleteCurrentVertexButton";
            this.DeleteCurrentVertexButton.Size = new System.Drawing.Size(74, 24);
            this.DeleteCurrentVertexButton.TabIndex = 22;
            this.DeleteCurrentVertexButton.Text = "Delete";
            this.DeleteCurrentVertexButton.UseVisualStyleBackColor = true;
            this.DeleteCurrentVertexButton.Click += new System.EventHandler(this.DeleteCurrentVertexButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(12, 132);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(212, 23);
            this.RefreshButton.TabIndex = 23;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 545);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.DeleteCurrentVertexButton);
            this.Controls.Add(this.IsEdgedCheckBox);
            this.Controls.Add(this.EdgedVertexComboBox);
            this.Controls.Add(this.TagTextBox);
            this.Controls.Add(this.DangerTextBox);
            this.Controls.Add(this.ProfitTextBox);
            this.Controls.Add(this.YCoordTextBox);
            this.Controls.Add(this.XCoordTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CurrentVertexComboBox);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.OpenGraphButton);
            this.Controls.Add(this.CanvasContainer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.CanvasContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel CanvasContainer;
        private System.Windows.Forms.Panel Canvas;
        private System.Windows.Forms.Button OpenGraphButton;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.ComboBox CurrentVertexComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox XCoordTextBox;
        private System.Windows.Forms.TextBox YCoordTextBox;
        private System.Windows.Forms.TextBox ProfitTextBox;
        private System.Windows.Forms.TextBox DangerTextBox;
        private System.Windows.Forms.TextBox TagTextBox;
        private System.Windows.Forms.ComboBox EdgedVertexComboBox;
        private System.Windows.Forms.CheckBox IsEdgedCheckBox;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Button DeleteCurrentVertexButton;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Button RefreshButton;
    }
}

