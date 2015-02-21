using DxFramework.Reversi;
using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework
{
    class UserBoxGraphic:Button
    {
        private MultiGraphicButton tabButton1;
        private MultiGraphicButton tabButton2;
        private MultiGraphicButton tabButton3;
        public UserBoxGraphic(ReversiGraphic board,UserBarGraphic bar,ArtificialIntelligence ai,GameUmpire umpire) 
        {
           
            this.color = DX.GetColor(255, 255, 255);
            this.mouseOnColor = this.color;
            this.top = new Vector2(board.Top.x + board.Size.x + 5, 70);
            this.size = new Vector2(1600 - board.Top.x - board.Size.x, board.Size.y + bar.size.y + 2);

            var fontHandle1 = DX.CreateFontToHandle("メイリオ", 16, 1, DX.DX_FONTTYPE_ANTIALIASING);
 
            var Handle1 = DX.MakeGraph(150, 25);
            DX.DrawBox(0, 0, 150, 30, DX.GetColor(255, 230, 110), DX.TRUE);
            DX.GetDrawScreenGraph(0, 0, 150, 25,Handle1);
            DX.ClearDrawScreen();
            
            var Handle2 = DX.MakeGraph(150, 25);
            DX.DrawBox(0, 0, 150, 25, bar.color, DX.TRUE);
            DX.GetDrawScreenGraph(0, 0, 150, 25, Handle2);
            DX.ClearDrawScreen();
            
            var Handle3 = DX.MakeGraph(150, 25);
            DX.DrawBox(0, 0, 150, 25,DX.GetColor(90,90,120), DX.TRUE);
            DX.GetDrawScreenGraph(0, 0, 150, 25, Handle3);
            DX.ClearDrawScreen();

            tabButton1 = new MultiGraphicButton(1);
            tabButton2 = new MultiGraphicButton(1);
            tabButton3 = new MultiGraphicButton(1);
            tabButton1.AddGraph(Handle1);
            tabButton2.AddGraph(Handle1);
            tabButton3.AddGraph(Handle1);
            tabButton1.AddGraph(Handle2, Handle3);
            tabButton2.AddGraph(Handle2, Handle3);
            tabButton3.AddGraph(Handle2, Handle3);
            tabButton1.top = this.top + new Vector2(1, 1);
            tabButton2.top = tabButton1.top + new Vector2(151, 0);
            tabButton3.top = tabButton2.top + new Vector2(151, 0);
            tabButton1.GraphNumber = 0;
            tabButton2.GraphNumber = 1;
            tabButton3.GraphNumber = 1;
            tabButton1.textFontHandle = fontHandle1;
            tabButton1.text = "一般";
            tabButton1.textPosition = new Vector2(2, 2);
            tabButton1.textColor = DX.GetColor(0, 0, 0);
            tabButton2.textFontHandle = fontHandle1;
            tabButton2.text = "詳細";
            tabButton2.textPosition = new Vector2(2, 2);
            tabButton2.textColor = DX.GetColor(250, 250, 250);
            tabButton3.textFontHandle = fontHandle1;
            tabButton3.text = "あいさつ";
            tabButton3.textPosition = new Vector2(2, 2);
            tabButton3.textColor = DX.GetColor(250, 250, 250);
            
            var lin = new Button(2);
            lin.top = new Vector2(tabButton1.top.x, tabButton1.bottom.y - 2);
            lin.size = new Vector2(this.size.x, 3);
            lin.color = DX.GetColor(255, 230, 110);
            lin.mouseOnColor = lin.color;
            
            var generalTab = new GeneralTab(board, umpire, ai, new Vector2(this.top.x, lin.bottom.y), this.bottom - lin.top);
            var detailTab = new DetailTab(board, umpire, ai, new Vector2(this.top.x, lin.bottom.y), this.bottom - lin.top);
            var aisatsuTab = new AisatsuTab(board, umpire, ai, new Vector2(this.top.x, lin.bottom.y), this.bottom - lin.top);
            generalTab.Selected = true;
            detailTab.Selected = false;
            aisatsuTab.Selected = false;
            tabButton1.ClickedAction = () =>
            {
                tabButton1.GraphNumber = 0;
                tabButton1.textColor = DX.GetColor(0, 0, 0);
                tabButton3.GraphNumber = 1;
                tabButton3.textColor = DX.GetColor(250, 250, 250);
                tabButton2.GraphNumber = 1;
                tabButton2.textColor = DX.GetColor(250, 250, 250);
                generalTab.Selected = true;
                detailTab.Selected = false;
                aisatsuTab.Selected = false;
            };
            tabButton2.ClickedAction = () =>
            {
                tabButton2.GraphNumber = 0;
                tabButton2.textColor = DX.GetColor(0, 0, 0);
                tabButton1.GraphNumber = 1;
                tabButton1.textColor = DX.GetColor(250, 250, 250);
                tabButton3.GraphNumber = 1;
                tabButton3.textColor = DX.GetColor(250, 250, 250);
                generalTab.Selected = false;
                detailTab.Selected = true;
                aisatsuTab.Selected = false;
            };
            tabButton3.ClickedAction = () =>
            {
                tabButton2.GraphNumber = 1;
                tabButton2.textColor = DX.GetColor(250, 250, 250);
                tabButton1.GraphNumber = 1;
                tabButton1.textColor = DX.GetColor(250, 250, 250);
                tabButton3.GraphNumber = 0;
                tabButton3.textColor = DX.GetColor(0, 0, 0);
                generalTab.Selected = false;
                detailTab.Selected = false;
                aisatsuTab.Selected = true;

            };
          
            var tabEnd = new Button(1);
            tabEnd.top = new Vector2(tabButton3.bottom.x+1, this.top.y);
            tabEnd.size = new Vector2(this.bottom.x-tabButton2.bottom.x,tabButton1.size.y );
            tabEnd.color = DX.GetColor(0, 0, 50);
            tabEnd.mouseOnColor = tabEnd.color;
          
        }
       
    }
}
