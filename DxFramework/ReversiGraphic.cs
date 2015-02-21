using DxFramework.Reversi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework
{
    class ReversiGraphic : Game
    {

        public ReversiGraphic()
        {
            top = new Vector2(50, 50);
            size = new Vector2(640, 640);
            Stones = new StoneGraphic[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Stones[i, j] = new StoneGraphic(new Int2((int)(Top.x + Size.x * i / 8 + Size.x / 16), (int)(Top.y + Size.y * j / 8 + Size.y / 16)));
                    Stones[i, j].layer = TopLayer;
                }
            back = new BoardGraphic();
            back.top = this.Top;
            back.size = this.Size;
            updateSton();
        }
        public new void init()
        {
            base.init(1);
            updateSton();
        }
        public void userInput()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Stones[i, j].isClickedOff())
                    {
                        put(new Int2(i, j));
                    }
                }
        }
        public override void put(Int2 ston)
        {
            base.put(ston);
            updateSton();
        }
        public override void pass()
        {
            base.pass();
            updateSton();
        }
        public override void undo()
        {
            base.undo();
            updateSton();
        }
        public StoneGraphic[,] Stones;
        private void setField()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    Stones[i, j].mid = new Vector2(Top.x + Size.x * i / 8 + Size.x / 16, Top.y + Size.y * j / 8 + Size.y / 16);
                    Stones[i, j].layer = TopLayer;
                }
            back.top = this.Top;
            back.size = this.Size;
            updateSton();

        }
        private void updateSton()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (this.board.getElement(i, j) == 1)
                    {
                        Stones[i, j].setBlack();
                    }
                    if (this.board.getElement(i, j) == -1)
                    {
                        Stones[i, j].setWhite();
                    }
                    if (this.board.getElement(i, j) == 0)
                    {
                        Stones[i, j].isVisible = false;
                    }
                }
            try
            {
                Stones[(int)PutLog[turnNumber - 1].x, (int)PutLog[turnNumber - 1].y].setLastPuted();
            }
            catch
            { }
            foreach (var itr in this.ablePosList)
            {
                if (this.turnPlayer == 1)
                {
                    Stones[itr.x, itr.y].setAbleBlack();
                }
                if (this.turnPlayer == -1)
                {
                    Stones[itr.x, itr.y].setAbleWhite();
                }
            }
        }
        private Vector2 top;
        private Vector2 size;
        public BoardGraphic back { get; private set; }
        public int TopLayer { get; set; }
        public Vector2 Top { get { return top; } set { top = value; setField(); } }
        public Vector2 Size { get { return size; } set { size = value; setField(); } }

    }

}