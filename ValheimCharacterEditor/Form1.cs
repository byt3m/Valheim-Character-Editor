using System;
using System.Windows.Forms;

namespace ValheimCharacterEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Util.isGameRunning())
            {
                MessageBox.Show("Please close Valheim before editing your character.", "ERROR", MessageBoxButtons.OK);
                Application.Exit();
            }

            try
            {
                Customization.GetCharacters();
            }
            catch
            {
                MessageBox.Show("There was an error while scanning for characters.", "ERROR", MessageBoxButtons.OK);
            }

            comboBox_Characters.DataSource = Customization.Characters;
            comboBox_Characters.SelectedIndex = -1;

            comboBox_Beard.DataSource = Customization.Beards_UI;
            comboBox_Hair.DataSource = Customization.Hairs_UI;
            comboBox_HairColor.DataSource = Customization.Hair_Colors;

            Customization.FirstRun = false;
        }

        private void Form1_move(object sender, MouseEventArgs e)
        {
            Capture = false;
            Message msg = Message.Create(Handle, 0xA1, (IntPtr)0x02, IntPtr.Zero);
            base.WndProc(ref msg);
        }

        private void comboBox_Characters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Customization.FirstRun)
            {
                return;
            }        

            try
            {
                // Save character name and file for future operations
                Customization.Initialize(comboBox_Characters.SelectedItem.ToString());

                // Read current character data
                //String name = Customization.ReadCharacterName();
                String beard = Customization.ReadCharacterBeard();
                String hair = Customization.ReadCharacterHair();
                String color = Customization.ReadCharacterColor();

                if (/*String.IsNullOrEmpty(name) ||*/ String.IsNullOrEmpty(beard) || String.IsNullOrEmpty(hair))
                {
                    //textBox_Name.Enabled = false;
                    comboBox_Beard.Enabled = false;
                    comboBox_Hair.Enabled = false;
                    comboBox_HairColor.Enabled = false;
                    button_Apply.Enabled = false;
                    return;
                }

                //textBox_Name.Text = name;
                comboBox_Beard.SelectedIndex = comboBox_Beard.FindStringExact(beard);
                comboBox_Hair.SelectedIndex = comboBox_Hair.FindStringExact(hair);
                comboBox_HairColor.SelectedIndex = comboBox_HairColor.FindStringExact(color);

                //textBox_Name.Enabled = true;
                comboBox_Beard.Enabled = true;
                comboBox_Hair.Enabled = true;
                comboBox_HairColor.Enabled = true;
                button_Apply.Enabled = true;
            }
            catch
            {
                MessageBox.Show("There was an error while trying to get character data.", "ERROR", MessageBoxButtons.OK);
            }
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox_Characters.SelectedItem.ToString()) ||
                String.IsNullOrEmpty(comboBox_Beard.SelectedItem.ToString()) ||
                String.IsNullOrEmpty(comboBox_Hair.SelectedItem.ToString()) ||
                String.IsNullOrEmpty(comboBox_HairColor.SelectedItem.ToString()))
            {
                MessageBox.Show("Beard, hair and color must be chosen.", "WARNING", MessageBoxButtons.OK);

                return;
            }

            try
            {
                if (Customization.WriteCustomization(textBox_Name.Text, comboBox_Beard.SelectedItem.ToString(), comboBox_Hair.SelectedItem.ToString(), comboBox_HairColor.SelectedItem.ToString()))
                {
                    MessageBox.Show("Customization applied.", "INFO", MessageBoxButtons.OK);
                    Application.Exit();
                }
            }
            catch
            {
                MessageBox.Show("There was an error while applying the new customization. Character data will be restored.", "ERROR", MessageBoxButtons.OK);
                Util.RestoreFile(Customization.last_backup);
            }
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
