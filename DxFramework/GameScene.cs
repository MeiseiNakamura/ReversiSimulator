using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxFramework.Reversi;

namespace DxFramework
{
    class GameScene : Scene
    {

        public GameScene()
            : base()
        {
            init();
            instance = this;
        }
        public override void init()
        {
            var back = new Button(-5);
            back.color = DX.GetColor(0, 0, 50);
            back.mouseOnColor = back.color;
            back.top = new Vector2(0, 0);
            back.size = new Vector2(1600, 800);
           
            board = new ReversiGraphic();
            board.Top = new Vector2(5, 70);
 
            ai = new ArtificialIntelligence(board);
           
            umpire = new GameUmpire(board);
            umpire.gameMode = GameMode.PvP;
           
            UserBarGraphic bar = new UserBarGraphic(board, umpire);
            
            UserBoxGraphic box = new UserBoxGraphic(board,bar,ai,umpire);
            
            var head = new Button();
            head.color = DX.GetColor(230, 230, 250);
            head.mouseOnColor = head.color;
            head.top = new Vector2(0, 0);
            head.size = new Vector2(1600, 65);
            head.text = "Reversi Simulator";
            head.textFontHandle = DX.CreateFontToHandle("Papyrus", 18, 1);
            head.textPosition = new Vector2(10,10);

            var foot = new Button();
            foot.color = head.color;
            foot.mouseOnColor = foot.color;
            foot.top = new Vector2(0, bar.bottom.y + 5);
            foot.size = new Vector2(1600, 900 - bar.bottom.y - 5);
        }
        public override void update()
        {
            base.update();
            if (umpire.playerType == PlayerType.cpu)
            {
                base.draw();
                DX.ScreenFlip();
                ai.put();
                return;
            }
            if (umpire.playerType == PlayerType.user)
            {
                board.userInput();
                return;
            }
        }
        public static GameScene instance { get; private set; }
        private ReversiGraphic board { get; set; }
        public GameUmpire umpire { get; set; }
        private ArtificialIntelligence ai;
    }
}
