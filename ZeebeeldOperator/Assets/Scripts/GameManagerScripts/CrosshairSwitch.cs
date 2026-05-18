using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bnyhtz
{
    /// <summary>
    /// Shows or hides the crosshair UI object for systems that need to control player aiming feedback.
    /// </summary>
    public class CrosshairSwitch : MonoBehaviour
    {
        [SerializeField] private GameObject _crosshair;

        public void EnableCrosshair()
        {
            if (_crosshair != null)
            {
                _crosshair.SetActive(true);
            }
        }

        public void DisableCrosshair()
        {
            if (_crosshair != null)
            {
                _crosshair.SetActive(false);
            }
        }
    }
}
