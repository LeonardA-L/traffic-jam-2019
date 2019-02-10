using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace tfj
{
    public class YarnMap : MonoBehaviour
    {
        [YarnCommand("update")]
        public void UpdateMap()
        {
			var mapCtrl = GameObject.FindObjectOfType<MapController>();
            mapCtrl.DrawAll();
        }
    }
}