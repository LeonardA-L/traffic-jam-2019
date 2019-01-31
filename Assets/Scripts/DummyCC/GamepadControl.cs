using UnityEngine;

namespace LeikirTest
{
    /*
     * Control strategy reading the movement command from a user input.
     * Each instance of the strategy can define axes for the character, allowing multiplayer controls.
     * (see ControlStrategy.cs)
     */
    [CreateAssetMenu(menuName = "Control Strategies/Gamepad Control", order = 1)]
    public class GamepadControl : ControlStrategy
    {
        [Tooltip("Name of the vertical axis used by this playable character (see Project Input settings)")]
        public string m_verticalAxis = "Vertical";
        [Tooltip("Name of the horizontal axis used by this playable character (see Project Input settings)")]
        public string m_horizontalAxis = "Horizontal";

        private Transform m_camera;

        public override void Init(LeikirTest.CharacterController _controller)
        {
            m_camera = Camera.main.transform;
        }

        public override Vector3 ReadInputMovement()
        {
            Debug.Assert(m_camera != null, "No Main Camera. Call Init to assign one");

            // Fetch user input
            Vector2 joy = new Vector2(Input.GetAxis(m_horizontalAxis), Input.GetAxis(m_verticalAxis));
            float magnitude = Mathf.Clamp(joy.magnitude, 0, 1);
            joy.Normalize();
            joy *= magnitude * MaxSpeed;
            // Leonard 2019-01-23: This is a bit convoluted but it makes sure keyboard players don't have higher speed when pressing both direction keys

            Vector3 camForward = m_camera.forward;
            camForward.y = 0;
            //Debug.Assert(camForward.sqrMagnitude != 0);   // With the hack'n'slash view we assume we don't need this
            camForward.Normalize();

            return joy.y * camForward
                 + joy.x * m_camera.right;
        }

    }
}