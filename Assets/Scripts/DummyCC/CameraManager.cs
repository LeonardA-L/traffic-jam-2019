using UnityEngine;

namespace LeikirTest
{
    /*
     * Handles camera movements.
     * This manager makes the camera follow the player, in a hack'n'slash type view (3D ~iso)
     */
    public class CameraManager : Singleton<CameraManager>
    {
        protected CameraManager() { }

        [Tooltip("Distance between the camera and the character")]
        public float m_distance = 12;

        private Transform m_cam;
        private Transform m_character;

        void Start()
        {
            // Fetch needed transforms
            m_cam = Camera.main.transform;

            var character = (LeikirTest.CharacterController)FindObjectOfType(typeof(LeikirTest.CharacterController)); // Assume there is only one character
            // In an actual production, player character(s) would be dealt by a PlayerManager, which would expose a GetPlayers() accessor
            Debug.Assert(character != null, "No LeikirTest.CharacterController found on scene");
            m_character = character.transform;
        }

        void Update()
        {
            // Move the camera to aim at the character. The camera does not rotate.
            m_cam.transform.position = Vector3.Lerp(m_cam.transform.position, m_character.position - m_cam.forward * m_distance, 0.1f);
            /*
             * Note: this is a minimalistic camera controller. There are no additional effects, and the linear interpolation makes the
             * camera lag behind the character.
             * A good way to fight the lag would be to predict a tendency in the character's movement based on multiple frames and aim at a future position.
             * An example of this can be found here https://github.com/LeonardA-L/ide-vbg/blob/master/Assets/Scripts/Managers/CameraManager.cs#L121
             * However, this makes the camera blend really fast during sharp turns.
             */
        }
    }
}