using DxFramework.DxFrameWork;
using DxFramework.Reversi;
using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DxFramework
{
    class GeneralTab : TabBase
    {
        private int fontHandle1;
        private int fontHandle2;
        public GeneralTab(ReversiGraphic game, GameUmpire umpire, ArtificialIntelligence ai, Vector2 top, Vector2 size)
            : base(game, umpire, ai, top, size)
        {

            fontHandle1 = DX.CreateFontToHandle("メイリオ", 12, 1, DX.DX_FONTTYPE_ANTIALIASING);
            fontHandle2 = DX.CreateFontToHandle("メイリオ", 24, 1, DX.DX_FONTTYPE_ANTIALIASING);
           
            Text text1 = new Text();
            drawableList.Add(text1);
            text1.text = "各種設定";
            text1.FontHandle = fontHandle2;
            text1.top = this.Top + new Vector2(15, 15);

            Text stext1 = new Text();
            drawableList.Add(stext1);
            stext1.text = "譜面をリセットし、新しいゲームを開始します。";
            stext1.FontHandle = fontHandle1;
            stext1.top = this.Top + new Vector2(17, 85);
       
            var init = new Button(2);
            drawableList.Add(init);
            init.top = this.Top + new Vector2(17, 110);
            init.size = new Vector2(100, 30);
            init.text = "リセット";
            init.textFontHandle = fontHandle1;
            init.textPosition = new Vector2(25, 9);
            init.ClickedAction = () =>
            { game.init();};

            Text stext2 = new Text();
            drawableList.Add(stext2);
            stext2.text = "対戦モードを変更します。";
            stext2.FontHandle = fontHandle1;
            stext2.top = this.Top + new Vector2(17, 195);
            
            var checkBox1 = new CheckBox(3,this.Top + new Vector2(17, 240),2);
            umpire.gameMode = GameMode.PvC;
            //drawableList.Add(checkBox1.adaptButton);
            foreach (var itr in checkBox1.CheckButtonList)
            {
                drawableList.Add(itr);
            }
            checkBox1.setText (1, "プレーヤー対戦");
            checkBox1.setText(2, "CPUと対戦（あなたは黒番）");
            checkBox1.setText(3, "CPUと対戦（あなたは白番）");
           
            checkBox1.adaptedActionList[0] = () =>
            {
                umpire.gameMode = GameMode.PvP;
            };
            checkBox1.adaptedActionList[1] = () =>
            {
                umpire.gameMode = GameMode.PvC;
            };
            checkBox1.adaptedActionList[2] = () =>
            {
                umpire.gameMode = GameMode.CvP;
            };
          
            //Text text2 = new Text();
            //drawableList.Add(text2);
            //text2.text = "AI設定";
            //text2.FontHandle = fontHandle2;
            //text2.top = this.Top + new Vector2(315, 15);

            Text stext3 = new Text();
            drawableList.Add(stext3);
            stext3.text = "AIの読み方を変更します。";
            stext3.FontHandle = fontHandle1;
            stext3.top = this.Top + new Vector2(617, 195);

            var checkBox2 = new CheckBox(2, this.Top + new Vector2(617, 270), 1);
            //checkBox2.adaptButtonSpan = 40;
            //drawableList.Add(checkBox2.adaptButton);
            foreach (var itr in checkBox2.CheckButtonList)
            {
                drawableList.Add(itr);
            }
            checkBox2.setText(1, "勝ち読み（勝ちをめざします。）");
            checkBox2.setText(2, "負け読み（負けをめざします。）");
            checkBox2.adaptedActionList[0] = () =>
            {
                ai.Revflage = false;
            };
            checkBox2.adaptedActionList[1] = () =>
            {
                ai.Revflage = true;
            };

            Text stext4 = new Text();
            drawableList.Add(stext4);
            stext4.text = "AIの強さを変更します。";
            stext4.FontHandle = fontHandle1;
            stext4.top = this.Top + new Vector2(317, 195);

            var checkBox3 = new CheckBox(3,this.Top + new Vector2(317, 240),2);
            //drawableList.Add(checkBox3.adaptButton);
            foreach (var itr in checkBox3.CheckButtonList)
            {
                drawableList.Add(itr);
            }
            checkBox3.setText (1, "めっちょ強い");
            checkBox3.setText(2, "強い");
            checkBox3.setText(3, "普通");
            checkBox3.adaptedActionList[0] = () =>
            {
                ai.finalLookTurn = 44;
                ai.lookNum = 5;
            };
            checkBox3.adaptedActionList[1] = () =>
            {
                ai.finalLookTurn = 46;
                ai.lookNum = 4;
            };
            checkBox3.adaptedActionList[2] = () =>
            {
                ai.finalLookTurn = 50;
                ai.lookNum = 2;
            };

            Text text5 = new Text();
            drawableList.Add(text5);
            text5.text = "現在の状態";
            text5.FontHandle = fontHandle2;
            text5.top = this.Top + new Vector2(15, 500);

            var Handle1 = DX.MakeGraph(900, 100);
            DX.DrawBox(0, 0, 900, 100, DX.GetColor(255, 255, 255), DX.TRUE);
            DX.DrawBox(0, 0, 900, 100, DX.GetColor(0 ,0, 0), DX.FALSE);
            DX.GetDrawScreenGraph(0, 0, 900, 100, Handle1);
            DX.ClearDrawScreen();

            var button5 = new Button();
            drawableList.Add(button5);
            button5.GraphHandle = Handle1;
            button5.TLightFlag = false;
            button5.top = this.Top + new Vector2(15,570);

            var text6 = new Text();
            drawableList.Add(text6);
            text6.FontHandle = fontHandle1;
            text6.top = button5.top + new Vector2(5,10);
            var text7 = new Text();
            drawableList.Add(text7);
            text7.FontHandle = fontHandle1;
            text7.top = button5.top + new Vector2(5, 35);

            text6.updateAction = () =>
            {
                text6.text = "";
                text7.text = "";
                if (umpire.playerType == PlayerType.cpu) 
                {
                    text6.text = "ＣＰＵが考えています。";
                }
                if (umpire.playerType == PlayerType.user)
                {
                    if (umpire.gameMode == GameMode.PvP)
                    {
                        if(game.turnPlayer==1)
                           text6.text = "黒番です。";
                        if (game.turnPlayer == -1)
                            text6.text = "白番です。";
                    }
                    else text6.text = "あなたの番です。";
                    if (game.condition == Condition.pass)
                    {
                        text7.text = "パスとなります。進むボタンを押してください。";
                       
                    }
                }
                if (umpire.playerType == PlayerType.non)
                {
                    if (game.condition == Condition.end)
                    {
                        if (umpire.gameMode == GameMode.PvP)
                        {
                            if (game.blackScore > game.whiteScore)
                                text6.text = "黒手の勝ちです。";
                            if (game.blackScore < game.whiteScore)
                                text6.text = "白手の勝ちです。";
                            if (game.blackScore == game.whiteScore)
                                text6.text = "引き分けです。";
                        }
                        if (umpire.gameMode == GameMode.CvP)
                        {
                            if (game.blackScore > game.whiteScore)
                                text6.text = "残念ですが、あなたの負けです。";
                            if (game.blackScore < game.whiteScore)
                                text6.text = "おめでとう！あなたの勝ちです！";
                            if (game.blackScore == game.whiteScore)
                                text6.text = "引き分けです。";
                        }
                        if (umpire.gameMode == GameMode.PvC)
                        {
                            if (game.blackScore > game.whiteScore)
                                text6.text = "おめでとう！あなたの勝ちです！";
                            if (game.blackScore < game.whiteScore)
                                text6.text = "残念ですが、あなたの負けです。";
                            if (game.blackScore == game.whiteScore)
                                text6.text = "引き分けです。";
                        }

                    }
                }
            };


        }

    }
      
}
