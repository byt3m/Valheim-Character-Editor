
namespace ValheimCharacterEditor
{
    partial class Form_Item
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Item));
            this.pbChevron = new System.Windows.Forms.PictureBox();
            this.button_Close = new System.Windows.Forms.Button();
            this.itemImage = new System.Windows.Forms.PictureBox();
            this.comboBox_Item = new System.Windows.Forms.ComboBox();
            this.lbl_Item = new System.Windows.Forms.Label();
            this.tb_Quantity = new System.Windows.Forms.TrackBar();
            this.lbl_Quantity = new System.Windows.Forms.Label();
            this.lbl_tb_Quantity = new System.Windows.Forms.Label();
            this.button_Apply = new System.Windows.Forms.Button();
            this.tb_Quality = new System.Windows.Forms.TrackBar();
            this.lbl_Quality = new System.Windows.Forms.Label();
            this.lbl_tb_Quality = new System.Windows.Forms.Label();
            this.lbl_tb_Variant = new System.Windows.Forms.Label();
            this.lbl_Variant = new System.Windows.Forms.Label();
            this.tb_Variant = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pbChevron)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Quantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Quality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Variant)).BeginInit();
            this.SuspendLayout();
            // 
            // pbChevron
            // 
            this.pbChevron.Image = ((System.Drawing.Image)(resources.GetObject("pbChevron.Image")));
            this.pbChevron.InitialImage = null;
            this.pbChevron.Location = new System.Drawing.Point(-5, 23);
            this.pbChevron.Margin = new System.Windows.Forms.Padding(0);
            this.pbChevron.Name = "pbChevron";
            this.pbChevron.Size = new System.Drawing.Size(25, 25);
            this.pbChevron.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbChevron.TabIndex = 0;
            this.pbChevron.TabStop = false;
            // 
            // button_Close
            // 
            this.button_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Close.ForeColor = System.Drawing.Color.White;
            this.button_Close.Location = new System.Drawing.Point(434, 187);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(80, 28);
            this.button_Close.TabIndex = 1;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // itemImage
            // 
            this.itemImage.Location = new System.Drawing.Point(35, 23);
            this.itemImage.Margin = new System.Windows.Forms.Padding(0);
            this.itemImage.Name = "itemImage";
            this.itemImage.Size = new System.Drawing.Size(64, 64);
            this.itemImage.TabIndex = 2;
            this.itemImage.TabStop = false;
            // 
            // comboBox_Item
            // 
            this.comboBox_Item.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox_Item.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox_Item.FormattingEnabled = true;
            this.comboBox_Item.Location = new System.Drawing.Point(147, 23);
            this.comboBox_Item.Name = "comboBox_Item";
            this.comboBox_Item.Size = new System.Drawing.Size(325, 21);
            this.comboBox_Item.TabIndex = 3;
            this.comboBox_Item.SelectedIndexChanged += new System.EventHandler(this.comboBox_Item_SelectedIndexChanged);
            // 
            // lbl_Item
            // 
            this.lbl_Item.AutoSize = true;
            this.lbl_Item.ForeColor = System.Drawing.Color.White;
            this.lbl_Item.Location = new System.Drawing.Point(114, 26);
            this.lbl_Item.Name = "lbl_Item";
            this.lbl_Item.Size = new System.Drawing.Size(27, 13);
            this.lbl_Item.TabIndex = 18;
            this.lbl_Item.Text = "Item";
            // 
            // tb_Quantity
            // 
            this.tb_Quantity.Location = new System.Drawing.Point(137, 51);
            this.tb_Quantity.Maximum = 50;
            this.tb_Quantity.Name = "tb_Quantity";
            this.tb_Quantity.Size = new System.Drawing.Size(335, 45);
            this.tb_Quantity.TabIndex = 19;
            this.tb_Quantity.Scroll += new System.EventHandler(this.tb_Quantity_scroll);
            this.tb_Quantity.ValueChanged += new System.EventHandler(this.tb_Quantity_scroll);
            // 
            // lbl_Quantity
            // 
            this.lbl_Quantity.AutoSize = true;
            this.lbl_Quantity.ForeColor = System.Drawing.Color.White;
            this.lbl_Quantity.Location = new System.Drawing.Point(478, 62);
            this.lbl_Quantity.Name = "lbl_Quantity";
            this.lbl_Quantity.Size = new System.Drawing.Size(13, 13);
            this.lbl_Quantity.TabIndex = 20;
            this.lbl_Quantity.Text = "1";
            // 
            // lbl_tb_Quantity
            // 
            this.lbl_tb_Quantity.AutoSize = true;
            this.lbl_tb_Quantity.ForeColor = System.Drawing.Color.White;
            this.lbl_tb_Quantity.Location = new System.Drawing.Point(106, 62);
            this.lbl_tb_Quantity.Name = "lbl_tb_Quantity";
            this.lbl_tb_Quantity.Size = new System.Drawing.Size(35, 13);
            this.lbl_tb_Quantity.TabIndex = 21;
            this.lbl_tb_Quantity.Text = "Stack";
            // 
            // button_Apply
            // 
            this.button_Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Apply.ForeColor = System.Drawing.Color.White;
            this.button_Apply.Location = new System.Drawing.Point(335, 187);
            this.button_Apply.Name = "button_Apply";
            this.button_Apply.Size = new System.Drawing.Size(80, 28);
            this.button_Apply.TabIndex = 22;
            this.button_Apply.Text = "Apply";
            this.button_Apply.UseVisualStyleBackColor = true;
            this.button_Apply.Click += new System.EventHandler(this.button_Apply_Click);
            // 
            // tb_Quality
            // 
            this.tb_Quality.Location = new System.Drawing.Point(137, 91);
            this.tb_Quality.Maximum = 5;
            this.tb_Quality.Name = "tb_Quality";
            this.tb_Quality.Size = new System.Drawing.Size(335, 45);
            this.tb_Quality.TabIndex = 23;
            this.tb_Quality.Value = 1;
            this.tb_Quality.Scroll += new System.EventHandler(this.tb_Quality_Scroll);
            this.tb_Quality.ValueChanged += new System.EventHandler(this.tb_Quality_Scroll);
            // 
            // lbl_Quality
            // 
            this.lbl_Quality.AutoSize = true;
            this.lbl_Quality.ForeColor = System.Drawing.Color.White;
            this.lbl_Quality.Location = new System.Drawing.Point(478, 102);
            this.lbl_Quality.Name = "lbl_Quality";
            this.lbl_Quality.Size = new System.Drawing.Size(13, 13);
            this.lbl_Quality.TabIndex = 24;
            this.lbl_Quality.Text = "1";
            // 
            // lbl_tb_Quality
            // 
            this.lbl_tb_Quality.AutoSize = true;
            this.lbl_tb_Quality.ForeColor = System.Drawing.Color.White;
            this.lbl_tb_Quality.Location = new System.Drawing.Point(106, 99);
            this.lbl_tb_Quality.Name = "lbl_tb_Quality";
            this.lbl_tb_Quality.Size = new System.Drawing.Size(39, 13);
            this.lbl_tb_Quality.TabIndex = 25;
            this.lbl_tb_Quality.Text = "Quality";
            // 
            // lbl_tb_Variant
            // 
            this.lbl_tb_Variant.AutoSize = true;
            this.lbl_tb_Variant.ForeColor = System.Drawing.Color.White;
            this.lbl_tb_Variant.Location = new System.Drawing.Point(106, 144);
            this.lbl_tb_Variant.Name = "lbl_tb_Variant";
            this.lbl_tb_Variant.Size = new System.Drawing.Size(40, 13);
            this.lbl_tb_Variant.TabIndex = 28;
            this.lbl_tb_Variant.Text = "Variant";
            this.lbl_tb_Variant.Visible = false;
            // 
            // lbl_Variant
            // 
            this.lbl_Variant.AutoSize = true;
            this.lbl_Variant.ForeColor = System.Drawing.Color.White;
            this.lbl_Variant.Location = new System.Drawing.Point(478, 147);
            this.lbl_Variant.Name = "lbl_Variant";
            this.lbl_Variant.Size = new System.Drawing.Size(13, 13);
            this.lbl_Variant.TabIndex = 27;
            this.lbl_Variant.Text = "1";
            // 
            // tb_Variant
            // 
            this.tb_Variant.Location = new System.Drawing.Point(137, 136);
            this.tb_Variant.Maximum = 5;
            this.tb_Variant.Name = "tb_Variant";
            this.tb_Variant.Size = new System.Drawing.Size(335, 45);
            this.tb_Variant.TabIndex = 26;
            this.tb_Variant.Value = 1;
            this.tb_Variant.Scroll += new System.EventHandler(this.tb_Variant_Scroll);
            this.tb_Variant.ValueChanged += new System.EventHandler(this.tb_Variant_Scroll);
            // 
            // Form_Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(526, 227);
            this.Controls.Add(this.lbl_tb_Variant);
            this.Controls.Add(this.lbl_Variant);
            this.Controls.Add(this.tb_Variant);
            this.Controls.Add(this.lbl_tb_Quality);
            this.Controls.Add(this.lbl_Quality);
            this.Controls.Add(this.tb_Quality);
            this.Controls.Add(this.button_Apply);
            this.Controls.Add(this.lbl_tb_Quantity);
            this.Controls.Add(this.lbl_Quantity);
            this.Controls.Add(this.tb_Quantity);
            this.Controls.Add(this.lbl_Item);
            this.Controls.Add(this.comboBox_Item);
            this.Controls.Add(this.itemImage);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.pbChevron);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Item";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form_Item";
            ((System.ComponentModel.ISupportInitialize)(this.pbChevron)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Quantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Quality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Variant)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbChevron;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.PictureBox itemImage;
        private System.Windows.Forms.ComboBox comboBox_Item;
        private System.Windows.Forms.Label lbl_Item;
        private System.Windows.Forms.TrackBar tb_Quantity;
        private System.Windows.Forms.Label lbl_Quantity;
        private System.Windows.Forms.Label lbl_tb_Quantity;
        private System.Windows.Forms.Button button_Apply;
        private System.Windows.Forms.TrackBar tb_Quality;
        private System.Windows.Forms.Label lbl_Quality;
        private System.Windows.Forms.Label lbl_tb_Quality;
        private System.Windows.Forms.Label lbl_tb_Variant;
        private System.Windows.Forms.Label lbl_Variant;
        private System.Windows.Forms.TrackBar tb_Variant;
    }
}