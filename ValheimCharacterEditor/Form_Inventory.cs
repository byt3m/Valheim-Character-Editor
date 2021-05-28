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
    public partial class Form_Inventory : Form
    {
        Controls.Item_control[,] _inventory = new Controls.Item_control[4,8];
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

            // Show selected character in form
            label_Character.Text = Customization.SelectedCharacter.Data.Name;

            // Add controls for inventory
            int InventoryStartPositionV = 40;
            int InventoryStartPositionH = 18;
            int x=0,y=0;
            for(int i=0; i<32;i++)
            {
                
                Controls.Item_control itemcontrol = new Controls.Item_control();
                itemcontrol.Location = new System.Drawing.Point(InventoryStartPositionH, InventoryStartPositionV);
                itemcontrol.Position = new Tuple<int, int>(x, y);

                _inventory[y, x] = itemcontrol;

                Controls.Add(itemcontrol);
                
                if (i > 0 && (i % 8 == 7))
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


    }
}
