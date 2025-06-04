using UnityEngine;

using TMPro;

using Assets.Scripts.Game;

namespace Assets.Scripts.UI
{
    public class Window : MonoBehaviour
    {
        public TMP_Text welcomeBackMessage;
        public TMP_Text currency;

        void Start()
        {
            welcomeBackMessage.text = $"Welcome back {Player.playerName}";
            currency.text = $"${Player.currency}";
        }

        void Update()
        {
            currency.text = $"${Player.currency}";
        }
    }
}