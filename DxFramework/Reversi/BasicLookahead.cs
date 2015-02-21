using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework.Reversi
{
    class BasicLookahead : ILookaheadBase
    {
        private Game game;
        private Board board;
        public int AheadNumber { get; set; }
        public BasicLookahead(int aheadnum)
        {
            AheadNumber = aheadnum;
        }
        public Int2 lookahead(Game game)
        {
            this.game = game;
            this.board=new Board();
            List<int> scoreList = new List<int>();
            for (int i = 0; i < game.ablePosList.Count; i++)
            {
                scoreList.Add(0);
                game.put(game.ablePosList[i]);
                switch (game.condition)
                {
                    case Condition.wait:
                        scoreList[i] = algorithm(AheadNumber-1, -10000, 10000);
                        break;
                    case Condition.pass:
                        game.pass();
                        scoreList[i] = algorithm(AheadNumber-1, -10000, 10000);
                        game.undo();
                        break;
                    case Condition.end:
                        scoreList[i] = (game.blackScore - game.whiteScore) * 100;
                        break;
                }
                game.undo();
            }
            if (game.turnPlayer == 1)
            {
                for (int i = 0; i < scoreList.Count; i++)
                {
                    if (scoreList[i] == scoreList.Max())
                    {
                        return game.ablePosList[i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < scoreList.Count; i++)
                {
                    if (scoreList[i] == scoreList.Min())
                    {
                        return game.ablePosList[i];
                    }
                }
            }
            return game.ablePosList[0];
        }
        private int algorithm(int depth, int alfa, int beta)
        {
            if (depth == 0)
            {
                return judge();
            }
            if (game.turnPlayer == 1)
            {
                for (int i = 0; i < game.ablePosList.Count;i++ )
                {

                    game.put(game.ablePosList[i]);
                    switch (game.condition)
                    {
                        case Condition.wait:
                            alfa = Math.Max(alfa, algorithm(depth-1, alfa, beta));
                            break;
                        case Condition.pass:
                            game.pass();
                            // player -1のターン
                            alfa = Math.Max(alfa, algorithm(depth-1, alfa, beta));
                            game.undo();
                            break;
                        case Condition.end:
                            alfa = (game.blackScore - game.whiteScore) * 100;
                            break;
                    }
                    game.undo();
                    if (alfa >= beta)
                    {
                        return beta;
                    }
                }
                return alfa;
            }
            else
            {
                for (int i = 0; i < game.ablePosList.Count; i++)
                {
                    game.put(game.ablePosList[i]);
                    switch (game.condition)
                    {
                        case Condition.wait:
                            beta = Math.Min(beta, algorithm(depth - 1, alfa, beta));
                            break;
                        case Condition.pass:
                            game.pass();
                            beta = Math.Min(beta, algorithm(depth-1, alfa, beta));
                            game.undo();
                            break;
                        case Condition.end:
                            beta = (game.blackScore - game.whiteScore) * 100;
                            break;
                    }
                    game.undo();
                    if (alfa >= beta)
                    {
                        return alfa;
                    }
                }
                return beta;
            }
        }
        private int judge()
        {
            int score;
            score = judgeQ(game.board);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    board.setElement(i, 7 - j, game.board.getElement(i, j));
                }
            score += judgeQ(board);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    board.setElement(7 - i, j, game.board.getElement(i, j));
                }
            score += judgeQ(board);
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    board.setElement(7 - i, 7 - j, game.board.getElement(i, j));
                }
            score += judgeQ(board);
            score += game.BlackAbleNum;
            score -= game.WhiteAbleNum;
            score -= game.blackScore / 2;
            score += game.whiteScore / 2;
            return score;
        }
        private int judgeQ(Board board)
        {
            int score = 0;
            if (board.getElement(0, 0) == 1)
            {
                score += 100;
                if (!isStop(board, 1))
                {
                    score -= 50;
                }
            }
            else if (board.getElement(0, 0) == -1)
            {
                score -= 100;
                if (!isStop(board, -1))
                {
                    score += 50;
                }
            }
            else
            {
                score -= board.getElement(1, 1) * 100;
                if (Game.isAblePos(board, new Int3(0, 0, 1)))
                {
                    score += 100;
                }
                if (Game.isAblePos(board, new Int3(0, 0, -1)))
                {
                    score -= 100;
                }
            }
            return score;
        }
        bool isStop(Board board, int player)
        {
            int count = 1;
            int check = player;
            while (check == player && count < 8)
            {
                check = board.getElement(0, count);
                count++;
            }
            if (check == -player)
            {
                while (check == -player && count < 8)
                {
                    check = board.getElement(0, count);
                    count++;
                }
                if (check == player && count != 7)
                {
                    count = 1;
                    check = player;
                    while (check == player && count < 8)
                    {
                        check = board.getElement(count, 0);
                        count++;
                    }
                    if (check == -player)
                    {
                        while (check == -player && count < 8)
                        {
                            check = board.getElement(count, 0);
                            count++;
                        }
                        if (check == player && count != 7)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
