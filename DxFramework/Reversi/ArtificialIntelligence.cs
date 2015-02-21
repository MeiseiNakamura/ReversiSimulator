using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework.Reversi
{
    class ArtificialIntelligence
    {
        private Game game;
        private Game GameCopy;
        public int finalLookTurn { get; set; }
        public int lookNum { get; set; }
        public bool Revflage { get; set; }
        public ArtificialIntelligence(Game reversi)
        {
            this.GameCopy = new Game();
            this.Revflage = false;
            this.game = reversi;
            lookNum = 4;
            finalLookTurn = 46;
        }
        public void put()
        {
            if (game.condition == Condition.pass)
            {
                game.pass();
                return;
            }
            this.GameCopy.copy(game);
            ILookaheadBase look;
            if (Revflage)
            {
                if (game.turnNumber > finalLookTurn)
                {
                    look = new FinalLookaheadRev();
                }
                else
                {
                    look = new BasicLookaheadRev(lookNum);
                }
            }
            else
            {
                if (game.turnNumber > finalLookTurn)
                {
                    look = new FinalLookahead();
                }
                else
                {
                    look = new BasicLookahead(lookNum);
                }
            }
            game.put(look.lookahead(GameCopy));
        }
    }
}
