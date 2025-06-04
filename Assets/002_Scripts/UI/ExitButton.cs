using UnityEngine;

using Assets.Scripts.Game;

namespace Assets.Scripts.UI
{
    public class ExitButton : Button
    {
        void OnMouseUpAsButton()
        {
            if(GameManager.OpenMessageBox("Are you sure you want to exit the application?", "Exit", MessageBoxType.YesNo) == GameManager.MB_RESULT_YES) 
                Application.Quit();
        }
    }
}