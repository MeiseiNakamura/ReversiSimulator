using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework
{
    class BoardGraphic:Button
    {
        public BoardGraphic():base(-1)
        {
            this.color=DX.GetColor(10,200,100);
            this.mouseOnColor = this.color;
        }
        public override void draw()
        {
            base.draw();
            for(int i=0;i<=8;i++)
            {
                DX.DrawLine((int)(top.x+i*size.x/8), (int)top.y,(int)( top.x+i*size.x/8), (int)bottom.y, DX.GetColor(0, 0, 0));
                DX.DrawLine((int)top.x, (int)(top.y + i * size.y / 8), (int)bottom.x, (int)(top.y + i * size.y / 8), DX.GetColor(0, 0, 0));
            }
            DX.DrawCircle((int)(top.x + 2 * size.x / 8), (int)(top.y + 2 * size.y / 8), 2, DX.GetColor(0, 0, 0));
            DX.DrawCircle((int)(top.x + 6 * size.x / 8), (int)(top.y + 6 * size.y / 8), 2, DX.GetColor(0, 0, 0));
            DX.DrawCircle((int)(top.x + 2 * size.x / 8), (int)(top.y + 6 * size.y / 8), 2, DX.GetColor(0, 0, 0));
            DX.DrawCircle((int)(top.x + 6 * size.x / 8), (int)(top.y + 2 * size.y / 8), 2, DX.GetColor(0, 0, 0));
           
        }
    }
}
