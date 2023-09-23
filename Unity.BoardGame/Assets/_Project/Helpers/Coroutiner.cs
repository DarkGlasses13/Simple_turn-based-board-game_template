using System.Collections;
using UnityEngine;

namespace Assets._Project.Helpers
{
    public class Coroutiner : MonoBehaviour
    {
        public void StartRoutine(IEnumerator routine) => StartCoroutine(routine);

        public void StopRoutine(Coroutine routine) => StopCoroutine(routine);

        public void StopAllRoutines() => StopAllCoroutines();
    }
}
