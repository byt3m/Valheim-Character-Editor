namespace ValheimCharacterEditor
{
    partial class FormInventory
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label_Version = new System.Windows.Forms.Label();
            this.button_Exit = new System.Windows.Forms.Button();
            this.dataGridView_Inventory = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip_Inventory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_Update = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Inventory)).BeginInit();
            this.contextMenuStrip_Inventory.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Version
            // 
            this.label_Version.AutoSize = true;
            this.label_Version.Location = new System.Drawing.Point(3, 0);
            this.label_Version.Name = "label_Version";
            this.label_Version.Size = new System.Drawing.Size(156, 13);
            this.label_Version.TabIndex = 8;
            this.label_Version.Text = "Valheim Character Editor v1.4";
            // 
            // button_Exit
            // 
            this.button_Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_Exit.FlatAppearance.BorderSize = 0;
            this.button_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Exit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Exit.ForeColor = System.Drawing.Color.White;
            this.button_Exit.Location = new System.Drawing.Point(956, 0);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(19, 23);
            this.button_Exit.TabIndex = 10;
            this.button_Exit.Text = "x";
            this.button_Exit.UseVisualStyleBackColor = false;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // dataGridView_Inventory
            // 
            this.dataGridView_Inventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Inventory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridView_Inventory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridView_Inventory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Inventory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Inventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Inventory.ContextMenuStrip = this.contextMenuStrip_Inventory;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Inventory.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_Inventory.EnableHeadersVisualStyles = false;
            this.dataGridView_Inventory.GridColor = System.Drawing.Color.White;
            this.dataGridView_Inventory.Location = new System.Drawing.Point(6, 36);
            this.dataGridView_Inventory.Name = "dataGridView_Inventory";
            this.dataGridView_Inventory.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Inventory.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_Inventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Inventory.Size = new System.Drawing.Size(965, 461);
            this.dataGridView_Inventory.TabIndex = 11;
            // 
            // contextMenuStrip_Inventory
            // 
            this.contextMenuStrip_Inventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.contextMenuStrip_Inventory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cloneToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip_Inventory.Name = "contextMenuStrip_Inventory";
            this.contextMenuStrip_Inventory.Size = new System.Drawing.Size(108, 48);
            // 
            // cloneToolStripMenuItem
            // 
            this.cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
            this.cloneToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.cloneToolStripMenuItem.Text = "Clone";
            this.cloneToolStripMenuItem.Click += new System.EventHandler(this.cloneToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // button_Update
            // 
            this.button_Update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_Update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Update.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Update.ForeColor = System.Drawing.Color.White;
            this.button_Update.Location = new System.Drawing.Point(891, 503);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(80, 28);
            this.button_Update.TabIndex = 12;
            this.button_Update.Text = "Update";
            this.button_Update.UseVisualStyleBackColor = false;
            this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // FormInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(977, 543);
            this.Controls.Add(this.button_Update);
            this.Controls.Add(this.dataGridView_Inventory);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.label_Version);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormInventory";
            this.Text = "FormInventory";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Inventory)).EndInit();
            this.contextMenuStrip_Inventory.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Version;
        private System.Windows.Forms.Button button_Exit;
        private System.Windows.Forms.DataGridView dataGridView_Inventory;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Inventory;
        private System.Windows.Forms.ToolStripMenuItem cloneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}