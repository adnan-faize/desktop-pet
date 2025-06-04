using System;
using System.Runtime.InteropServices;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Assets.Scripts.Game
{
    public enum MessageBoxType
    {
        OK,
        YesNo
    }

    public class GameManager : MonoBehaviour
    {
        [DllImport("user32.dll")]
        static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-messagebox

        const uint MB_OK = (uint)(0x00000000L | 0x00000040L);
        const uint MB_YES_NO = (uint)(0x00000004L | 0x00000020L);

        public const int MB_RESULT_OK = 1;
        public const int MB_RESULT_YES = 6;
        public const int MB_RESULT_NO = 7;

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        [DllImport("Dwmapi.dll")]
        static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins margins);

        static readonly IntPtr hWndTopMost = new IntPtr(-1);

        IntPtr hWnd = new IntPtr();

        const int GWL_EXSTYLE = -0x14;
        const uint WS_EX_LAYERED = 0x00080000;
        const uint WS_EX_TRANSPARENT = 0x00000020;
        const uint WS_EX_TOOLWINDOW = 0x0080;

        public static Camera _cam;
        static Vector2 screenBounds;

        struct Margins
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        void Awake()
        {
            Application.runInBackground = true;

#if !UNITY_EDITOR
        hWnd = GetActiveWindow();

        Margins margins = new Margins { cxLeftWidth = -1 };

        DwmExtendFrameIntoClientArea(hWnd, ref margins);
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW);
        SetWindowPos(hWnd, hWndTopMost, 0, 0, 0, 0, 0);
#endif

            _cam = Camera.main;
            screenBounds = _cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        }

        void Update()
        {
            if (Physics2D.OverlapPoint(_cam.ScreenToWorldPoint(Input.mousePosition)) == null)
                SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW);
            else
                SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TOOLWINDOW);
        }

        public static Vector2Int BounceFromScreen(Vector2 pos, Vector2 offset)
        {
            Vector2Int dir = Vector2Int.zero;
            
            if (pos.x > screenBounds.x - offset.x)
                dir = Vector2Int.left;
            else if (pos.x < -screenBounds.x + offset.x)
                dir = Vector2Int.right;
            else if (pos.y > screenBounds.y - offset.y)
                dir = Vector2Int.down;
            else if (pos.y < -screenBounds.y + offset.y)
                dir = Vector2Int.up;
            
            return dir;
        }

        public static Vector2 GetRandomPos()
        {
            float x = Random.Range(-screenBounds.x, screenBounds.x);
            float y = Random.Range(-screenBounds.y, screenBounds.y);
            return new Vector2(x, y);
        }

        public static int OpenMessageBox(string text, string caption, MessageBoxType type)
        {
            uint messageBoxType = 0;

            switch (type)
            {
                case MessageBoxType.OK:
                    messageBoxType = MB_OK;
                    break;
                case MessageBoxType.YesNo:
                    messageBoxType = MB_YES_NO;
                    break;
            }

            return MessageBox(GetActiveWindow(), text, caption, messageBoxType);
        }
    }
}
