using DxFramework.Reversi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework
{
    enum GameMode { PvP, PvC, CvP }
    enum PlayerType { non, user, cpu }
    class GameUmpire
    {
        private Game game;
        private GameMode _gameMode;
        private PlayerType _playerType;
        public GameMode gameMode
        {
            get
            {
                return _gameMode;
            }
            set { this._gameMode = value; }
        }
        public PlayerType playerType
        {
            get
            {
                if (game.condition == Condition.end)
                {
                     _playerType = PlayerType.non;
                }
                else
                {
                    if ((gameMode == GameMode.CvP && game.turnPlayer == 1) || (gameMode == GameMode.PvC && game.turnPlayer == -1))
                    {
                        _playerType = PlayerType.cpu;
                    }
                    else _playerType = PlayerType.user;
                }
                return _playerType;
            }
        }

        public GameUmpire(Game game)
        {
            this.game = game;
            this.gameMode = GameMode.PvP;
        }

    }
}
