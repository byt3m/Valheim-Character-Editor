using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValheimCharacterEditor
{
    public partial class Form_Item : Form
    {
        ValheimEngine.Character.Item _itemData;
        public event EventHandler<ValheimEngine.Character.Item> ApplyItemChange;
        bool firstLoad = true;
        public Form_Item(ValheimEngine.Character.Item ItemData)
        {
            InitializeComponent();

            _itemData = ItemData;
            DataTable cmbItems = new DataTable();
            cmbItems.Columns.Add("displayname");
            cmbItems.Columns.Add("value");
            foreach(var item in ValheimEngine.ItemProperties.Keys)
            {
                cmbItems.Rows.Add(new Object[]{ValheimEngine.ItemProperties[item].DisplayName, item});
            }
            comboBox_Item.DataSource = cmbItems;
            comboBox_Item.DisplayMember = "displayname";
            comboBox_Item.ValueMember = "value";
            comboBox_Item.SelectedValue = _itemData.Name;

            //Application.DoEvents();

            tb_Quantity.Value = _itemData.Stack;
            tb_Variant.Value = _itemData.Variant;
            tb_Quality.Value = _itemData.Quality;
            firstLoad = false;
            
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox_Item_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateImage();
            if (!(comboBox_Item.SelectedValue is null))
            {
                if (!firstLoad)
                {
                    _itemData.Name = comboBox_Item.SelectedValue.ToString();
                    if(ValheimEngine.ItemProperties[_itemData.Name].Craftable)
                    {
                        _itemData.CrafterId = Customization.SelectedCharacter.Data.Id;
                        _itemData.CrafterName = Customization.SelectedCharacter.Data.Name;
                    }
                    else
                    {
                        _itemData.CrafterId = 0;
                        _itemData.CrafterName = "";
                    }
                    _itemData.Durability = Util.GetMaxDurability(_itemData.Name, tb_Quality.Value);
                    _itemData.Equipped = false;

                }
                if (ValheimEngine.ItemProperties.ContainsKey(comboBox_Item.SelectedValue.ToString()))
                {
                    
                    tb_Quality.Maximum = ValheimEngine.ItemProperties[comboBox_Item.SelectedValue.ToString()].MaxQuality;
                    tb_Quantity.Maximum = ValheimEngine.ItemProperties[comboBox_Item.SelectedValue.ToString()].MaxStackSize;
                    if (ValheimEngine.ItemProperties[comboBox_Item.SelectedValue.ToString()].MaxVariants > 1)
                    {
                        lbl_tb_Variant.Visible = true;
                        tb_Variant.Visible = true;
                        lbl_Variant.Visible = true;
                        tb_Variant.Value = 0;
                        tb_Variant.Maximum = ValheimEngine.ItemProperties[comboBox_Item.SelectedValue.ToString()].MaxVariants-1;
                    }
                    else
                    {
                        lbl_tb_Variant.Visible = false;
                        tb_Variant.Visible = false;
                        lbl_Variant.Visible = false;
                        tb_Variant.Value = 0;
                        tb_Variant.Maximum = 0;
                    }

                    if(ValheimEngine.ItemProperties[comboBox_Item.SelectedValue.ToString()].MaxStackSize>1)
                    {
                        lbl_tb_Quantity.Visible = true;
                        tb_Quantity.Visible = true;
                        lbl_Quantity.Visible = true;
                        tb_Quantity.Maximum = ValheimEngine.ItemProperties[comboBox_Item.SelectedValue.ToString()].MaxStackSize;                        
                    }
                    else
                    {
                        lbl_tb_Quantity.Visible = false;
                        tb_Quantity.Visible = false;
                        lbl_Quantity.Visible = false;
                        tb_Quantity.Maximum = ValheimEngine.ItemProperties[comboBox_Item.SelectedValue.ToString()].MaxStackSize;
                    }
                    if (ValheimEngine.ItemProperties[comboBox_Item.SelectedValue.ToString()].MaxQuality > 1)
                    {
                        lbl_tb_Quality.Visible = true;
                        tb_Quality.Visible = true;
                        lbl_Quality.Visible = true;
                        tb_Quality.Maximum = ValheimEngine.ItemProperties[comboBox_Item.SelectedValue.ToString()].MaxQuality;
                    }
                    else
                    {
                        lbl_tb_Quality.Visible = false;
                        tb_Quality.Visible = false;
                        lbl_Quality.Visible = false;
                        tb_Quality.Maximum = ValheimEngine.ItemProperties[comboBox_Item.SelectedValue.ToString()].MaxQuality;
                    }
                }
                else
                {
                    lbl_Quantity.Text = "0";
                    tb_Quantity.Minimum = 0;
                    tb_Quantity.Value = 0;
                    tb_Quantity.Maximum = 0;
                    tb_Quality.Value = 1;
                    tb_Quality.Maximum = 1;
                }
            }
        }

        private void tb_Quantity_scroll(object sender, EventArgs e)
        {
            lbl_Quantity.Text = tb_Quantity.Value.ToString();
            _itemData.Stack = tb_Quantity.Value;
        }

        private void tb_Quality_Scroll(object sender, EventArgs e)
        {
            lbl_Quality.Text = tb_Quality.Value.ToString();
            _itemData.Quality = tb_Quality.Value;
            _itemData.Durability = Util.GetMaxDurability(_itemData.Name, tb_Quality.Value);
        }
        private void tb_Variant_Scroll(object sender, EventArgs e)
        {
            lbl_Variant.Text = tb_Variant.Value.ToString();
            _itemData.Variant = tb_Variant.Value;
            UpdateImage();
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            ApplyItemChange.Invoke(this, _itemData);
            this.Close();
        }

        private void UpdateImage()
        {
            if (!(comboBox_Item.SelectedValue is null))
            {
                if (ValheimEngine.ItemProperties.ContainsKey(comboBox_Item.SelectedValue.ToString()))
                {
                    ValheimEngine.GameObjectItemProperties gameobject = ValheimEngine.ItemProperties[comboBox_Item.SelectedValue.ToString()];
                    if (gameobject.MaxVariants > 1 && _itemData.Variant > 0)
                    {
                        try
                        {
                            itemImage.Image = System.Drawing.Bitmap.FromFile(String.Concat("Images", System.IO.Path.DirectorySeparatorChar, comboBox_Item.SelectedValue.ToString(), _itemData.Variant, ".png"));
                        }
                        catch (System.IO.FileNotFoundException exFNF)
                        {
                            itemImage.Image = System.Drawing.Bitmap.FromFile(String.Concat("Images", System.IO.Path.DirectorySeparatorChar, "QuestionMark.png"));
                        }
                    }
                    else
                    {
                        try
                        {
                            itemImage.Image = System.Drawing.Bitmap.FromFile(String.Concat("Images", System.IO.Path.DirectorySeparatorChar, comboBox_Item.SelectedValue.ToString(), ".png"));
                        }
                        catch (System.IO.FileNotFoundException exFNF)
                        {
                            itemImage.Image = System.Drawing.Bitmap.FromFile(String.Concat("Images", System.IO.Path.DirectorySeparatorChar, "QuestionMark.png"));
                        }

                    }
                }
                else
                {
                    try
                    {
                        itemImage.Image = System.Drawing.Bitmap.FromFile(String.Concat("Images", System.IO.Path.DirectorySeparatorChar, comboBox_Item.SelectedValue.ToString(), ".png"));
                    }
                    catch (System.IO.FileNotFoundException exFNF)
                    {
                        itemImage.Image = System.Drawing.Bitmap.FromFile(String.Concat("Images", System.IO.Path.DirectorySeparatorChar, "QuestionMark.png"));
                    }

                }
            }

        }

    }
}
