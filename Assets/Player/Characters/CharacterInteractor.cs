using Interactables.Interfaces;
using UnityEngine;

namespace Player
{
    public class CharacterInteractor : MonoBehaviour
    {
        public CharacterType Type;
        public bool isEnabled;
       

        // private void OnTriggerEnter(Collider other)
        // {
        //
        //     if (other.gameObject.TryGetComponent(out IInteractor interactor))
        //         interactObject = interactor;
        // }
        
        // private void OnTriggerExit(Collider other)
        // {
        //     if (other.gameObject.TryGetComponent(out IInteractor interactor))
        //     {
        //         if (interactor == interactObject)
        //             interactObject = null;
        //     }
        // }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position,0.2f);
        }
    }
}
