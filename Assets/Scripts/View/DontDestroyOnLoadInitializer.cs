using UnityEngine;

namespace View
{
    public class DontDestroyOnLoadInitializer : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
