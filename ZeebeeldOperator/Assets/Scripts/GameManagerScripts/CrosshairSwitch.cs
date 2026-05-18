using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bnyhtz
{
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
