using System.Collections.Generic;
using System.Linq;

namespace Assets._Project.Actors_Base
{
    public abstract class ActorsBase<A> where A : Actor
    {
        private readonly List<A> _actors = new();

        public A GetByID(string id, bool isUsed = false, bool willWse = true)
        {
            A actor = _actors
                .Where(actor => actor.IsInUse == isUsed)
                .FirstOrDefault(actor => actor.Data.ID == id);

            actor ??= GetNewByID(id, willWse);

            return actor;
        }

        public A GetNewByID(string id, bool willWse = true)
        {
            A created = CreateByID(id);
            _actors.Add(created);
            created.IsInUse = willWse;
            return created;
        }

        protected abstract A CreateByID(string id);
    }
}
