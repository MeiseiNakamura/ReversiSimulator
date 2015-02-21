using DxFramework.Reversi;
using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework
{
    class UserBarGraphic : Button
    {
        private GameUmpire umpire;

        private ReversiGraphic board;
        private MultiGraphicButton backButton;
        private int FontHandle1;
        private Graphic turnButton;
        private MultiGraphicButton gotoButton;
        private int FontHandle2;
        public Vector2 Top { get { return new Vector2(board.Top.x, board.Top.y + board.Size.y + 2); } }
        public Vector2 Size { get { return new Vector2(board.Size.x, backButton.size.y); } }
        public UserBarGraphic(ReversiGraphic board, GameUmpire umpire)
        {
            this.board = board;
            this.umpire = umpire;
            this.top = board.Top + new Vector2(0, board.Size.y + 2);
            this.size = new Vector2(board.Size.x, 80);
            this.color = DX.GetColor(50,50,80);
            this.mouseOnColor = this.color;
            backButton = new MultiGraphicButton();
            backButton.AddGraph("resource/img/undo.png", "resource/img/undo_moused.png");
            backButton.AddGraph("resource/img/undo_null.png");
            backButton.GraphNumber = 1;
            backButton.top = Top;
            backButton.ClickedAction = () =>
            {
                this.board.undo();
                if (umpire.playerType == PlayerType.cpu && !(umpire.gameMode == GameMode.CvP && board.turnNumber == 1))
                { this.board.undo(); }
            };
            gotoButton = new MultiGraphicButton();
            gotoButton.AddGraph("resource/img/goto.png", "resource/img/goto_moused.png");
            gotoButton.AddGraph("resource/img/goto_null.png");
            gotoButton.GraphNumber = 1;
            gotoButton.bottom = Top + Size;
            gotoButton.ClickedAction = () =>
            {
                this.board.pass();
            };
            FontHandle1 = DX.CreateFontToHandle("", 36, 1, -1);
            FontHandle2 = DX.CreateFontToHandle("Miriam Fixed", 30, 1, -1);
            turnButton = new Graphic();
            turnButton.GraphName = "resource/img/turn.png";
            turnButton.top = new Vector2(Top.x + 265, Top.y);
        }
        public override void update()
        {
            base.update();
            if (board.turnNumber == 0 || (umpire.gameMode == GameMode.CvP && board.turnNumber == 1))
            {   
                backButton.GraphNumber = 1;
            }
            else
            {
                backButton.GraphNumber = 0;
            }
            if (board.condition == Condition.pass)
            { 
                gotoButton.GraphNumber = 0;
            }
            else
            {
                gotoButton.GraphNumber = 1;
            }
        }
        public override void draw()
        {
            base.draw();
            {
                DX.DrawStringToHandle((int)(Top.x + 165), (int)(Top.y + 22), "○", DX.GetColor(240, 240, 240), FontHandle1);
                DX.DrawStringToHandle((int)(Top.x + 210), (int)(Top.y + 25), "" + board.blackScore, DX.GetColor(240, 240, 240), FontHandle2);
            }
            {
                DX.DrawStringToHandle((int)(Top.x + 400), (int)(Top.y + 22), "●", DX.GetColor(240, 240, 240), FontHandle1);
                DX.DrawStringToHandle((int)(Top.x + 445), (int)(Top.y + 25), "" + board.whiteScore, DX.GetColor(240, 240, 240), FontHandle2);
            }
            {
                DX.DrawStringToHandle((int)(Top.x + 335), (int)(Top.y + 25), "" + (board.turnNumber + 1), DX.GetColor(240, 240, 240), FontHandle2);
            }
           
        }
    }
}
