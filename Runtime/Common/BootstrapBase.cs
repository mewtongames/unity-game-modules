using System.Collections;
using UnityEngine;

namespace MewtonGames.Common
{
    public class BootstrapBase : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 60;
        [SerializeField] private bool _isMultiTouchEnabled;

        protected virtual void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;
            Input.multiTouchEnabled = _isMultiTouchEnabled;
        }

        protected IEnumerator Initialize(IInitializable initializable)
        {
            initializable.Initialize();
            yield return new WaitWhile(() => !initializable.isInitialized);
        }
        
        protected IEnumerator Initialize<T>(IInitializable<T> initializable, T data)
        {
            initializable.Initialize(data);
            yield return new WaitWhile(() => !initializable.isInitialized);
        }
    }
}