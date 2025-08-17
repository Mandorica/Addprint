namespace Addprint
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBox = new ComboBox();
            folderButton = new Button();
            printButton = new Button();
            txtFolderPath = new TextBox();
            comboBox2 = new ComboBox();
            SuspendLayout();
            // 
            // comboBox
            // 
            comboBox.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            comboBox.FormattingEnabled = true;
            comboBox.Location = new Point(41, 49);
            comboBox.Name = "comboBox";
            comboBox.Size = new Size(121, 25);
            comboBox.TabIndex = 0;
            // 
            // folderButton
            // 
            folderButton.Location = new Point(344, 51);
            folderButton.Name = "folderButton";
            folderButton.Size = new Size(75, 23);
            folderButton.TabIndex = 1;
            folderButton.Text = "파일 찾기";
            folderButton.UseVisualStyleBackColor = true;
            folderButton.Click += folderButton_Click;
            // 
            // printButton
            // 
            printButton.Location = new Point(649, 51);
            printButton.Name = "printButton";
            printButton.Size = new Size(75, 23);
            printButton.TabIndex = 2;
            printButton.Text = "프린트";
            printButton.UseVisualStyleBackColor = true;
            printButton.Click += printButton_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(238, 93);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new Size(306, 23);
            txtFolderPath.TabIndex = 3;
            // 
            // comboBox２
            // 
            comboBox2.Font = new Font("맑은 고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(41, 181);
            comboBox2.Name = "comboBox２";
            comboBox2.Size = new Size(121, 25);
            comboBox2.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(comboBox2);
            Controls.Add(txtFolderPath);
            Controls.Add(printButton);
            Controls.Add(folderButton);
            Controls.Add(comboBox);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox;
        private Button folderButton;
        private Button printButton;
        private TextBox txtFolderPath;
        private ComboBox comboBox2;
    }
}
