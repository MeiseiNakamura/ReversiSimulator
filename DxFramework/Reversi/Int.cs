using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework.Reversi
{
    public class Int2
    {
        public int x{set; get;}
        public int y{set; get;}
        public Int2() { x=0; y=0;}
        public Int2(int x, int y){ this.x = x; this.y = y; }
    }
   public class Int3
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public Int3() { x = 0; y = 0; z = 0; }
        public Int3(int x, int y, int z) { this.x = x; this.y = y; this.z = z; }
        public Int3(Int2 int2, int z) { this.x = int2.x; this.y = int2.y; this.z = z; }
        public Int3(int x, Int2 int2) { this.x = x; this.y = int2.x; this.z = int2.y; }
    }
}
