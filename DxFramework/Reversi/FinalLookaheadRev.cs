using DxFramework.Reversi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DxFramework.Reversi
{
    class FinalLookaheadRev : ILookaheadBase
    {
        public FinalLookaheadRev()
        {
        }
        private Game game;
        public Int2 lookahead(Game game)
        {
            this.game = game;
            List<int> scoreList = new List<int>();
            for (int i = 0; i < game.ablePosList.Count; i++)
            {
                scoreList.Add(0);
                game.put(game.ablePosList[i]);
                switch (game.condition)
                {
                    case Condition.wait:
                        scoreList[i] = algorithm(-10000, 10000);
                        break;
                    case Condition.pass:
                        game.pass();
                        scoreList[i] = algorithm(-10000, 10000);
                        game.undo();
                        break;
                    case Condition.end:
                        scoreList[i] = -(game.blackScore - game.whiteScore) * 100;//
                        break;
                }
                game.undo();
                if (scoreList[i] > 0 && game.turnPlayer == 1 || scoreList[i] < 0 && game.turnPlayer == -1)
                {
                    break;
                }
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

        private int algorithm(int alfa, int beta)
        {
            if (game.turnPlayer == 1)
            {
                for (int i = 0; i < game.ablePosList.Count; i++)
                {

                    game.put(game.ablePosList[i]);
                    switch (game.condition)
                    {
                        case Condition.wait:
                            alfa = Math.Max(alfa, algorithm(alfa, beta));
                            break;
                        case Condition.pass:
                            game.pass();
                            alfa = Math.Max(alfa, algorithm(alfa, beta));
                            game.undo();
                            break;
                        case Condition.end:
                            alfa = -(game.blackScore - game.whiteScore);//
                            break;
                    }
                    game.undo();
                    if (alfa >= beta)
                    {
                        return beta;
                    }
                    if (alfa > 0)
                    {
                        return alfa;
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
                            beta = Math.Min(beta, algorithm(alfa, beta));
                            break;
                        case Condition.pass:
                            game.pass();
                            beta = Math.Min(beta, algorithm(alfa, beta));
                            game.undo();
                            break;
                        case Condition.end:
                            beta = -(game.blackScore - game.whiteScore);//
                            break;
                    }
                    game.undo();
                    if (alfa >= beta)
                    {
                        return alfa;
                    }
                    if (beta < 0)
                    {
                        return beta;
                    }
                }
                return beta;
            }
        }
    }
}
