using System;
using System.Drawing;
using System.Windows.Forms;

namespace ValheimCharacterEditor.Controls
{
    public partial class Item_control : UserControl
    {
        ValheimEngine.Character.Item _itemData = null;
        int _x, _y;
        public event EventHandler<ItemControlClickEventArgs> ItemClicked;

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
                        new ToolTip().SetToolTip(itemImage, gameobject.DisplayName);
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
                        new ToolTip().SetToolTip(itemImage, _itemData.Name);
                    }
                }
                              
            }
        }

        private void itemImage_Click(object sender, EventArgs e)
        {
            ItemClicked.Invoke(this, new ItemControlClickEventArgs(_x, _y, _itemData));
            
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

    public class ItemControlClickEventArgs: EventArgs
    {
        private int _x,_y;
        private ValheimEngine.Character.Item _itemData;
        public int x { get { return _x; } }
        public int y { get { return _y; } }
        public ValheimEngine.Character.Item ItemData { get { return _itemData; } }
        public ItemControlClickEventArgs(int x, int y, ValheimEngine.Character.Item ItemData)
        {
            this._x = x;
            this._y = y;
            this._itemData = ItemData;
        }
    }

}
