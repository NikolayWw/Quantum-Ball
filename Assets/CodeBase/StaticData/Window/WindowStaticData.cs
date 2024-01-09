using CodeBase.UI.Windows.InputWindow;
using UnityEngine;

namespace CodeBase.StaticData.Window
{
    [CreateAssetMenu(menuName = "Static Data/Window Static Data", order = 0)]
    public class WindowStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject UiRootPrefab { get; private set; }
        [field: SerializeField] public InputWindow InputWindowPrefab { get; private set; }
        [field: SerializeField] public GameObject WinWindowPrefab { get; private set; }
        [field: SerializeField] public GameObject LoseWindowPrefab { get; private set; }
        [field: SerializeField] public GameObject HUDPrefab { get; private set; }
    }
}