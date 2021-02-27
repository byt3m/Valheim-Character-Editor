using System;
using System.Windows.Forms;
using System.Linq;

namespace ValheimCharacterEditor
{
    public partial class Form1 : Form
    {
        private string[] _presetNames;

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
            comboBox_Characters.DataSource = Util.GetCharactersNames(Customization.FoundCharacters);
            comboBox_Characters.SelectedIndex = -1;
            comboBox_Beard.DataSource = ValheimEngine.BeardsUI;
            comboBox_Hair.DataSource = ValheimEngine.HairsUI;
            
            _presetNames = new string[Customization.HairColorPresets.Count];
            for(int i = 0; i < Customization.HairColorPresets.Count; i++)
                _presetNames[i] = Customization.HairColorPresets.ElementAt(i).Name;

            comboBox_HairColor.DataSource = _presetNames;
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

        private void comboBox_Characters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Customization.FirstRun || String.IsNullOrEmpty(comboBox_Characters.SelectedItem.ToString()))
            {
                return;
            }        

            try
            {
                // Initialize
                Customization.Initialize(comboBox_Characters.SelectedItem.ToString());

                // Put appearance in gui
                comboBox_Beard.SelectedIndex = comboBox_Beard.FindStringExact(ValheimEngine.BeardsUI[Util.FindInArrayString(ValheimEngine.BeardsInternal, Customization.SelectedCharacter.Data.Beard)]);
                comboBox_Hair.SelectedIndex = comboBox_Hair.FindStringExact(ValheimEngine.HairsUI[Util.FindInArrayString(ValheimEngine.HairsInternal, Customization.SelectedCharacter.Data.Hair)]);
                comboBox_HairColor.SelectedIndex = comboBox_HairColor.FindStringExact(Customization.SelectedCharacter.ColorPreset.Name);
                textBox_Name.Text = Customization.SelectedCharacter.Data.Name;
                checkBox_Female.Checked = Customization.SelectedCharacter.Data.Model == 1;

                // Enable gui elements
                textBox_Name.Enabled = true;
                comboBox_Beard.Enabled = true;
                comboBox_Hair.Enabled = true;
                comboBox_HairColor.Enabled = true;
                checkBox_Female.Enabled = true;
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
            textBox_Name.Enabled = false;
            comboBox_Beard.Enabled = false;
            comboBox_Hair.Enabled = false;
            comboBox_HairColor.Enabled = false;
            checkBox_Female.Enabled = false;
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
            if (String.IsNullOrEmpty(comboBox_Beard.SelectedItem.ToString()) || String.IsNullOrEmpty(textBox_Name.Text) ||
                String.IsNullOrEmpty(comboBox_Hair.SelectedItem.ToString())  || String.IsNullOrEmpty(comboBox_HairColor.SelectedItem.ToString()))
            {
                MessageBox.Show("Please fill in every field under Character customization.", "ERROR", MessageBoxButtons.OK);
                return;
            }

            // Check name length
            if (!(textBox_Name.Text.Length >= 3 && textBox_Name.Text.Length <= 15))
            {
                MessageBox.Show("Name must be between 3 and 15 characters.", "ERROR", MessageBoxButtons.OK);
                return;
            }

            try
            {
                // Ask to continue and write customization
                DialogResult continue_with_write = MessageBox.Show("The following customization will be applied:\n\t- Name: " +
                                                            textBox_Name.Text + "\n\t- Beard: " +
                                                            comboBox_Beard.SelectedItem.ToString() + ".\n\t- Hair: " +
                                                            comboBox_Hair.SelectedItem.ToString() + ".\n\t- Hair color: " +
                                                            comboBox_HairColor.SelectedItem.ToString() + ".\n\t- Gender: " +
                                                            (checkBox_Female.Checked ? "Female" : "Male") + ".\n\n Do you want to continue?",
                                                            "WARNING", MessageBoxButtons.YesNo);
                if (continue_with_write == DialogResult.No)
                    return;

                // Make a backup of the selected character file
                if (!Util.BackupFile(Customization.SelectedCharacter.File))
                {
                    MessageBox.Show("Error while backing up character file.", "ERROR", MessageBoxButtons.OK);
                    return;
                }

                // Apply changes from the form into CurrentCharacter
                Customization.SelectedCharacter.Data.Name = textBox_Name.Text;
                Customization.SelectedCharacter.Data.Hair = ValheimEngine.HairsInternal[Array.IndexOf(ValheimEngine.HairsUI, comboBox_Hair.SelectedItem)];
                Customization.SelectedCharacter.Data.Beard = ValheimEngine.BeardsInternal[Array.IndexOf(ValheimEngine.BeardsUI, comboBox_Beard.SelectedItem)];
                Customization.SelectedCharacter.Data.HairColor = Customization.GetHairColor(Array.IndexOf(_presetNames, comboBox_HairColor.Text));
                Customization.SelectedCharacter.Data.Model = checkBox_Female.Checked ? 1 : 0;

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

        private void textBox_Name_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox_Name.Text))
                return;

            String new_text = "";

            for (int i = 0; i < textBox_Name.Text.Length; i++)
            {
                if (!ValheimEngine.NameDisallowedCharacters.Contains(textBox_Name.Text[i]))
                {
                    new_text += textBox_Name.Text[i];
                }
            }

            textBox_Name.Text = new_text;
            textBox_Name.Select(textBox_Name.Text.Length, 0);
        }
    }
}
