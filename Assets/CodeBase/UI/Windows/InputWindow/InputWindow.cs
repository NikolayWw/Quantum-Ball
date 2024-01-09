using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.UI.Windows.InputWindow
{
    public class InputWindow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private IInputService _inputService;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _inputService.SendTouchUp();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _inputService.SendTouchDown();
        }
    }
}