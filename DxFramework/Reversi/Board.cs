using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework.Reversi
{
    public class Board
    {
        private int[,] Elements;
        public Board Clone()
        {
            return (Board)MemberwiseClone();
        }
        public Board()
        {
            init();
        }
        public Board(Board board)
        {
            Elements = new int[8,8];
            copy(board);
        }
        public void copy(Board board)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    this.Elements[i, j] = board.getElement(i, j);
                }
        }
        private void clearElements()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Elements[i,j] = 0;
                }
            }
        }
        private void init()
        {
            Elements = new int[8,8];        
            clearElements();
        }
        public void setElement(Int3 ston)
        {
            setElement(ston.x, ston.y, ston.z);
        }
        public void setElement(int x, int y, int z)
        {
            Elements[x,y] = z;
        }
        public int getElement(Int2 pos)
        {
            return Elements[pos.x,pos.y];
        }
        public int getElement(int x, int y)
        {
            return Elements[x,y];
        }
        public void consolePrint()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(Elements[i,j]+" ");
                }
                Console.WriteLine();
            }
        }
        public int count(int num)
        {
            int count=0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Elements[i,j] == num) count++;
                }
            return count;
        }
        public int countElements()
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (Elements[i, j] != 0) count++;
                }
            return count;
        }
    }
}
