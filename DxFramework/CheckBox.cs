using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework
{
    class CheckBox
    {
        public int fontHandle
        {
            set
            {
                adaptButton.textFontHandle = value;
                foreach (var itr in CheckButtonList)
                    itr.textFontHandle = value;
            }
        }
        private int selectedMode;
        public MultiGraphicButton[] CheckButtonList;
        public Button adaptButton;
        public Action[] adaptedActionList;
        private int adaptedMode;
        private int boxNumber;

        public CheckBox(int boxNumber, Vector2 top,int preAdaptedMode)
        {
            this.Top = top;
            this.boxNumber = boxNumber;
            this.adaptedActionList = new Action[boxNumber];
            this.selectedMode = preAdaptedMode;
            this.adaptedMode = preAdaptedMode;
            adaptButton = new Button();
            CheckButtonList = new MultiGraphicButton[boxNumber];
            int count = 0;
            for (int i = 0; i < boxNumber; i++)
            {
                count++;
                adaptedActionList[i] = initialadaptedAction;
                CheckButtonList[i] = new MultiGraphicButton();
                CheckButtonList[i].AddGraph("resource/img/checkCircle_not.png");
                CheckButtonList[i].AddGraph("resource/img/checkCircle_check.png");
                CheckButtonList[i].top = this.Top + new Vector2(0, i*70);
                CheckButtonList[i].textPosition = new Vector2(70, 30);
                CheckButtonList[i].GraphNumber = 0;
                CheckButtonList[i].TLightFlag = true;
                CheckButtonList[i].ClickedComplexAction = (object sender) =>
                {
                    selectedMode = (int)sender+1;
                    for (int j = 0; j < boxNumber; j++)
                    {
                        CheckButtonList[j].GraphNumber = 0;
                        CheckButtonList[j].TLightFlag = true;
                    }
                    CheckButtonList[(int)sender].GraphNumber = 1;
                    CheckButtonList[(int)sender].TLightFlag = false;
                    if (selectedMode == adaptedMode) 
                    {
                        adaptButton.color = DX.GetColor(220, 220, 220);
                        adaptButton.textColor = DX.GetColor(180, 180, 180);
                        adaptButton.mouseOnColor = adaptButton.color;
                    }
                    else
                    {
                        adaptButton.color = DX.GetColor(200, 200, 200);
                        adaptButton.mouseOnColor = DX.GetColor(220, 220, 220);
                        adaptButton.textColor = DX.GetColor(0, 0, 0);
                    }
                };
                CheckButtonList[i].ComplexActionSender = i;
            }
            CheckButtonList[preAdaptedMode - 1].GraphNumber = 1;
            CheckButtonList[preAdaptedMode - 1].TLightFlag = false;

            fontHandle = DX.CreateFontToHandle("メイリオ", 12, 1, DX.DX_FONTTYPE_ANTIALIASING);
            adaptButton.top = this.Top + new Vector2(0, 70*count);
            adaptButton.size = new Vector2(100, 30);
            adaptButton.text = "適応";
            adaptButton.textPosition = new Vector2(38, 9);
            adaptButton.color = DX.GetColor(220, 220, 220);
            adaptButton.textColor = DX.GetColor(180, 180, 180);
            adaptButton.mouseOnColor = adaptButton.color;
            adaptButton.ClickedAction = () =>
            {
                adaptedMode = selectedMode;
                adaptButton.color = DX.GetColor(220, 220, 220);
                adaptButton.textColor = DX.GetColor(180, 180, 180);
                adaptButton.mouseOnColor = adaptButton.color;
                adaptedActionList[selectedMode-1]();
            };
          
        }

        private void initialadaptedAction()
        {
            ;
        }
        public Vector2 Top { get; set; }

        public void setText(int number, string text)
        {
            CheckButtonList[number-1].text = text;
        }

        public int adaptButtonSpan { set {

            adaptButton.top = this.Top + new Vector2(0, 70 * boxNumber + value);
        
        } }
    }
}
