using System;
using DxLibDLL;
using DxFramework.Reversi;

namespace DxFramework
{
    static class Test
    {

        [STAThread]
        static void Main()
        {
            //--------------------------initializing dxlib--------------------------
            DX.ChangeWindowMode(DX.TRUE);


            DX.SetWindowSizeChangeEnableFlag(DX.TRUE, DX.TRUE);
            DX.SetMainWindowText("");
            DX.SetGraphMode(1600, 900, 32);
            //  DX.SetWindowSizeExtendRate(0.9);
            DX.SetWindowInitPosition(-8, 0);

            if (DX.DxLib_Init() == -1) return;
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            //++++++++++++++++++++++++++initialized dxlib++++++++++++++++++++++++++++
            var menuscene = new MenuScene();
            var gamescene = new GameScene();
            Scene scene = gamescene;
            while (DX.ScreenFlip() == 0 && DX.ProcessMessage() == 0 && DX.ClearDrawScreen() == 0)
            {
                //-----------------------------mainloop---------------------------
                BasicInput.update();
                scene.update();
                DX.ClearDrawScreen();
                scene.draw();
                if (scene != scene.NextScene)
                {
                    //fade-in
                    for (int i = 255; i >= 0; i -= 20)
                    {
                        DX.SetDrawScreen(DX.DX_SCREEN_BACK);
                        DX.SetDrawBright(i, i, i);
                        scene.draw();
                        DX.ScreenFlip();
                    }
                    //fade-out
                    for (int i = 0; i <= 260; i += 20)
                    {
                        DX.SetDrawScreen(DX.DX_SCREEN_BACK);
                        DX.SetDrawBright(i, i, i);
                        scene.NextScene.draw();
                        DX.ScreenFlip();
                    }
                }
                scene = scene.NextScene;
                //+++++++++++++++++++++++++++++++mainloop+++++++++++++++++++++++++
            }
            DX.DxLib_End();
        }
    }
}