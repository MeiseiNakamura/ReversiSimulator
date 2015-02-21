using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxFramework
{
	class MenuScene:Scene
	{
		public static MenuScene instance {get;private set; }
		public MenuScene() : base() {
            init();
            instance = this;}
		public override void init()
		{
            var Back = new Graphic(-1,new Vector2(800,450), "resource/img/タイトル1280_720.png");
            Time = 0;
		}
		public override void update()
		{
			base.update();
            Time++;
          //  if (Time > 50)
            {
                NextScene = GameScene.instance;
            }            
		}
        public int Time { get; set; }
    }
}
