using UnityEngine;

namespace Assets._Project.Actors_Base
{
    public class ActorInstance : MonoBehaviour
    {
        public string ID { get; private set; }

        public void Construct(string id)
        {
            ID = id;
        }
    }
}