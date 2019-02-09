using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    public class BirdView
    {
        public static IEnumerator EndBirdEyeView(DockingStrategy _dockingStrategy)
        {
            CameraManager.Instance.SetFollowState(0.03f);
            var shake = EZCameraShake.CameraShaker.Instance.StartShake(4.0f, 5.0f, 0.2f);

            yield return new WaitForSeconds(0.8f);
            shake.StartFadeOut(0.1f);

            yield return new WaitForSeconds(3);

            CameraManager.Instance.SetFollowState();
            _dockingStrategy.StartDocking();
        }
    }
}