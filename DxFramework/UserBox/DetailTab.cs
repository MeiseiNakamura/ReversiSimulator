using DxFramework.DxFrameWork;
using DxFramework.Reversi;
using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework
{
    class DetailTab:TabBase
    {
        public DetailTab(Game game, GameUmpire umpire, ArtificialIntelligence ai, Vector2 top, Vector2 size)
            : base(game, umpire, ai, top, size)
        {
            fontHandle1 = DX.CreateFontToHandle("メイリオ", 32, 2, DX.DX_FONTTYPE_ANTIALIASING);

            Text text1 = new Text();
            drawableList.Add(text1);
            text1.text = "完全版にご期待ください。";
            text1.FontHandle = fontHandle1;
            text1.top = this.Top + new Vector2(100, 100);
          
        }

        public int fontHandle1 { get; set; }

    }
}
