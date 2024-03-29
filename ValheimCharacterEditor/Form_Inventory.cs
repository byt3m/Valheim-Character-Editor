﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValheimCharacterEditor.Controls;

namespace ValheimCharacterEditor
{
    public partial class Form_Inventory : Form
    {
        Controls.Item_control[,] _inventory = new Controls.Item_control[4,8];//Non-Modded inventory size
        public Form_Inventory()
        {
            InitializeComponent();
        }
        private void Form_Inventory_Load(object sender, EventArgs e)
        {
            _Populate();
        }
        private void _Populate()
        {
            if (Customization.SelectedCharacter.Data is null || Customization.SelectedCharacter.File is null)
            {
                MessageBox.Show("No Character Selected");
                this.Close();
                return;
            }
            // Show selected character in form
            label_Character.Text = Customization.SelectedCharacter.Data.Name;
            
            //Compatibility with Inventory Mods
            int maxY=0, maxX = 0;
            foreach (ValheimEngine.Character.Item item in Customization.SelectedCharacter.Data.Inventory)
            {
                if (item.Pos.Item2 > maxY)
                {
                    maxY = item.Pos.Item2;
                }
                if(item.Pos.Item1 > maxX)
                {
                    maxX = item.Pos.Item1;
                }
            }
            if(maxY+1 > _inventory.GetLength(0) || maxX+1 > _inventory.GetLength(1))
            {
                _inventory = new Controls.Item_control[maxY+1, maxX+1];
                //Add the difference from the standard inventory onto the window size
                this.Width += 70 * ((maxX + 1) - 8);
                this.Height += 70 * ((maxY + 1) - 4);
            }

            // Add controls for inventory
            int InventoryStartPositionV = 40;
            int InventoryStartPositionH = 18;
            int x=0,y=0;
            for(int i=0; i<_inventory.Length;i++)
            {
                
                Controls.Item_control itemcontrol = new Controls.Item_control();
                itemcontrol.Location = new System.Drawing.Point(InventoryStartPositionH, InventoryStartPositionV);
                itemcontrol.Position = new Tuple<int, int>(x, y);
                itemcontrol.ItemClicked += ItemControlClicked;

                _inventory[y, x] = itemcontrol;

                Controls.Add(itemcontrol);
                
                if (i > 0 && (i % (maxX+1) == maxX))
                {
                    InventoryStartPositionH = 18;
                    InventoryStartPositionV += 70;
                    y++;
                    x = 0;
                }
                else
                {
                    InventoryStartPositionH += 70;
                    x++;
                }

            }
            if (Customization.SelectedCharacter.Data.Inventory is null)
            {
                return;
            }
            foreach(ValheimEngine.Character.Item item in Customization.SelectedCharacter.Data.Inventory)
            {
                _inventory[item.Pos.Item2, item.Pos.Item1].ItemData = item;
            }

        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ItemControlClicked(object sender, ItemControlClickEventArgs e)
        {          
            Form_Item itemproperties = new Form_Item(_inventory[e.y, e.x].ItemData, e.x, e.y);
            itemproperties.ApplyItemChange += ItemChangeApplied;
            var location = _inventory[e.y, e.x].PointToScreen(Point.Empty);
            itemproperties.StartPosition = FormStartPosition.Manual;
            itemproperties.Location = new System.Drawing.Point(location.X + _inventory[e.y, e.x].Width, location.Y);
            itemproperties.ShowDialog();
            
        }

        private void ItemChangeApplied(object sender, ValheimEngine.Character.Item e)
        {
            _inventory[e.Pos.Item2, e.Pos.Item1].ItemData = e;
        }

        private void Form_Inventory_Move(object sender, MouseEventArgs e)
        {
            Capture = false;
            Message msg = Message.Create(Handle, 0xA1, (IntPtr)0x02, IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            Customization.SelectedCharacter.Data.Inventory.Clear();
            for(int i=0; i < _inventory.GetLength(0); i++)
            {
                for(int j=0; j< _inventory.GetLength(1); j++)
                {
                    if(!(_inventory[i,j] is null))
                    {
                        if (!(_inventory[i, j].ItemData is null))
                        {
                            Customization.SelectedCharacter.Data.Inventory.Add(_inventory[i, j].ItemData);
                        }
                    }
                }
            }
        }
    }
}
