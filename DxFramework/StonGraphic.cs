using DxFramework.Reversi;
using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework
{
    class StoneGraphic:Button
    {
        public int BlackGraphHandle { get;private set; }
        public int WhiteGraphHandle { get;private set; }
        public int AbleBlackGraphHandle { get; private set; }
        public int AbleWhiteGraphHandle { get; private set; }
        public int Condition { get; private set; }
        public int Radius { get; set; }
        
        public StoneGraphic(Int2 Position) 
        {
            //BlackGraphHandle = DX.LoadGraph("");
            //WhiteGraphHandle = DX.LoadGraph("");
            //AbleBlackGraphHandle = DX.LoadGraph("");
            //AbleWhiteGraphHandle = DX.LoadGraph("");
            this.size = new Vector2(50, 50);
            this.mid =new Vector2(Position.x,Position.y);
            this.colorFlag = false;
            Radius = 26;
            isVisible = false;

        }
        public void setBlack()
        {
            this.isVisible = true;
            this.Condition = 1;
         //   this.GraphHandle = BlackGraphHandle;
        }
        public void setWhite()
        {
            this.isVisible = true;
            this.Condition = -1;
          //  this.GraphHandle = WhiteGraphHandle;
        }
        public void setAbleBlack()
        {
            this.isVisible = true;
            this.Condition = 10;
           // this.GraphHandle = BlackGraphHandle;
        }
        public void setAbleWhite()
        {
            this.isVisible = true;
            this.Condition = -10;
          //  this.GraphHandle = WhiteGraphHandle;
        }
        public void setLastPuted()
        {
            this.Condition *= 100;
        }
        public override void draw()
        {
            base.draw();
            switch (Condition)
            {case 1:
                    DX.DrawCircle((int)this.mid.x, (int)this.mid.y, this.Radius, DX.GetColor(30, 30, 30), DX.TRUE);
                    break;
                case -1:
                    DX.DrawCircle((int)this.mid.x, (int)this.mid.y, this.Radius, DX.GetColor(240, 240, 240), DX.TRUE);
                    break;
                case 10:
                    if (isMoused())
                    {
                        DX.DrawCircle((int)this.mid.x, (int)this.mid.y, this.Radius, DX.GetColor(30, 75, 40), DX.TRUE);
                    }
                    else
                    {
                        DX.DrawCircle((int)this.mid.x, (int)this.mid.y, this.Radius, DX.GetColor(30, 160, 70), DX.TRUE);
                    }
                    break;
                case -10:
                    if (isMoused())
                    {
                        DX.DrawCircle((int)this.mid.x, (int)this.mid.y, this.Radius, DX.GetColor(180, 240, 180), DX.TRUE);
                    }
                    else
                    {
                        DX.DrawCircle((int)this.mid.x, (int)this.mid.y, this.Radius, DX.GetColor(20, 220, 120), DX.TRUE);
                    }
                    break;
                case 100:
                    DX.DrawCircle((int)this.mid.x, (int)this.mid.y, this.Radius + 4, DX.GetColor(220, 220, 80), DX.TRUE);
                    DX.DrawCircle((int)this.mid.x, (int)this.mid.y, this.Radius, DX.GetColor(30, 30, 30), DX.TRUE);
                   
                    break;
                case -100:
                    DX.DrawCircle((int)this.mid.x, (int)this.mid.y, this.Radius + 4, DX.GetColor(220, 220, 80), DX.TRUE);
                    DX.DrawCircle((int)this.mid.x, (int)this.mid.y, this.Radius, DX.GetColor(240, 240, 240), DX.TRUE);
                  
                    break;
            }
        }
    }
}
