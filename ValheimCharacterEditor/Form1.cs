using System;
using System.Windows.Forms;
using System.Linq;

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
            comboBox_Gender.DataSource = ValheimEngine.Genders;
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
            if (Customization.FirstRun || comboBox_Characters.SelectedIndex == -1 || comboBox_Characters.SelectedItem.ToString() == null)
            {
                return;
            }        

            try
            {
                // Initialize
                Customization.Initialize(comboBox_Characters.SelectedItem.ToString());

                // Put appearance in gui
                textBox_Name.Text = Customization.SelectedCharacter.Data.Name;
                comboBox_Beard.SelectedIndex = comboBox_Beard.FindStringExact(ValheimEngine.BeardsUI[Util.FindInArrayString(ValheimEngine.BeardsInternal, Customization.SelectedCharacter.Data.Beard)]);
                comboBox_Hair.SelectedIndex = comboBox_Hair.FindStringExact(ValheimEngine.HairsUI[Util.FindInArrayString(ValheimEngine.HairsInternal, Customization.SelectedCharacter.Data.Hair)]);
                comboBox_Gender.SelectedIndex = comboBox_Gender.FindStringExact(Customization.GenderInternaltoUI(Customization.SelectedCharacter.Data.Gender));
                textBox_HairColor.BackColor = Util.Vec3ToColor(Customization.SelectedCharacter.Data.HairColor);
                textBox_SkinTone.BackColor = Util.Vec3ToColor(Customization.SelectedCharacter.Data.SkinColor);

                // Enable gui elements
                textBox_Name.Enabled = true;
                comboBox_Beard.Enabled = true;
                comboBox_Hair.Enabled = true;
                comboBox_Gender.Enabled = true;
                button_SkinTone.Enabled = true;
                button_HairColor.Enabled = true;
                textBox_HairColor.Enabled = true;
                textBox_SkinTone.Enabled = true;
                button_Apply.Enabled = true;
                button_Skills.Enabled = true;
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
            comboBox_Gender.Enabled = false;
            button_SkinTone.Enabled = false;
            button_HairColor.Enabled = false;
            textBox_HairColor.Enabled = false;
            textBox_SkinTone.Enabled = false;
            button_Apply.Enabled = false;
            button_Skills.Enabled = false;

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
                String.IsNullOrEmpty(comboBox_Hair.SelectedItem.ToString())  || String.IsNullOrEmpty(comboBox_Gender.SelectedItem.ToString()))
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
                                                            comboBox_Hair.SelectedItem.ToString() + ".\n\t- Gender: " +
                                                            comboBox_Gender.SelectedItem.ToString() + ".\n\n Do you want to continue?",
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
                Customization.SelectedCharacter.Data.Gender = Customization.GenderUItoInternal(comboBox_Gender.SelectedItem.ToString());
                Customization.SelectedCharacter.Data.HairColor = Util.ColorToVec3(textBox_HairColor.BackColor);
                Customization.SelectedCharacter.Data.SkinColor = Util.ColorToVec3(textBox_SkinTone.BackColor);

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

        private void button_SkinTone_Click(object sender, EventArgs e)
        {
            colorDialog_SkinTone.Color = textBox_SkinTone.BackColor;
            if (colorDialog_SkinTone.ShowDialog() == DialogResult.OK)
                textBox_SkinTone.BackColor = colorDialog_SkinTone.Color;
        }

        private void button_HairColor_Click(object sender, EventArgs e)
        {
            colorDialog_HairColor.Color = textBox_HairColor.BackColor;
            if (colorDialog_HairColor.ShowDialog() == DialogResult.OK)
                textBox_HairColor.BackColor = colorDialog_HairColor.Color;
        }
          

        private void button_Skills_Click(object sender, EventArgs e)
        {
            Form_Skills skills_form = new Form_Skills();
            skills_form.ShowDialog();
            

        }
    }
}
