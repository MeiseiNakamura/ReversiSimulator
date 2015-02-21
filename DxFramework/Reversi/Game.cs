using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework.Reversi
{
    public enum Condition { wait, pass, end }
    public class Game
    {
        private Board[] BoardList = new Board[100];
        protected Int2[] PutLog = new Int2[100];
        public int BlackAbleNum{
            get
            {
                if (turnPlayer == 1)
                {
                    return ablePosList.Count;
                }
                else 
                {
                    int count = 0;
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (isAblePos(new Int3(i, j, 1)))
                            {
                                count++;
                            }
                        }
                    return count;
                }
            }
        }
        public int WhiteAbleNum
        {
            get
            {
                if (turnPlayer == -1)
                {
                    return ablePosList.Count;
                }
                else
                {
                    int count = 0;
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            if (isAblePos(new Int3(i, j, -1)))
                            {
                                count++;
                            }
                        }
                    return count;
                }
            }
        }
        private static Vector2[] dir = 
        { new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1), new Vector2(-1, 0), 
          new Vector2(0, -1), new Vector2(-1, -1), new Vector2(1, -1), new Vector2(-1, 1) };
        public List<Int2> ablePosList;
        public Condition condition { get; private set; }
        public int firstTurnPlayer { get; private set; }
        public int turnPlayer
        {
            get
            {
                if (turnNumber % 2 == 0)
                { return firstTurnPlayer; }
                else
                { return -firstTurnPlayer; }
            }
        }
        public int turnNumber { get; private set; }
        public int whiteScore { get { return board.count(-1); } }
        public int blackScore { get { return board.count(1); } }
        public Board board { get { return BoardList[turnNumber]; } }
        public Game()
        {
            init(1);
        }
        public virtual void init(int firstTurnPlayer)
        {
            turnNumber = 0;
            this.firstTurnPlayer = firstTurnPlayer;
            for (int i = 0; i < 100; i++)
            {
                BoardList[i] = new Board();
            }
            ablePosList = new List<Int2>();
            BoardList[0].setElement(new Int3(3, 3, 1));
            BoardList[0].setElement(new Int3(3, 4, -1));
            BoardList[0].setElement(new Int3(4, 3, -1));
            BoardList[0].setElement(new Int3(4, 4, 1));
            setAblePos();

        }
        public virtual void put(Int3 stone)
        {
            if (BoardList[turnNumber].getElement(stone.x, stone.y) != 0) return;
            bool putFlag = false;
            BoardList[turnNumber + 1].copy(BoardList[turnNumber]);
            for (int i = 0; i < 8; i++)
            {
                if (stone.x + dir[i].x < 0 || stone.x + dir[i].x > 7 || stone.y + dir[i].y < 0 || stone.y + dir[i].y > 7) { continue; }
                if (BoardList[turnNumber].getElement(stone.x + (int)dir[i].x, stone.y + (int)dir[i].y) == -stone.z)
                {
                    int count = 1;
                    while (true)
                    {
                        count++;
                        if (stone.x + dir[i].x * count < 0 || stone.x + dir[i].x * count > 7 || stone.y + dir[i].y * count < 0 || stone.y + dir[i].y * count > 7) { break; }
                        int color = BoardList[turnNumber].getElement(stone.x + (int)dir[i].x * count, stone.y + (int)dir[i].y * count);
                        if (color == 0) { break; }
                        if (color == stone.z)
                        {
                            putFlag = true;
                            for (; count >= 1; count--)
                            {
                                BoardList[turnNumber + 1].setElement(stone.x + (int)dir[i].x * count, stone.y + (int)dir[i].y * count, stone.z);
                            }
                            break;
                        }
                    }
                }
            }
            if (putFlag)
            {
                BoardList[turnNumber + 1].setElement(stone);
                PutLog[turnNumber] = new Int2(stone.x, stone.y);
                this.goNextTurn();
            }
        }
        public virtual void put(Int2 stone)
        { put(new Int3(stone, this.turnPlayer)); }
        public virtual void put(int num)
        {
            try
            {
                put(new Int3(ablePosList[num - 1], turnPlayer));
            }
            catch { }
        }
        public virtual void undo()
        {
            if (this.turnNumber == 0) return;
            this.turnNumber--;
            this.setAblePos();

        }
        public virtual void pass()
        {
            if (condition == Condition.pass)
            {
                BoardList[turnNumber + 1].copy(board);
                goNextTurn();
            }
        }
        public void copy(Game rev)
        {
            init(rev.firstTurnPlayer);
            for (int i = 0; i < rev.turnNumber; i++)
            {
                if (this.condition==Condition.pass)
                { this.pass(); continue; }
                else if(this.condition==Condition.wait)
                {
                    this.put(rev.PutLog[i]);
                }
            }
        }
        private void setAblePos(int player)
        {
            ablePosList.Clear();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (isAblePos(new Int3(i, j, player)))
                    {
                        ablePosList.Add(new Int2(i, j));
                    }
                }
            if (ablePosList.Count == 0)
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        if (isAblePos(new Int3(i, j, -player)))
                        {
                            condition = Condition.pass;
                            return;
                        }
                    }
                condition = Condition.end;
                return;
            }
            condition = Condition.wait;
        }
        private void setAblePos()
        {
            setAblePos(turnPlayer);
        }
        private bool isAblePos(Int3 stone)
        {
            return isAblePos(this.board, stone);
        }
        public static bool isAblePos(Board board, Int3 stone)
        {
            if (board.getElement(stone.x, stone.y) != 0) return false;
            for (int i = 0; i < 8; i++)
            {
                if (stone.x + dir[i].x < 0 || stone.x + dir[i].x > 7 || stone.y + dir[i].y < 0 || stone.y + dir[i].y > 7) { continue; }
                if (board.getElement(stone.x + (int)dir[i].x, stone.y + (int)dir[i].y) == -stone.z)
                {
                    int count = 1;
                    while (true)
                    {
                        count++;
                        if (stone.x + dir[i].x * count < 0 || stone.x + dir[i].x * count > 7 || stone.y + dir[i].y * count < 0 || stone.y + dir[i].y * count > 7) { break; }
                        int color = board.getElement(stone.x + (int)dir[i].x * count, stone.y + (int)dir[i].y * count);
                        if (color == 0) { break; }
                        if (color == stone.z)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private void goNextTurn()
        {
            this.turnNumber++;
            this.setAblePos();
        }
    }
}
