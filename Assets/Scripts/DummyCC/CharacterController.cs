using UnityEngine;

namespace LeikirTest
{
    /*
     * Controls a character, using a control strategy (see ControlStrategy.cs)
     */
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterController : MonoBehaviour
    {
        [Tooltip("Control strategy for this character. Instantiate one from the Assets menu.")]
        public ControlStrategy m_controlStrategy;

        private Rigidbody rb;
        private Vector3 lastDirection;

        void Start()
        {
            // Fetch needed components and initialize
            rb = GetComponent<Rigidbody>();
            lastDirection = transform.forward;
            Debug.Assert(m_controlStrategy != null, "No control strategy provided");
            m_controlStrategy.Init(this);
            /*
             * Note: because of the Init we can't change strategies mid-game but that is trivial to work around, we could call
             * Init any time the strategy changes.
             * Leonard 2019-01-23: Right now it might work by accident if strategies have been properly initialized once, but hotswapping strategies is
             * currently not supported.
             */
        }

        /*
         * Reads input from the control strategy (see ControlStrategy.cs) and moves the rigidbody based on the input
         * Note: Really minimalistic.
         */
        void FixedUpdate()
        {
            lastDirection = m_controlStrategy.ReadInputMovement();
            rb.MovePosition(rb.position + lastDirection * Time.fixedDeltaTime);
        }

        /*
         * Updates the orientation of the character based on its trajectory.
         */
        void Update()
        {
            transform.forward = Vector3.Slerp(transform.forward, lastDirection, m_controlStrategy.RotationSpeed * Time.deltaTime);
            //Note: Here the orientation lerp is purely cosmetic. Direction does not impact movement (cf how FixedUpdate does not take direction into account).
        }
    }
}