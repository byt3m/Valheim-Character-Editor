
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
            this.groupBox_Customization = new System.Windows.Forms.GroupBox();
            this.button_HairColor = new System.Windows.Forms.Button();
            this.textBox_HairColor = new System.Windows.Forms.TextBox();
            this.button_SkinTone = new System.Windows.Forms.Button();
            this.textBox_SkinTone = new System.Windows.Forms.TextBox();
            this.comboBox_Gender = new System.Windows.Forms.ComboBox();
            this.label_Gender = new System.Windows.Forms.Label();
            this.label_HairColor = new System.Windows.Forms.Label();
            this.label_SkinTone = new System.Windows.Forms.Label();
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
            this.colorDialog_SkinTone = new System.Windows.Forms.ColorDialog();
            this.comboBox_Characters = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.colorDialog_HairColor = new System.Windows.Forms.ColorDialog();
            this.button_Skills = new System.Windows.Forms.Button();
            this.button_Inventory = new System.Windows.Forms.Button();
            this.groupBox_Customization.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Customization
            // 
            this.groupBox_Customization.Controls.Add(this.button_HairColor);
            this.groupBox_Customization.Controls.Add(this.textBox_HairColor);
            this.groupBox_Customization.Controls.Add(this.button_SkinTone);
            this.groupBox_Customization.Controls.Add(this.textBox_SkinTone);
            this.groupBox_Customization.Controls.Add(this.comboBox_Gender);
            this.groupBox_Customization.Controls.Add(this.label_Gender);
            this.groupBox_Customization.Controls.Add(this.label_HairColor);
            this.groupBox_Customization.Controls.Add(this.label_SkinTone);
            this.groupBox_Customization.Controls.Add(this.label_Name);
            this.groupBox_Customization.Controls.Add(this.textBox_Name);
            this.groupBox_Customization.Controls.Add(this.comboBox_Beard);
            this.groupBox_Customization.Controls.Add(this.label_Beard);
            this.groupBox_Customization.Controls.Add(this.comboBox_Hair);
            this.groupBox_Customization.Controls.Add(this.label_Hair);
            this.groupBox_Customization.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox_Customization.ForeColor = System.Drawing.Color.White;
            this.groupBox_Customization.Location = new System.Drawing.Point(12, 130);
            this.groupBox_Customization.Name = "groupBox_Customization";
            this.groupBox_Customization.Size = new System.Drawing.Size(564, 170);
            this.groupBox_Customization.TabIndex = 2;
            this.groupBox_Customization.TabStop = false;
            this.groupBox_Customization.Text = "Customization";
            // 
            // button_HairColor
            // 
            this.button_HairColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_HairColor.Enabled = false;
            this.button_HairColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_HairColor.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_HairColor.ForeColor = System.Drawing.Color.White;
            this.button_HairColor.Location = new System.Drawing.Point(493, 103);
            this.button_HairColor.Name = "button_HairColor";
            this.button_HairColor.Size = new System.Drawing.Size(50, 32);
            this.button_HairColor.TabIndex = 15;
            this.button_HairColor.Text = "Select";
            this.button_HairColor.UseVisualStyleBackColor = false;
            this.button_HairColor.Click += new System.EventHandler(this.button_HairColor_Click);
            // 
            // textBox_HairColor
            // 
            this.textBox_HairColor.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_HairColor.Enabled = false;
            this.textBox_HairColor.ForeColor = System.Drawing.Color.White;
            this.textBox_HairColor.Location = new System.Drawing.Point(385, 103);
            this.textBox_HairColor.MaxLength = 15;
            this.textBox_HairColor.Multiline = true;
            this.textBox_HairColor.Name = "textBox_HairColor";
            this.textBox_HairColor.ReadOnly = true;
            this.textBox_HairColor.Size = new System.Drawing.Size(102, 41);
            this.textBox_HairColor.TabIndex = 16;
            // 
            // button_SkinTone
            // 
            this.button_SkinTone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_SkinTone.Enabled = false;
            this.button_SkinTone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_SkinTone.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_SkinTone.ForeColor = System.Drawing.Color.White;
            this.button_SkinTone.Location = new System.Drawing.Point(311, 103);
            this.button_SkinTone.Name = "button_SkinTone";
            this.button_SkinTone.Size = new System.Drawing.Size(50, 32);
            this.button_SkinTone.TabIndex = 11;
            this.button_SkinTone.Text = "Select";
            this.button_SkinTone.UseVisualStyleBackColor = false;
            this.button_SkinTone.Click += new System.EventHandler(this.button_SkinTone_Click);
            // 
            // textBox_SkinTone
            // 
            this.textBox_SkinTone.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_SkinTone.Enabled = false;
            this.textBox_SkinTone.ForeColor = System.Drawing.Color.White;
            this.textBox_SkinTone.Location = new System.Drawing.Point(203, 103);
            this.textBox_SkinTone.MaxLength = 15;
            this.textBox_SkinTone.Multiline = true;
            this.textBox_SkinTone.Name = "textBox_SkinTone";
            this.textBox_SkinTone.ReadOnly = true;
            this.textBox_SkinTone.Size = new System.Drawing.Size(102, 41);
            this.textBox_SkinTone.TabIndex = 14;
            // 
            // comboBox_Gender
            // 
            this.comboBox_Gender.Enabled = false;
            this.comboBox_Gender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_Gender.FormattingEnabled = true;
            this.comboBox_Gender.Location = new System.Drawing.Point(18, 103);
            this.comboBox_Gender.Name = "comboBox_Gender";
            this.comboBox_Gender.Size = new System.Drawing.Size(158, 21);
            this.comboBox_Gender.TabIndex = 13;
            // 
            // label_Gender
            // 
            this.label_Gender.AutoSize = true;
            this.label_Gender.Location = new System.Drawing.Point(15, 84);
            this.label_Gender.Name = "label_Gender";
            this.label_Gender.Size = new System.Drawing.Size(45, 13);
            this.label_Gender.TabIndex = 12;
            this.label_Gender.Text = "Gender";
            // 
            // label_HairColor
            // 
            this.label_HairColor.AutoSize = true;
            this.label_HairColor.Location = new System.Drawing.Point(382, 84);
            this.label_HairColor.Name = "label_HairColor";
            this.label_HairColor.Size = new System.Drawing.Size(57, 13);
            this.label_HairColor.TabIndex = 10;
            this.label_HairColor.Text = "Hair color";
            // 
            // label_SkinTone
            // 
            this.label_SkinTone.AutoSize = true;
            this.label_SkinTone.Location = new System.Drawing.Point(200, 84);
            this.label_SkinTone.Name = "label_SkinTone";
            this.label_SkinTone.Size = new System.Drawing.Size(56, 13);
            this.label_SkinTone.TabIndex = 9;
            this.label_SkinTone.Text = "Skin tone";
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(15, 26);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(36, 13);
            this.label_Name.TabIndex = 7;
            this.label_Name.Text = "Name";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Enabled = false;
            this.textBox_Name.ForeColor = System.Drawing.Color.Black;
            this.textBox_Name.Location = new System.Drawing.Point(18, 43);
            this.textBox_Name.MaxLength = 15;
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(158, 22);
            this.textBox_Name.TabIndex = 6;
            this.textBox_Name.TextChanged += new System.EventHandler(this.textBox_Name_TextChanged);
            // 
            // comboBox_Beard
            // 
            this.comboBox_Beard.Enabled = false;
            this.comboBox_Beard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_Beard.FormattingEnabled = true;
            this.comboBox_Beard.Location = new System.Drawing.Point(203, 43);
            this.comboBox_Beard.Name = "comboBox_Beard";
            this.comboBox_Beard.Size = new System.Drawing.Size(158, 21);
            this.comboBox_Beard.TabIndex = 4;
            // 
            // label_Beard
            // 
            this.label_Beard.AutoSize = true;
            this.label_Beard.Location = new System.Drawing.Point(200, 26);
            this.label_Beard.Name = "label_Beard";
            this.label_Beard.Size = new System.Drawing.Size(36, 13);
            this.label_Beard.TabIndex = 5;
            this.label_Beard.Text = "Beard";
            // 
            // comboBox_Hair
            // 
            this.comboBox_Hair.Enabled = false;
            this.comboBox_Hair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_Hair.FormattingEnabled = true;
            this.comboBox_Hair.Location = new System.Drawing.Point(385, 43);
            this.comboBox_Hair.Name = "comboBox_Hair";
            this.comboBox_Hair.Size = new System.Drawing.Size(158, 21);
            this.comboBox_Hair.TabIndex = 2;
            // 
            // label_Hair
            // 
            this.label_Hair.AutoSize = true;
            this.label_Hair.Location = new System.Drawing.Point(382, 27);
            this.label_Hair.Name = "label_Hair";
            this.label_Hair.Size = new System.Drawing.Size(28, 13);
            this.label_Hair.TabIndex = 3;
            this.label_Hair.Text = "Hair";
            // 
            // button_Apply
            // 
            this.button_Apply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_Apply.Enabled = false;
            this.button_Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Apply.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Apply.ForeColor = System.Drawing.Color.White;
            this.button_Apply.Location = new System.Drawing.Point(496, 306);
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
            this.pictureBox1.Location = new System.Drawing.Point(331, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(245, 100);
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
            this.button_Exit.Location = new System.Drawing.Point(567, -5);
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
            this.label_Version.Location = new System.Drawing.Point(3, 0);
            this.label_Version.Name = "label_Version";
            this.label_Version.Size = new System.Drawing.Size(156, 13);
            this.label_Version.TabIndex = 7;
            this.label_Version.Text = "Valheim Character Editor v1.6";
            // 
            // button_Minimize
            // 
            this.button_Minimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_Minimize.FlatAppearance.BorderSize = 0;
            this.button_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Minimize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Minimize.ForeColor = System.Drawing.Color.White;
            this.button_Minimize.Location = new System.Drawing.Point(552, -5);
            this.button_Minimize.Name = "button_Minimize";
            this.button_Minimize.Size = new System.Drawing.Size(19, 23);
            this.button_Minimize.TabIndex = 8;
            this.button_Minimize.Text = "-";
            this.button_Minimize.UseVisualStyleBackColor = false;
            this.button_Minimize.Click += new System.EventHandler(this.button_Minimize_Click);
            // 
            // colorDialog_SkinTone
            // 
            this.colorDialog_SkinTone.FullOpen = true;
            // 
            // comboBox_Characters
            // 
            this.comboBox_Characters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_Characters.FormattingEnabled = true;
            this.comboBox_Characters.Location = new System.Drawing.Point(18, 49);
            this.comboBox_Characters.Name = "comboBox_Characters";
            this.comboBox_Characters.Size = new System.Drawing.Size(260, 21);
            this.comboBox_Characters.TabIndex = 0;
            this.comboBox_Characters.SelectedIndexChanged += new System.EventHandler(this.comboBox_Characters_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Select character";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBox_Characters);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(313, 108);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // colorDialog_HairColor
            // 
            this.colorDialog_HairColor.FullOpen = true;
            // 
            // button_Skills
            // 
            this.button_Skills.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_Skills.Enabled = false;
            this.button_Skills.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Skills.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Skills.ForeColor = System.Drawing.Color.White;
            this.button_Skills.Location = new System.Drawing.Point(12, 306);
            this.button_Skills.Name = "button_Skills";
            this.button_Skills.Size = new System.Drawing.Size(80, 28);
            this.button_Skills.TabIndex = 11;
            this.button_Skills.Text = "Skills";
            this.button_Skills.UseVisualStyleBackColor = false;
            this.button_Skills.Click += new System.EventHandler(this.button_Skills_Click);
            // 
            // button_Inventory
            // 
            this.button_Inventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_Inventory.Enabled = false;
            this.button_Inventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Inventory.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Inventory.ForeColor = System.Drawing.Color.White;
            this.button_Inventory.Location = new System.Drawing.Point(98, 306);
            this.button_Inventory.Name = "button_Inventory";
            this.button_Inventory.Size = new System.Drawing.Size(80, 28);
            this.button_Inventory.TabIndex = 12;
            this.button_Inventory.Text = "Inventory";
            this.button_Inventory.UseVisualStyleBackColor = false;
            this.button_Inventory.Click += new System.EventHandler(this.button_Inventory_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(583, 346);
            this.Controls.Add(this.button_Inventory);
            this.Controls.Add(this.button_Skills);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_Minimize);
            this.Controls.Add(this.label_Version);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_Apply);
            this.Controls.Add(this.groupBox_Customization);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Valheim Character Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_move);
            this.groupBox_Customization.ResumeLayout(false);
            this.groupBox_Customization.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox_Customization;
        private System.Windows.Forms.Label label_SkinTone;
        private System.Windows.Forms.ComboBox comboBox_Beard;
        private System.Windows.Forms.Label label_Beard;
        private System.Windows.Forms.ComboBox comboBox_Hair;
        private System.Windows.Forms.Label label_Hair;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog_FCHFilesDir;
        private System.Windows.Forms.Label label_Name;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.Label label_Version;
        private System.Windows.Forms.Button button_Minimize;
        private System.Windows.Forms.ColorDialog colorDialog_SkinTone;
        private System.Windows.Forms.Label label_HairColor;
        private System.Windows.Forms.ComboBox comboBox_Gender;
        private System.Windows.Forms.Label label_Gender;
        private System.Windows.Forms.Button button_HairColor;
        private System.Windows.Forms.TextBox textBox_HairColor;
        private System.Windows.Forms.Button button_SkinTone;
        private System.Windows.Forms.TextBox textBox_SkinTone;
        private System.Windows.Forms.ComboBox comboBox_Characters;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColorDialog colorDialog_HairColor;
        private System.Windows.Forms.Button button_Skills;
        private System.Windows.Forms.Button button_Inventory;
    }
}

