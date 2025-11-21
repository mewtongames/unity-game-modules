using UnityEngine;

namespace MewtonGames.Common
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _direction;
        [SerializeField] private float _speed;

        private void Update()
        {
            if (_speed <= 0f)
            {
                return;
            }

            transform.Rotate(_direction * (_speed * UnityEngine.Time.deltaTime));
        }
    }
}