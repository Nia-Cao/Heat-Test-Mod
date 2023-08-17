using MelonLoader;
using UnityEngine;
using Heat;

[assembly: MelonInfo(typeof(Heat_Test_Mod.Main), "Just a Test", "0.0", "JamJar(Niamh)")]
[assembly: MelonGame()]
namespace Heat_Test_Mod
{
    public class Main : MelonMod
    {

        private GameObject cloneAuroraInstance;

       // private HeatAPI heat = new HeatAPI();

        private Vector3 offset = new Vector3(1, 0, 0); // Define the offset here

        public override void OnApplicationStart()
        {
            
        }

        public override void OnGUI()
        {
            // You can add GUI elements here if needed
        }

        public override void OnUpdate()
        {
            // Find the original "Aurora" object
            GameObject originalAurora = GameObject.Find("Aurora(Clone)");

            // Destroy the clone if the original is destroyed
            if (originalAurora == null && cloneAuroraInstance != null)
            {
                GameObject.Destroy(cloneAuroraInstance);
                cloneAuroraInstance = null;
            }

            // Create and update the clone if the original is found
            if (originalAurora != null)
            {
                if (cloneAuroraInstance == null)
                {
                   // cloneAuroraInstance = GameObject.Instantiate(originalAurora);
                   // DisableAllAnimations(cloneAuroraInstance);
                    CallPossessFunction(originalAurora);
                }

              //  UpdateTransforms(originalAurora.transform, cloneAuroraInstance.transform);
            }
        }

        private void DisableAllAnimations(GameObject gameObject)
        {
            Animator animator = gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }

            foreach (Transform child in gameObject.transform)
            {
                DisableAllAnimations(child.gameObject);
            }
        }

        private void UpdateTransforms(Transform originalParent, Transform cloneParent)
        {
            // Recursively update the position and rotation of all child GameObjects
            for (int i = 0; i < originalParent.childCount; i++)
            {
                Transform originalChild = originalParent.GetChild(i);
                Transform cloneChild = cloneParent.GetChild(i);

                // Copy position and rotation with offset
                cloneChild.position = originalChild.position + offset;
                cloneChild.rotation = originalChild.rotation;

                // Recursively update child transforms
                UpdateTransforms(originalChild, cloneChild);
            }
        }

        private void CallPossessFunction(GameObject originalAurora)
        {

            // Check if the original "Aurora" object was found
            if (originalAurora != null)
            {
                // Get the NPCController component of the original "Aurora" object
                NPCController npcController = originalAurora.GetComponent<NPCController>();

                // Check if the NPCController component is valid
                if (npcController != null)
                {
                    // Call the Possess function to possess the NPCController
                    PossessNPCController(npcController);
                }
            }
        }
        private void PossessNPCController(NPCController npcController)
        {
            // Call the Possess function of the NPCController component
            npcController.PossessCharacter.Possess(true); // Set shouldReplaceHands according to your needs
        }

    }
}
