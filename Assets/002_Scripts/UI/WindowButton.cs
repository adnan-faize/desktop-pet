using UnityEngine;

namespace Assets.Scripts.UI
{
    public class WindowButton : Button
    {
        public GameObject panelToActivate;
        public GameObject[] panelsToDeactivate;

        void Update()
        {
            if (panelToActivate.activeInHierarchy)
                UpdateColor(Color.white);
            else
                UpdateColor(Color.gray);
        }

        void OnMouseUpAsButton()
        {
            panelToActivate.SetActive(true);

            foreach (GameObject panelToDeactivate in panelsToDeactivate)
                panelToDeactivate.SetActive(false);
        }
    }
}