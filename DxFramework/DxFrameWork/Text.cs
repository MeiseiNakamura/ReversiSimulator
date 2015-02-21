using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework.DxFrameWork
{
    class Text:DrawableBase
    {
        public Vector2 top { get; set; }
        public int FontHandle
        {
            get { return _FontHandle; }
            set
            {
                updateAction=()=>{};
                fontFlage = true;
                _FontHandle = value;
            }
        }
        public int color { get; set; }
        public string text;
        private int _FontHandle;
        private bool fontFlage;
        public Text() : base(0) { top = new Vector2(0, 0); }
        public Text(int layer) : base(layer) { top = new Vector2(0, 0); }
        public override void draw()
        {
            if (fontFlage) { DX.DrawStringToHandle((int)top.x, (int)top.y, text, color, FontHandle); }
            else DX.DrawString((int)top.x , (int)top.y, text, color);
        }
        public override void update()
        {
            updateAction();
        }
        public Action updateAction { get; set; }
    }
}
