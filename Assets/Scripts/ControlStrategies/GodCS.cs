using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace tfj
{
    [CreateAssetMenu(menuName = "Control Strategies/God", order = 2)]
    public class GodCS : ControlStrategy
    {
        public override void Init(TFJCharacterController _controller)
        {
            base.Init(_controller);
        }

        public override void Execute(Vector2 _joy)
        {
            throw new System.NotImplementedException();
        }

    }
}