namespace ValheimCharacterEditor.Controls
{
    partial class Skill_control
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBar_skill = new System.Windows.Forms.TrackBar();
            this.label_skill_level = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_skill)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBar_skill);
            this.groupBox1.Controls.Add(this.label_skill_level);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(494, 69);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // trackBar_skill
            // 
            this.trackBar_skill.Location = new System.Drawing.Point(6, 20);
            this.trackBar_skill.Maximum = 100;
            this.trackBar_skill.Minimum = 1;
            this.trackBar_skill.Name = "trackBar_skill";
            this.trackBar_skill.Size = new System.Drawing.Size(437, 45);
            this.trackBar_skill.TabIndex = 1;
            this.trackBar_skill.Value = 1;
            this.trackBar_skill.Scroll += new System.EventHandler(this.trackBar_skill_Scroll);
            // 
            // label_skill_level
            // 
            this.label_skill_level.AutoSize = true;
            this.label_skill_level.Location = new System.Drawing.Point(449, 29);
            this.label_skill_level.Name = "label_skill_level";
            this.label_skill_level.Size = new System.Drawing.Size(29, 17);
            this.label_skill_level.TabIndex = 0;
            this.label_skill_level.Text = "100";
            // 
            // Skill_control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.groupBox1);
            this.Name = "Skill_control";
            this.Size = new System.Drawing.Size(500, 75);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_skill)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar trackBar_skill;
        private System.Windows.Forms.Label label_skill_level;
    }
}
