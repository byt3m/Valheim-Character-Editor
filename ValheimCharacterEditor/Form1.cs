using System;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace ValheimCharacterEditor
{
    public partial class Form1 : Form
    {
        private string[] presetNames;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Check game is not running
            if (Util.isGameRunning())
            {
                MessageBox.Show("Please close Valheim before editing your character.", "ERROR", MessageBoxButtons.OK);
                Application.Exit();
            }

            // Populate forms with data
            _Populate();

            // Complete first run
            Customization.FirstRun = false;
        }

        private void _Populate()
        {
            // Get characters
            try
            {
                Customization.GetCharacters();
            }
            catch
            {
                MessageBox.Show("There was an error while scanning for characters.", "ERROR", MessageBoxButtons.OK);
            }

            // Populate forms with data
            comboBox_Characters.DataSource = Customization.Characters;
            comboBox_Characters.SelectedIndex = -1;
            comboBox_Beard.DataSource = Customization.BeardsUi;
            comboBox_Hair.DataSource = Customization.HairsUi;
            
            presetNames = new string[Customization.HairColorPresets.Count];
            for(var i = 0; i < Customization.HairColorPresets.Count; i++)
                presetNames[i] = Customization.HairColorPresets.ElementAt(i).Name;

            comboBox_HairColor.DataSource = presetNames;
        }

        private void Form1_move(object sender, MouseEventArgs e)
        {
            Capture = false;
            Message msg = Message.Create(Handle, 0xA1, (IntPtr)0x02, IntPtr.Zero);
            base.WndProc(ref msg);
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
            if (String.IsNullOrEmpty(textBox_Name.Text))
                return;

            String new_text = "";

            foreach (var t in textBox_Name.Text)
            {
                if (char.IsLetter(t) || t == ' ')
                {
                    new_text += t;
                }
            }

            textBox_Name.Text = new_text;
            textBox_Name.Select(textBox_Name.Text.Length, 0);
        }

        private void checkBox_ChangeName_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_ChangeName.Checked)
                textBox_Name.Enabled = true;
            else
                textBox_Name.Enabled = false;
        }

        private void comboBox_Characters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Customization.FirstRun)
            {
                return;
            }        

            try
            {
                // Initialize
                Customization.Initialize(comboBox_Characters.SelectedItem.ToString());

                // Interpret internal names as a human readable ones (beard and hair)
                Customization.InternalToName();

                // Put appearance in gui
                comboBox_Beard.SelectedIndex = comboBox_Beard.FindStringExact(Customization.SelectedCharacterBeard);
                comboBox_Hair.SelectedIndex = comboBox_Hair.FindStringExact(Customization.SelectedCharacterHair);
                comboBox_HairColor.SelectedIndex = comboBox_HairColor.FindStringExact(Customization.closestPreset.Name);
                textBox_Name.Text = Customization.CurrentCharacter.Name;

                // Enable gui elements
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

        private void _Refresh()
        {
            // Disable forms to avoid a crash
            comboBox_Beard.Enabled = false;
            comboBox_Hair.Enabled = false;
            comboBox_HairColor.Enabled = false;
            button_Apply.Enabled = false;

            // Make a first run again to avoid fully executing "comboBox_Characters_SelectedIndexChanged"
            Customization.FirstRun = true;

            // Populate forms with data
            _Populate();

            // Complete first run
            Customization.FirstRun = false;
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            // Check GUI elements content
            if (String.IsNullOrEmpty(comboBox_Characters.SelectedItem.ToString()) || String.IsNullOrEmpty(comboBox_Beard.SelectedItem.ToString()) ||
                String.IsNullOrEmpty(comboBox_Hair.SelectedItem.ToString()) || String.IsNullOrEmpty(comboBox_HairColor.SelectedItem.ToString()))
            {
                MessageBox.Show("At least character, beard, hair and color must be chosen.", "ERROR", MessageBoxButtons.OK);
                return;
            }

            // Check name if enabled
            if (checkBox_ChangeName.Checked && String.IsNullOrEmpty(textBox_Name.Text) && !Customization.IsCorrectName(textBox_Name.Text))
            {
                MessageBox.Show("Name must contain ONLY letters (A-Z) and stay between 3 and 15 characters long.", "ERROR", MessageBoxButtons.OK);
                return;
            }

            try
            {
                // WRITE CUSTOMIZATION WITH NAME
                // Ask to continue
                DialogResult continue_with_write = MessageBox.Show("The following customization will be applied:\n\t- Name: " +
                                                            textBox_Name.Text + "\n\t- Beard: " +
                                                            comboBox_Beard.SelectedItem.ToString() + ".\n\t- Hair: " +
                                                            comboBox_Hair.SelectedItem.ToString() + ".\n\t- Hair color: " +
                                                            comboBox_HairColor.SelectedItem.ToString() + ".\n\n Do you want to continue?",
                                                            "WARNING", MessageBoxButtons.YesNo);
                if (continue_with_write == DialogResult.No)
                    return;

                // Make a backup of the selected character file
                if (!Util.BackupFile(Customization.CurrentCharacter.Path))
                {
                    MessageBox.Show("Error while backing up character file.", "ERROR", MessageBoxButtons.OK);
                    return;
                }

                // Apply changes from the form into CurrentCharacter
                Customization.CurrentCharacter.Name = textBox_Name.Text;
                Customization.CurrentCharacter.Hair = Customization.HairsInternal[Array.IndexOf(Customization.HairsUi, comboBox_Hair.SelectedItem)];
                Customization.CurrentCharacter.Beard = Customization.BeardsInternal[Array.IndexOf(Customization.BeardsUi, comboBox_Beard.SelectedItem)];
                int index = Array.IndexOf(presetNames, comboBox_HairColor.Text);
                Customization.CurrentCharacter.HairColor = new Customization.Vector3
                {
                    X = Customization.HairColorPresets.ElementAt(index).Red,
                    Y = Customization.HairColorPresets.ElementAt(index).Green,
                    Z = Customization.HairColorPresets.ElementAt(index).Blue,
                };

                // Write customization, if fail restore backup
                if (Customization.WriteCustomization())
                {
                    MessageBox.Show("Customization applied.", "INFO", MessageBoxButtons.OK);
                    _Refresh();
                }
                else
                {
                    MessageBox.Show("There was an error while applying the new customization. Last backup will be restored.", "ERROR", MessageBoxButtons.OK);
                    Util.RestoreFile();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("There was an error while applying the new customization. Last backup will be restored.", "FATAL ERROR", MessageBoxButtons.OK);
                Util.RestoreFile();
            }
        }

        private void button_RepairCharacter_Click(object sender, EventArgs e)
        {

        }
    }
}
