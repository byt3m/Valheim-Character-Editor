namespace ValheimCharacterEditor
{
    partial class Form_Skills
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
            this.label_Character = new System.Windows.Forms.Label();
            this.button_apply = new System.Windows.Forms.Button();
            this.button_set_100 = new System.Windows.Forms.Button();
            this.button_set_50 = new System.Windows.Forms.Button();
            this.button_set_1 = new System.Windows.Forms.Button();
            this.groupBox_fast_set = new System.Windows.Forms.GroupBox();
            this.button_exit = new System.Windows.Forms.Button();
            this.button_Minimize = new System.Windows.Forms.Button();
            this.groupBox_fast_set.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Character
            // 
            this.label_Character.AutoSize = true;
            this.label_Character.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Character.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_Character.Location = new System.Drawing.Point(2, 9);
            this.label_Character.Name = "label_Character";
            this.label_Character.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_Character.Size = new System.Drawing.Size(98, 25);
            this.label_Character.TabIndex = 0;
            this.label_Character.Text = "Character";
            // 
            // button_apply
            // 
            this.button_apply.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_apply.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_apply.Location = new System.Drawing.Point(922, 774);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(100, 25);
            this.button_apply.TabIndex = 4;
            this.button_apply.Text = "Apply";
            this.button_apply.UseVisualStyleBackColor = true;
            this.button_apply.Click += new System.EventHandler(this.button_apply_Click);
            // 
            // button_set_100
            // 
            this.button_set_100.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_set_100.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_set_100.Location = new System.Drawing.Point(218, 19);
            this.button_set_100.Name = "button_set_100";
            this.button_set_100.Size = new System.Drawing.Size(100, 25);
            this.button_set_100.TabIndex = 5;
            this.button_set_100.Text = "100";
            this.button_set_100.UseVisualStyleBackColor = true;
            this.button_set_100.Click += new System.EventHandler(this.button_set_100_Click);
            // 
            // button_set_50
            // 
            this.button_set_50.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_set_50.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_set_50.Location = new System.Drawing.Point(112, 19);
            this.button_set_50.Name = "button_set_50";
            this.button_set_50.Size = new System.Drawing.Size(100, 25);
            this.button_set_50.TabIndex = 6;
            this.button_set_50.Text = "50";
            this.button_set_50.UseVisualStyleBackColor = true;
            this.button_set_50.Click += new System.EventHandler(this.button_set_50_Click);
            // 
            // button_set_1
            // 
            this.button_set_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_set_1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_set_1.Location = new System.Drawing.Point(6, 19);
            this.button_set_1.Name = "button_set_1";
            this.button_set_1.Size = new System.Drawing.Size(100, 25);
            this.button_set_1.TabIndex = 7;
            this.button_set_1.Text = "1";
            this.button_set_1.UseVisualStyleBackColor = true;
            this.button_set_1.Click += new System.EventHandler(this.button_set_1_Click);
            // 
            // groupBox_fast_set
            // 
            this.groupBox_fast_set.Controls.Add(this.button_set_100);
            this.groupBox_fast_set.Controls.Add(this.button_set_50);
            this.groupBox_fast_set.Controls.Add(this.button_set_1);
            this.groupBox_fast_set.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox_fast_set.Location = new System.Drawing.Point(7, 746);
            this.groupBox_fast_set.Name = "groupBox_fast_set";
            this.groupBox_fast_set.Size = new System.Drawing.Size(327, 53);
            this.groupBox_fast_set.TabIndex = 8;
            this.groupBox_fast_set.TabStop = false;
            this.groupBox_fast_set.Text = "Set all";
            // 
            // button_exit
            // 
            this.button_exit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_exit.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_exit.Location = new System.Drawing.Point(1002, 9);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(20, 25);
            this.button_exit.TabIndex = 9;
            this.button_exit.Text = "x";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // button_Minimize
            // 
            this.button_Minimize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Minimize.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_Minimize.Location = new System.Drawing.Point(976, 9);
            this.button_Minimize.Name = "button_Minimize";
            this.button_Minimize.Size = new System.Drawing.Size(20, 25);
            this.button_Minimize.TabIndex = 10;
            this.button_Minimize.Text = "-";
            this.button_Minimize.UseVisualStyleBackColor = true;
            this.button_Minimize.Click += new System.EventHandler(this.button_Minimize_Click);
            // 
            // Form_Skills
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1034, 811);
            this.Controls.Add(this.button_Minimize);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.button_apply);
            this.Controls.Add(this.label_Character);
            this.Controls.Add(this.groupBox_fast_set);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_Skills";
            this.Text = "Skills";
            this.Load += new System.EventHandler(this.Skills_Load);
            this.groupBox_fast_set.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_runskill;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_Character;
        private System.Windows.Forms.Button button_apply;
        private System.Windows.Forms.Button button_set_100;
        private System.Windows.Forms.Button button_set_50;
        private System.Windows.Forms.Button button_set_1;
        private System.Windows.Forms.GroupBox groupBox_fast_set;
        private System.Windows.Forms.Button button_exit;
        private System.Windows.Forms.Button button_Minimize;
    }
}