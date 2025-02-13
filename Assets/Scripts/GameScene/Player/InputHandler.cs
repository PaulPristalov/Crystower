using UnityEngine;
using UnityEngine.Events;

namespace GameScene.Player
{
    public class InputHandler : MonoBehaviour
    {
        public Vector3 RaycastPosition => Physics.Raycast(_ray, out RaycastHit hit) ? hit.point : Vector3.zero;

        private Camera _camera;
        private Ray _ray => _camera.ScreenPointToRay(Input.mousePosition);
        
        public event UnityAction OnFPressed;
        public event UnityAction OnClicked;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                OnFPressed?.Invoke();
            if (Input.GetMouseButtonDown(0))
            {
                OnClicked?.Invoke();
                
                if (Physics.Raycast(_ray, out RaycastHit hit) && hit.collider.TryGetComponent(out IClickable obj))
                {
                    obj.Click();
                }
            }
        }
    }
}
