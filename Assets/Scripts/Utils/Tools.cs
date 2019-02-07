using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class Tools : MonoBehaviour
    {
        public static float LinearScale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
        {
            if (OldValue > OldMax)
                OldValue = OldMax;
            if (OldValue < OldMin)
                OldValue = OldMin;
            float OldRange = (OldMax - OldMin);
            float NewRange = (NewMax - NewMin);
            float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

            return (NewValue);
        }
    }
}