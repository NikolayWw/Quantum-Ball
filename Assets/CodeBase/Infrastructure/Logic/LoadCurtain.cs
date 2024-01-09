using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure.Logic
{
    public class LoadCurtain
    {
        private const string CurtainPath = "LoadinCurtain";

        private CanvasGroup _loadCurtain;

        private readonly CanvasGroup _curtainPrefab;
        private readonly ICoroutineRunner _coroutine;
        private readonly IAddDontDestroyOnLoad _addDontDestroyOnLoad;
        private bool _curtainShow;

        public LoadCurtain(ICoroutineRunner coroutine, IAddDontDestroyOnLoad addDontDestroyOnLoad)
        {
            _coroutine = coroutine;
            _addDontDestroyOnLoad = addDontDestroyOnLoad;
            _curtainPrefab = Resources.Load<GameObject>(CurtainPath).GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            if (_curtainShow == false)
            {
                _loadCurtain = Object.Instantiate(_curtainPrefab);
                _addDontDestroyOnLoad.AddDontDestroyOnLoad(_loadCurtain);
                _curtainShow = true;
            }
        }

        public void Hide()
        {
            _coroutine.StartCoroutine(HideCurtain());
        }

        private IEnumerator HideCurtain()
        {
            while (_loadCurtain != null && _loadCurtain.alpha > 0.0f)
            {
                _loadCurtain.alpha -= Time.deltaTime * 2f;
                yield return null;
            }

            if (_loadCurtain != null)
                Object.Destroy(_loadCurtain.gameObject);
            _curtainShow = false;
        }
    }
}