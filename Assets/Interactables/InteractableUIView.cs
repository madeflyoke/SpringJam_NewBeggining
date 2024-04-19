using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(Canvas))]
    public class InteractableUIView : MonoBehaviour
    {
        [SerializeField] private Transform _buttonInfoTr;

        private void Awake()
        {
            HideButtonInfo();
        }
        
        public void ShowButtonInfo()
        {
            _buttonInfoTr.gameObject.SetActive(true);
        }
        
        public void HideButtonInfo()
        {
            _buttonInfoTr.gameObject.SetActive(false);
        }
        
    }
}
