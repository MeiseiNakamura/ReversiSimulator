using DxFramework.Reversi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework
{
    abstract class TabBase
    {
        protected Game game;
        protected GameUmpire umpire;
        protected ArtificialIntelligence ai;
        protected bool _selected;
        public Vector2 Top { get; set; }
        public Vector2 Size { get; set; }
        protected List<DrawableBase> drawableList;
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                if (Selected)
                { visualize(); }
                else
                { hide(); }
            }
        }

        public TabBase(Game game, GameUmpire umpire, ArtificialIntelligence ai, Vector2 top, Vector2 size)
        {
            drawableList = new List<DrawableBase>();
            this.game = game;
            this.umpire = umpire;
            this.ai = ai;
            this.Top = top;
            this.Size = size;
        }
        protected void visualize()
        {
            foreach (var itr in drawableList)
            {
                itr.isVisible = true;
            }
        }

        protected void hide()
        {
            foreach (var itr in drawableList)
            {
                itr.isVisible = false;
            }
        }
    }
}
