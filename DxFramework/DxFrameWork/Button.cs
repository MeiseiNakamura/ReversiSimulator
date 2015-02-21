using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DxFramework
{
    class Button : Graphic
    {
        public string text { get; set; }
        public int textFontHandle
        {
            get { return _textFontHandle; }
            set
            {
                fontFlage = true;
                _textFontHandle = value;
            }
        }
        public bool fontFlage { get; set; }
        public int textColor { get; set; }
        public int color { get; set; }
        public int mouseOnColor { get; set; }
        public Vector2 textPosition { get; set; }
        public int TGraphHandle { get; protected set; }
        public bool colorFlag { get; set; }
        public int clickedTimes { get; private set; }
        public bool draggableFlag { get; set; }
        public bool draggedFlag { get; private set; }
        protected Action clickedAction;
        private int _TLight;
        private int _textFontHandle;
        private ComplexAction clickedComplexAction;
        public Button()
            : base()
        {
            init();
        }
        public Button(int layer)
            : base(layer)
        {
            init();
        }
        public Button(int layer, Vector2 midpos, string graphicname, Action clicked)
            : base(layer)
        {
            init();
            mid = midpos;
            this.GraphName = graphicname;
            clickedAction = clicked;
        }
        public Button(Vector2 midpos, string graphicname, Action clicked)
            : base()
        {
            init();
            mid = midpos;
            this.GraphName = graphicname;
            clickedAction = clicked;
        }
        public void init()
        {
            text = "";
            textColor = DX.GetColor(0, 0, 0);
            color = DX.GetColor(200, 200, 200);
            mouseOnColor = DX.GetColor(220, 220, 220);
            textPosition = new Vector2(0, 0);
            TLightFlag = true;
            TLight = 50;
            colorFlag = true;
            draggableFlag = false;
            draggedFlag = false;
            clickedTimes = 0;
            clickedAction = () => this.initialClickedAction();
            clickedComplexAction = (object i) => { };
        }

        private void initialClickedAction()
        {
            ;
        }
        public Action ClickedAction
        {
            set { clickedAction = value; }
        }
        public delegate void ComplexAction(object sender);
        public ComplexAction ClickedComplexAction
        {
            set { clickedComplexAction = value; }
        }

        public override void update()
        {
            base.update();
            if (isClickedOn()) draggedFlag = true;
            if (BasicInput.mouse.left.pressed == false) draggedFlag = false;
            if (draggableFlag && draggedFlag)
                mid += BasicInput.mouse.speed;
            if (isClickedOff())
            {
                clickedAction();
                clickedComplexAction(ComplexActionSender);
                clickedTimes++;
            }
        }
        public int TLight
        {
            get { return _TLight; }
            set
            {
                this._TLight = value;
                TGraphHandle = DX.MakeGraph((int)size.x, (int)size.y);
                DX.GraphFilterBlt(GraphHandle, TGraphHandle, DX.DX_GRAPH_FILTER_HSB, 0, 0, 0, value);
            }
        }
        public bool TLightFlag
        {
            get;
            set;
        }
        public new string GraphName
        {
            set
            {
                colorFlag = false;
                base.GraphName = value;
                TGraphHandle = GraphHandle;
            }
        }
        public override void draw()
        {
            if (isMoused())
            {
                if (colorFlag) DX.DrawBox((int)top.x, (int)top.y, (int)bottom.x, (int)bottom.y, mouseOnColor, 1);
                if (TLightFlag) DX.DrawGraphF((float)top.x, (float)top.y, TGraphHandle, 1);
                else base.draw();
            }
            else
            {
                if (colorFlag) DX.DrawBox((int)top.x, (int)top.y, (int)bottom.x, (int)bottom.y, color, 1);
                base.draw();
            }
            if (fontFlage)
            {
                DX.DrawStringToHandle((int)(top.x + textPosition.x), (int)(top.y + textPosition.y), text, textColor, textFontHandle);
            }
            else
                DX.DrawString((int)(top.x + textPosition.x), (int)(top.y + textPosition.y), text, textColor);

        }

        public object ComplexActionSender { get; set; }
    }
}
