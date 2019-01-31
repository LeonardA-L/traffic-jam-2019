using UnityEngine;

namespace LeikirTest
{
    /*
     * A Control Strategy is used by the CharacterController to fetch inputs to move a character.
     * A child class must implement ReadInputMovement() to define a way to decide on the next movement
     * For the character.
     * For instance, a Gamepad strategy will read input from a user game pad.
     * An AI strategy will compute the next movement from a scripted AI.
     * An Online strategy will read input over the network from a remote player.
     */
    public abstract class ControlStrategy : ScriptableObject
    {
        [Tooltip("Rotation speed of the character (does not impact movement)")]
        public float m_rotationSpeed = 10;
        [Tooltip("Max speed for the character")]
        public float m_maxSpeed = 7;

        public abstract Vector3 ReadInputMovement();
        public abstract void Init(LeikirTest.CharacterController _controller);

        public float MaxSpeed
        {
            get
            {
                return m_maxSpeed;
            }
        }
        public float RotationSpeed
        {
            get
            {
                return m_rotationSpeed;
            }
        }
    }
}