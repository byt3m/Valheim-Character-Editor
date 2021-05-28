using System;
using System.Windows.Forms;

namespace ValheimCharacterEditor.Controls
{
    public partial class Item_control : UserControl
    {
        ValheimEngine.Character.Item _itemData = null;
        int _x, _y;
        public Item_control()
        {
            InitializeComponent();
        }
        public bool HasItem { get { return !(_itemData is null); } }
        public ValheimEngine.Character.Item ItemData
        {
            get
            {
                return _itemData;
            }
            set
            {
                _itemData = value;
                if (!(value is null))
                {
                    if (ValheimEngine.ItemProperties.ContainsKey(_itemData.Name))
                    {
                        ValheimEngine.GameObjectItemProperties gameobject = ValheimEngine.ItemProperties[_itemData.Name];
                        if (gameobject.MaxVariants > 1 && _itemData.Variant > 0)
                        {
                            try
                            {
                                itemImage.Image = System.Drawing.Bitmap.FromFile(String.Concat("Images", System.IO.Path.DirectorySeparatorChar, value.Name, _itemData.Variant , ".png"));
                            }
                            catch (System.IO.FileNotFoundException exFNF)
                            {
                                itemImage.Image = System.Drawing.Bitmap.FromFile(String.Concat("Images", System.IO.Path.DirectorySeparatorChar, "QuestionMark.png"));
                            }
                        }
                        else
                        {
                            try
                            {
                                itemImage.Image = System.Drawing.Bitmap.FromFile(String.Concat("Images", System.IO.Path.DirectorySeparatorChar, value.Name, ".png"));
                            }
                            catch (System.IO.FileNotFoundException exFNF)
                            {
                                itemImage.Image = System.Drawing.Bitmap.FromFile(String.Concat("Images", System.IO.Path.DirectorySeparatorChar, "QuestionMark.png"));
                            }

                        }
                    }
                    else
                    {
                        try
                        {
                            itemImage.Image = System.Drawing.Bitmap.FromFile(String.Concat("Images", System.IO.Path.DirectorySeparatorChar, value.Name, ".png"));
                        }
                        catch (System.IO.FileNotFoundException exFNF)
                        {
                            itemImage.Image = System.Drawing.Bitmap.FromFile(String.Concat("Images", System.IO.Path.DirectorySeparatorChar, "QuestionMark.png"));
                        }
                    }
                }
                new ToolTip().SetToolTip(itemImage, _itemData.Name);               
            }
        }

        public Tuple<int, int> Position
        {
            get
            {
                return new Tuple<int, int>(_x, _y);
            }
            set
            {
                _x = value.Item1;
                _y = value.Item2;
            }
        }
    }

}
