using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework
{
    class MultiGraphicButton : Button
    {
        private List<int> GraphHandleList;
        private List<int> TGraphHandleList;
        public int GraphNumber
        {
            set
            {
                colorFlag = false;
                base.GraphHandle = GraphHandleList[value];
                base.TGraphHandle = TGraphHandleList[value];
            }
        }

        public MultiGraphicButton()
        {
            GraphHandleList = new List<int>();
            TGraphHandleList = new List<int>();
            TLight = 0;
        }
        public MultiGraphicButton(int layer):base(layer)
        {
            GraphHandleList = new List<int>();
            TGraphHandleList = new List<int>();
            TLight = 0;
        }
        public void AddGraph(string graphName)
        {
            GraphHandleList.Add(DX.LoadGraph(graphName));
            int x, y;
            DX.GetGraphSize(GraphHandleList.Last(), out x, out y);
            var Handle = DX.MakeGraph(x, y);
            DX.GraphFilterBlt(GraphHandleList.Last(), Handle, DX.DX_GRAPH_FILTER_HSB, 0, 0, 0, TLight);
            TGraphHandleList.Add(Handle);
        }
        public void AddGraph(string graphName, string mousedGraphName)
        {
            GraphHandleList.Add(DX.LoadGraph(graphName));
            TGraphHandleList.Add(DX.LoadGraph(mousedGraphName));
        }
        public void AddGraph(int graphHandle)
        {
            GraphHandleList.Add(graphHandle);
            int x, y;
            DX.GetGraphSize(graphHandle, out x, out y);
            var Handle = DX.MakeGraph(x, y);
            DX.GraphFilterBlt(graphHandle, Handle, DX.DX_GRAPH_FILTER_HSB, 0, 0, 0, TLight);
            TGraphHandleList.Add(Handle);
        }
        public void AddGraph(int graphHandle, int mousedGraphHandle)
        {
            GraphHandleList.Add(graphHandle);
            TGraphHandleList.Add(mousedGraphHandle);
        }
    }
}
