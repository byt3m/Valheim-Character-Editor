using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValheimCharacterEditor.Controls
{
    public partial class Skill_control : UserControl
    {
        public string skill_name
        {
           get
            {
                return  groupBox1.Text; 
            }
            set
            {
                groupBox1.Text = value;

            }
        }

        public int skill_level
        {
            get
            {
                return Int32.Parse(label_skill_level.Text);
            }
            set
            {

                if (value == 0)
                {
                    value = 1;

                }
                trackBar_skill.Value = value;
                label_skill_level.Text = value.ToString();
                ValheimEngine.Character.SkillName SN = (ValheimEngine.Character.SkillName)Enum.Parse(typeof(ValheimEngine.Character.SkillName), skill_name);
                Customization.SelectedCharacter.Data.Skills.Where(w => w.SkillName == SN).ToList().ForEach(s => s.Level = value);

            }
        }



        public Skill_control()
        {
            InitializeComponent();
        }

        private void trackBar_skill_Scroll(object sender, EventArgs e)
        {
            label_skill_level.Text = trackBar_skill.Value.ToString();
            ValheimEngine.Character.SkillName SN = (ValheimEngine.Character.SkillName)Enum.Parse(typeof(ValheimEngine.Character.SkillName), skill_name);
            Customization.SelectedCharacter.Data.Skills.Where(w => w.SkillName == SN).ToList().ForEach(s => s.Level = trackBar_skill.Value);
        }
    }
}
