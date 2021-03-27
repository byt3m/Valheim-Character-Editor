using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ValheimCharacterEditor
{
    public partial class Form_Skills : Form
    {
        public Form_Skills()
        {
            InitializeComponent();
        }

        private void Skills_Load(object sender, EventArgs e)
        {
            // Populate forms with data
            _Populate();
        }

        private void _Populate()
        {
            // Show selected character in form
            label_Character.Text = Customization.SelectedCharacter.Data.Name;

            // Add controls for editable skills 
            int SkillStartPositionV = 40;
            int SkillStartPositionH = 18;
            foreach (var skill in ValheimEngine.SkillsUI)
            {
                Controls.Skill_control skillcontrol = new Controls.Skill_control();
                skillcontrol.Location = new System.Drawing.Point(SkillStartPositionH, SkillStartPositionV);
                skillcontrol.skill_name = skill.ToString();
                int skill_level = 101;
                ValheimEngine.Character.SkillName SN = (ValheimEngine.Character.SkillName)Enum.Parse(typeof(ValheimEngine.Character.SkillName), skill.ToString());
                Customization.SelectedCharacter.Data.Skills.Where(w => w.SkillName == SN).ToList().ForEach(s => skill_level  = (int)s.Level);
                              
                if (skill_level != 101)
                {
                    skillcontrol.skill_level = skill_level;
                    Controls.Add(skillcontrol);
                } else
                {
                    ValheimEngine.Character.Skill add_skill = new ValheimEngine.Character.Skill();
                    add_skill.Level = 1;
                    add_skill.SkillName = SN;
                    Customization.SelectedCharacter.Data.Skills.Add(add_skill);
                    skillcontrol.skill_level = 1;
                    Controls.Add(skillcontrol);
                }                

                SkillStartPositionV += 70;
                if (SkillStartPositionV > 700)
                {
                    SkillStartPositionH += 500;
                    SkillStartPositionV = 40;
                }
            }

        }

        private void button_apply_Click(object sender, EventArgs e)
        {
            try
            {
                // Make a backup of the selected character file
                if (!Util.BackupFile(Customization.SelectedCharacter.File))
                {
                    MessageBox.Show("Error while backing up character file.", "ERROR", MessageBoxButtons.OK);
                    return;
                }
                               
                // Write customization, if fail restore backup
                if (Customization.WriteCustomization())
                {
                    MessageBox.Show("Skill customization applied.", "INFO", MessageBoxButtons.OK);
                    _Populate();
                    Close();
                }
                else
                {
                    MessageBox.Show("There was an error while applying the new skill customization. Last backup will be restored.", "ERROR", MessageBoxButtons.OK);
                    Util.RestoreFile();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("There was an error while applying the new skill customization. Last backup will be restored.", "FATAL ERROR", MessageBoxButtons.OK);
                Util.RestoreFile();
            }
        }

        private void button_set_1_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if(ctrl.GetType().ToString() == "ValheimCharacterEditor.Controls.Skill_control")
                {

                    Controls.Skill_control skillcontrol = (Controls.Skill_control)ctrl;
                    skillcontrol.skill_level = 1;
                }
                Console.WriteLine(ctrl.Name);
                Console.WriteLine(ctrl.GetType().ToString());
            }
        }

        private void button_set_50_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.GetType().ToString() == "ValheimCharacterEditor.Controls.Skill_control")
                {

                    Controls.Skill_control skillcontrol = (Controls.Skill_control)ctrl;
                    skillcontrol.skill_level = 50;
                }
                Console.WriteLine(ctrl.Name);
                Console.WriteLine(ctrl.GetType().ToString());
            }

        }

        private void button_set_100_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.GetType().ToString() == "ValheimCharacterEditor.Controls.Skill_control")
                {
                    Controls.Skill_control skillcontrol = (Controls.Skill_control)ctrl;
                    skillcontrol.skill_level = 100;
                }
                Console.WriteLine(ctrl.Name);
                Console.WriteLine(ctrl.GetType().ToString());
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

        private void FormSkills_Move(object sender, MouseEventArgs e)
        {
            Capture = false;
            Message msg = Message.Create(Handle, 0xA1, (IntPtr)0x02, IntPtr.Zero);
            base.WndProc(ref msg);
        }
    }
}
