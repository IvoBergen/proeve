using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bnyhtz
{
    public class KeycardSwiper : MonoBehaviour, IInterface
    {
        public void Interact()
        {
            Debug.Log("Swiped Keycard!");
        }
    }
}

