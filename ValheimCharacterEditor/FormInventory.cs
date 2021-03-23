using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValheimCharacterEditor
{
    public partial class FormInventory : Form
    {
        public FormInventory()
        {
            InitializeComponent();
            Populate();
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Populate()
        {
            dataGridView_Inventory.DataSource = Customization.SelectedCharacter.Data.Inventory.Select(x => new InventoryData(x)).ToList();
        }

        private class InventoryData
        {
            protected ValheimEngine.Character.Item data;

            public InventoryData(ValheimEngine.Character.Item _data) => data = _data;

            [DisplayName("Equipped")]
            public bool Equipped
            {
                get { return data.Equipped; }
                set { data.Equipped = value; }
            }

            [DisplayName("Name")]
            public string Name
            {
                get { return data.Name; }
                set { data.Name = value; }
            }

            [DisplayName("Stack")]
            public int Stack
            {
                get { return data.Stack; }
                set { data.Stack = value; }
            }

            [DisplayName("Pos")]
            public Tuple<int, int> Pos
            {
                get { return data.Pos; }
                set { data.Pos = value; }
            }

            [DisplayName("Durability")]
            public float Durability
            {
                get { return data.Durability; }
                set { data.Durability = value; }
            }


            [DisplayName("Quality")]
            public int Quality
            {
                get { return data.Quality; }
                set { data.Quality = value; }
            }

            [DisplayName("Variant")]
            public int Variant
            {
                get { return data.Variant; }
                set { data.Variant = value; }
            }

            internal ValheimEngine.Character.Item GetObject()
            {
                return data;
            }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private Tuple<int, int> NextFreeSlot()
        {
            bool[,] slots = new bool[8, 4];
            foreach (var item in Customization.SelectedCharacter.Data.Inventory)
                slots[item.Pos.Item1, item.Pos.Item2] = true;
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 4; y++)
                    if (slots[x, y] == false)
                        return new Tuple<int, int>(x, y);
            return null;
        }


        private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pos = NextFreeSlot();
            if (pos == null)
            {
                MessageBox.Show("No inventory space");
                return;
            }
            if (dataGridView_Inventory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select an item to clone");
                return;
            }
            var item = (InventoryData)dataGridView_Inventory.SelectedRows[0].DataBoundItem;
            var newItem = new ValheimEngine.Character.Item()
            {
                CrafterId = 0,
                CrafterName = "",
                Durability = item.Durability,
                Equipped = false,
                Name = item.Name,
                Pos = pos,
                Quality = item.Quality,
                Stack = item.Stack,
                Variant = item.Variant
            };
            Customization.SelectedCharacter.Data.Inventory.Add(newItem);
            Populate();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_Inventory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select an item to delete");
                return;
            }
            var item = (InventoryData)dataGridView_Inventory.SelectedRows[0].DataBoundItem;
            for (int i = 0; i < Customization.SelectedCharacter.Data.Inventory.Count; i++)
            {
                if (Customization.SelectedCharacter.Data.Inventory[i].Pos == item.Pos)
                {
                    Customization.SelectedCharacter.Data.Inventory.RemoveAt(i);
                    break;
                }
            }
            Populate();
        }
    }
}
