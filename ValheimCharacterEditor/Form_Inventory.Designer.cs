
namespace ValheimCharacterEditor
{
    partial class Form_Inventory
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
            this.button_Minimize = new System.Windows.Forms.Button();
            this.button_exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_Character
            // 
            this.label_Character.AutoSize = true;
            this.label_Character.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Character.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label_Character.Location = new System.Drawing.Point(12, 9);
            this.label_Character.Name = "label_Character";
            this.label_Character.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_Character.Size = new System.Drawing.Size(77, 21);
            this.label_Character.TabIndex = 1;
            this.label_Character.Text = "Character";
            // 
            // button_Minimize
            // 
            this.button_Minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Minimize.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Minimize.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_Minimize.Location = new System.Drawing.Point(592, 9);
            this.button_Minimize.Name = "button_Minimize";
            this.button_Minimize.Size = new System.Drawing.Size(20, 25);
            this.button_Minimize.TabIndex = 12;
            this.button_Minimize.Text = "-";
            this.button_Minimize.UseVisualStyleBackColor = true;
            this.button_Minimize.Click += new System.EventHandler(this.button_Minimize_Click);
            // 
            // button_exit
            // 
            this.button_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_exit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_exit.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button_exit.Location = new System.Drawing.Point(618, 9);
            this.button_exit.Name = "button_exit";
            this.button_exit.Size = new System.Drawing.Size(20, 25);
            this.button_exit.TabIndex = 11;
            this.button_exit.Text = "x";
            this.button_exit.UseVisualStyleBackColor = true;
            this.button_exit.Click += new System.EventHandler(this.button_exit_Click);
            // 
            // Form_Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(650, 350);
            this.Controls.Add(this.button_Minimize);
            this.Controls.Add(this.button_exit);
            this.Controls.Add(this.label_Character);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_Inventory";
            this.Text = "Form_Inventory";
            this.Load += new System.EventHandler(this.Form_Inventory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Character;
        private System.Windows.Forms.Button button_Minimize;
        private System.Windows.Forms.Button button_exit;
    }
}