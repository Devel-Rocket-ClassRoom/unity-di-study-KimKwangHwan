using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using VContainer;

namespace DIStudy.Mogura
{
    public class MoguraClickRouter : MonoBehaviour
    {
        [SerializeField] private LayerMask m_ClickMask = ~0;

        private IGameService m_Game;

        [Inject]
        public void Construct(IGameService game)
        {
            m_Game = game;
        }

        private void Update()
        {
            if (!m_Game.IsRunning) return;

            if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame)
                return;

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            if (Camera.main == null)
                return;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, m_ClickMask))
                return;

            Mogura mogura = hit.collider.GetComponent<Mogura>();
            if (mogura != null)
                mogura.Collect();
        }
    }
}
