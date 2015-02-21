using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
namespace DxFramework
{
    abstract class DrawableBase
    {
        static Action<DrawableBase> autoAddFunc;
        private int _layer;
        public int layer 
        {
            get {return _layer; }
            set { _layer = value; }
        }
        public bool isVisible { get; set; }
        public DrawableBase(int layer)
        {
            isVisible = true;
            this.layer = layer;
            if (autoAddFunc != null) autoAddFunc(this);
        }
        public static void setAutoAddFunc(Action<DrawableBase> addlistfunc) { autoAddFunc = addlistfunc; }
        abstract public void draw();
        abstract public void update();
    }
}
