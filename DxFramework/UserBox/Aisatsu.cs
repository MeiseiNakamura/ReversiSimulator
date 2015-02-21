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
    class AisatsuTab:TabBase
    {
          public AisatsuTab(Game game, GameUmpire umpire, ArtificialIntelligence ai, Vector2 top, Vector2 size)
            : base(game, umpire, ai, top, size)
        {
           var fontHandle1 = DX.CreateFontToHandle("メイリオ", 54, 2, DX.DX_FONTTYPE_ANTIALIASING);
           var fontHandle2 = DX.CreateFontToHandle("メイリオ", 24, 2, DX.DX_FONTTYPE_ANTIALIASING);
           var fontHandle3 = DX.CreateFontToHandle("メイリオ", 14, 2, DX.DX_FONTTYPE_ANTIALIASING);

            Text text1 = new Text();
            drawableList.Add(text1);
            text1.text = "理大祭へようこそ。";
            text1.FontHandle = fontHandle1;
            text1.top = this.Top + new Vector2(250, 30);

            Text text2 = new Text();
            drawableList.Add(text2);
            text2.text = "この度は作品を見ていただきありがとうございます。";
            text2.FontHandle = fontHandle2;
            text2.top = this.Top + new Vector2(15, 150);

            Text text3 = new Text();
            drawableList.Add(text3);
            text3.text = "ここでは、このオセロＡＩのアルゴリズムについて解説したいと思います。";
            text3.FontHandle = fontHandle2;
            text3.top = this.Top + new Vector2(15, 200);

            Text text4 = new Text();
            drawableList.Add(text4);
            text4.text = "...と思ったのですが、余白がたりないようなので諦めます（笑）";
            text4.FontHandle = fontHandle2;
            text4.top = this.Top + new Vector2(15, 350);

            var button1 = new Button();
            drawableList.Add(button1);
            button1.top = this.Top + new Vector2(500, 400);
            button1.color = DX.GetColor(0,150,250);
            button1.mouseOnColor = DX.GetColor(20,170,255);
            button1.size = new Vector2(100,30);
            button1.textFontHandle = fontHandle3;
            button1.text = "いいね!";
            button1.textPosition = new Vector2(25,9);
            
            Text text5 = new Text();
            drawableList.Add(text5);
            text5.text = "" + button1.clickedTimes;
            text5.FontHandle = fontHandle3;
            text5.top = button1.top + new Vector2(-30-(button1.clickedTimes.ToString().Length) * 10, +9);
            button1.ClickedAction = () =>
            {
                text5.text = "" + button1.clickedTimes;
                text5.top = button1.top + new Vector2(-30 - (button1.clickedTimes.ToString().Length)*10, +9);
            };

        }

       
    }
}
