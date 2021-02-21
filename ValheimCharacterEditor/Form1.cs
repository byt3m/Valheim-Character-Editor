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
            if (Util.IsGameRunning())
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

            comboBox_Beard.DataSource = Customization.BeardsUi;
            comboBox_Hair.DataSource = Customization.HairsUi;
            comboBox_HairColor.DataSource = Customization.HairColors;

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
            //try
            //{
                // Save character name and file for future operations
                Customization.Initialize(comboBox_Characters.SelectedItem.ToString());

                // Read current character data
                string name = Customization.ReadCharacterName();
                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Pattern not found. Character can not be customized.", "ERROR", MessageBoxButtons.OK);
                    button_RepairCharacter.Enabled = true;
                    return;
                }

                string beard = Customization.ReadCharacterBeard();
                string hair = Customization.ReadCharacterHair();
                if (string.IsNullOrEmpty(hair) && string.IsNullOrEmpty(beard))
                {
                    MessageBox.Show("Please use the \"Repair character\" button.", "ERROR", MessageBoxButtons.OK);
                    button_RepairCharacter.Enabled = true;
                    return;
                }

                string color = Customization.ReadCharacterColor();
                if (string.IsNullOrEmpty(color))
                {
                    MessageBox.Show("Pattern not found. Character can not be customized.", "ERROR", MessageBoxButtons.OK);
                    return;
                }

                button_RepairCharacter.Enabled = false;


                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(beard) || string.IsNullOrEmpty(hair))
                {
                    textBox_Name.Enabled = false;
                    comboBox_Beard.Enabled = false;
                    comboBox_Hair.Enabled = false;
                    comboBox_HairColor.Enabled = false;
                    button_Apply.Enabled = false;
                    return;
                }

                textBox_Name.Text = name;
                comboBox_Beard.SelectedIndex = comboBox_Beard.FindStringExact(beard);
                comboBox_Hair.SelectedIndex = comboBox_Hair.FindStringExact(hair);
                comboBox_HairColor.SelectedIndex = comboBox_HairColor.FindStringExact(color);

                //textBox_Name.Enabled = true;
                comboBox_Beard.Enabled = true;
                comboBox_Hair.Enabled = true;
                comboBox_HairColor.Enabled = true;
                button_Apply.Enabled = true;
            //}
            //catch
            //{
            //    MessageBox.Show("There was an error while trying to get character data.", "ERROR", MessageBoxButtons.OK);
            //}
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox_Characters.SelectedItem.ToString()) ||
                string.IsNullOrEmpty(comboBox_Beard.SelectedItem.ToString()) ||
                string.IsNullOrEmpty(comboBox_Hair.SelectedItem.ToString()) ||
                string.IsNullOrEmpty(comboBox_HairColor.SelectedItem.ToString()))
            {
                MessageBox.Show("Beard, hair and color must be chosen.", "WARNING", MessageBoxButtons.OK);

                return;
            }

            try
            {
                if (!Customization.WriteCustomization(textBox_Name.Text, comboBox_Beard.SelectedItem.ToString(),
                    comboBox_Hair.SelectedItem.ToString(), comboBox_HairColor.SelectedItem.ToString(),
                    checkBox_ChangeName)) return;
                MessageBox.Show("Customization applied.", "INFO", MessageBoxButtons.OK);
                Application.Exit();
            }
            catch
            {
                MessageBox.Show("There was an error while applying the new customization. Character data will be restored.", "ERROR", MessageBoxButtons.OK);
                Util.RestoreFile(Customization.LastBackup);
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

        private void textBox_Name_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_Name.Text))
                return;

            string newText = "";


            if (Customization.IsCorrectName(textBox_Name.Text))
            {
                newText = textBox_Name.Text;
            }

            textBox_Name.Text = newText;
            textBox_Name.Select(textBox_Name.Text.Length, 0);
        }

        private void button_RepairCharacter_Click(object sender, EventArgs e)
        {
            if (!Customization.RepairCharacter())
            {
                button_RepairCharacter.Enabled = false;
                Application.Exit();
            }

            MessageBox.Show("Character repaired, please restart the program.", "INFO", MessageBoxButtons.OK);
            Application.Exit();
        }

        private void checkBox_ChangeName_CheckedChanged(object sender, EventArgs e)
        {
            textBox_Name.Enabled = checkBox_ChangeName.Checked;
        }
    }
}
