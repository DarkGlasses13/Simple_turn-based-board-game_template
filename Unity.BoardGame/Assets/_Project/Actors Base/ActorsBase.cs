using System.Collections.Generic;
using System.Linq;

namespace Assets._Project.Actors_Base
{
    public abstract class ActorsBase<A> where A : Actor
    {
        private readonly List<A> _actors = new();

        public A GetByID(string id)
        {
            A actor = _actors
                .Where(actor => actor.IsInUse == false)
                .FirstOrDefault(actor => actor.Data.ID == id);

            if (actor != null)
            {
                CreateByID(id);
            }

            return actor;
        }

        public A GetNewByID(string id)
        {
            A created = CreateByID(id);
            _actors.Add(created);
            return created;
        }

        protected abstract A CreateByID(string id);
    }
}
