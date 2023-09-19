using Assets._Project.Actors_Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Assets._Project.Game.Characters
{
    public class CharactersBase : ActorsBase<Character>
    {
        private IEnumerable<CharacterData> _datas;

        public IReadOnlyCollection<CharacterData> Datas => (IReadOnlyCollection<CharacterData>)_datas;

        public async Task InitAsync()
        {
            _datas = await Addressables.LoadAssetsAsync<CharacterData>("Character Data", null).Task;
        }

        protected override Character CreateByID(string id)
        {
            return new Character(_datas.Single(data => data.ID == id));
        }
    }
}
