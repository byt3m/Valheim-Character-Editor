
namespace ValheimCharacterEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comboBox_Characters = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_ChangeName = new System.Windows.Forms.CheckBox();
            this.label_SelectChar = new System.Windows.Forms.Label();
            this.comboBox_HairColor = new System.Windows.Forms.ComboBox();
            this.label_HairColor = new System.Windows.Forms.Label();
            this.label_Name = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.comboBox_Beard = new System.Windows.Forms.ComboBox();
            this.label_Beard = new System.Windows.Forms.Label();
            this.comboBox_Hair = new System.Windows.Forms.ComboBox();
            this.label_Hair = new System.Windows.Forms.Label();
            this.button_Apply = new System.Windows.Forms.Button();
            this.folderBrowserDialog_FCHFilesDir = new System.Windows.Forms.FolderBrowserDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_Exit = new System.Windows.Forms.Button();
            this.label_Version = new System.Windows.Forms.Label();
            this.button_Minimize = new System.Windows.Forms.Button();
            this.button_RepairCharacter = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_Characters
            // 
            this.comboBox_Characters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_Characters.FormattingEnabled = true;
            this.comboBox_Characters.Location = new System.Drawing.Point(26, 41);
            this.comboBox_Characters.Name = "comboBox_Characters";
            this.comboBox_Characters.Size = new System.Drawing.Size(260, 21);
            this.comboBox_Characters.TabIndex = 0;
            this.comboBox_Characters.SelectedIndexChanged += new System.EventHandler(this.comboBox_Characters_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_ChangeName);
            this.groupBox1.Controls.Add(this.label_SelectChar);
            this.groupBox1.Controls.Add(this.comboBox_Characters);
            this.groupBox1.Controls.Add(this.comboBox_HairColor);
            this.groupBox1.Controls.Add(this.label_HairColor);
            this.groupBox1.Controls.Add(this.label_Name);
            this.groupBox1.Controls.Add(this.textBox_Name);
            this.groupBox1.Controls.Add(this.comboBox_Beard);
            this.groupBox1.Controls.Add(this.label_Beard);
            this.groupBox1.Controls.Add(this.comboBox_Hair);
            this.groupBox1.Controls.Add(this.label_Hair);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 195);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // checkBox_ChangeName
            // 
            this.checkBox_ChangeName.AutoSize = true;
            this.checkBox_ChangeName.Location = new System.Drawing.Point(26, 117);
            this.checkBox_ChangeName.Name = "checkBox_ChangeName";
            this.checkBox_ChangeName.Size = new System.Drawing.Size(102, 17);
            this.checkBox_ChangeName.TabIndex = 11;
            this.checkBox_ChangeName.Text = "Change name?";
            this.checkBox_ChangeName.UseVisualStyleBackColor = true;
            this.checkBox_ChangeName.CheckedChanged += new System.EventHandler(this.checkBox_ChangeName_CheckedChanged);
            // 
            // label_SelectChar
            // 
            this.label_SelectChar.AutoSize = true;
            this.label_SelectChar.Location = new System.Drawing.Point(23, 25);
            this.label_SelectChar.Name = "label_SelectChar";
            this.label_SelectChar.Size = new System.Drawing.Size(95, 13);
            this.label_SelectChar.TabIndex = 10;
            this.label_SelectChar.Text = "Select character*:";
            // 
            // comboBox_HairColor
            // 
            this.comboBox_HairColor.Enabled = false;
            this.comboBox_HairColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_HairColor.FormattingEnabled = true;
            this.comboBox_HairColor.Location = new System.Drawing.Point(165, 153);
            this.comboBox_HairColor.Name = "comboBox_HairColor";
            this.comboBox_HairColor.Size = new System.Drawing.Size(121, 21);
            this.comboBox_HairColor.TabIndex = 8;
            // 
            // label_HairColor
            // 
            this.label_HairColor.AutoSize = true;
            this.label_HairColor.Location = new System.Drawing.Point(162, 137);
            this.label_HairColor.Name = "label_HairColor";
            this.label_HairColor.Size = new System.Drawing.Size(62, 13);
            this.label_HairColor.TabIndex = 9;
            this.label_HairColor.Text = "Hair color*";
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(23, 73);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(36, 13);
            this.label_Name.TabIndex = 7;
            this.label_Name.Text = "Name";
            // 
            // textBox_Name
            // 
            this.textBox_Name.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_Name.Enabled = false;
            this.textBox_Name.ForeColor = System.Drawing.Color.Black;
            this.textBox_Name.Location = new System.Drawing.Point(26, 89);
            this.textBox_Name.MaxLength = 15;
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(121, 22);
            this.textBox_Name.TabIndex = 6;
            this.textBox_Name.TextChanged += new System.EventHandler(this.textBox_Name_TextChanged);
            // 
            // comboBox_Beard
            // 
            this.comboBox_Beard.Enabled = false;
            this.comboBox_Beard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_Beard.FormattingEnabled = true;
            this.comboBox_Beard.Location = new System.Drawing.Point(165, 90);
            this.comboBox_Beard.Name = "comboBox_Beard";
            this.comboBox_Beard.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Beard.TabIndex = 4;
            // 
            // label_Beard
            // 
            this.label_Beard.AutoSize = true;
            this.label_Beard.Location = new System.Drawing.Point(162, 73);
            this.label_Beard.Name = "label_Beard";
            this.label_Beard.Size = new System.Drawing.Size(41, 13);
            this.label_Beard.TabIndex = 5;
            this.label_Beard.Text = "Beard*";
            // 
            // comboBox_Hair
            // 
            this.comboBox_Hair.Enabled = false;
            this.comboBox_Hair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_Hair.FormattingEnabled = true;
            this.comboBox_Hair.Location = new System.Drawing.Point(26, 153);
            this.comboBox_Hair.Name = "comboBox_Hair";
            this.comboBox_Hair.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Hair.TabIndex = 2;
            // 
            // label_Hair
            // 
            this.label_Hair.AutoSize = true;
            this.label_Hair.Location = new System.Drawing.Point(23, 137);
            this.label_Hair.Name = "label_Hair";
            this.label_Hair.Size = new System.Drawing.Size(33, 13);
            this.label_Hair.TabIndex = 3;
            this.label_Hair.Text = "Hair*";
            // 
            // button_Apply
            // 
            this.button_Apply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_Apply.Enabled = false;
            this.button_Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Apply.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Apply.ForeColor = System.Drawing.Color.White;
            this.button_Apply.Location = new System.Drawing.Point(246, 362);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(80, 28);
            this.button_Apply.TabIndex = 4;
            this.button_Apply.Text = "Apply";
            this.button_Apply.UseVisualStyleBackColor = false;
            this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 230);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(314, 125);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // button_Exit
            // 
            this.button_Exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_Exit.FlatAppearance.BorderSize = 0;
            this.button_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Exit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Exit.ForeColor = System.Drawing.Color.White;
            this.button_Exit.Location = new System.Drawing.Point(318, 0);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(19, 23);
            this.button_Exit.TabIndex = 6;
            this.button_Exit.Text = "x";
            this.button_Exit.UseVisualStyleBackColor = false;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // label_Version
            // 
            this.label_Version.AutoSize = true;
            this.label_Version.Location = new System.Drawing.Point(9, 5);
            this.label_Version.Name = "label_Version";
            this.label_Version.Size = new System.Drawing.Size(156, 13);
            this.label_Version.TabIndex = 7;
            this.label_Version.Text = "Valheim Character Editor v1.4";
            // 
            // button_Minimize
            // 
            this.button_Minimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_Minimize.FlatAppearance.BorderSize = 0;
            this.button_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Minimize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Minimize.ForeColor = System.Drawing.Color.White;
            this.button_Minimize.Location = new System.Drawing.Point(293, 0);
            this.button_Minimize.Name = "button_Minimize";
            this.button_Minimize.Size = new System.Drawing.Size(19, 23);
            this.button_Minimize.TabIndex = 8;
            this.button_Minimize.Text = "-";
            this.button_Minimize.UseVisualStyleBackColor = false;
            this.button_Minimize.Click += new System.EventHandler(this.button_Minimize_Click);
            // 
            // button_RepairCharacter
            // 
            this.button_RepairCharacter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_RepairCharacter.Enabled = false;
            this.button_RepairCharacter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_RepairCharacter.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_RepairCharacter.ForeColor = System.Drawing.Color.White;
            this.button_RepairCharacter.Location = new System.Drawing.Point(12, 361);
            this.button_RepairCharacter.Name = "button_RepairCharacter";
            this.button_RepairCharacter.Size = new System.Drawing.Size(110, 28);
            this.button_RepairCharacter.TabIndex = 9;
            this.button_RepairCharacter.Text = "Repair character";
            this.button_RepairCharacter.UseVisualStyleBackColor = false;
            this.button_RepairCharacter.Click += new System.EventHandler(this.button_RepairCharacter_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(338, 405);
            this.Controls.Add(this.button_RepairCharacter);
            this.Controls.Add(this.button_Minimize);
            this.Controls.Add(this.label_Version);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_Apply);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Valheim Character Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_move);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Characters;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox_HairColor;
        private System.Windows.Forms.Label label_HairColor;
        private System.Windows.Forms.ComboBox comboBox_Beard;
        private System.Windows.Forms.Label label_Beard;
        private System.Windows.Forms.ComboBox comboBox_Hair;
        private System.Windows.Forms.Label label_Hair;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.Label label_SelectChar;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_FCHFilesDir;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Label label_Version;
        private System.Windows.Forms.Button button_Minimize;
        private System.Windows.Forms.CheckBox checkBox_ChangeName;
        private System.Windows.Forms.Button button_RepairCharacter;
    }
}

