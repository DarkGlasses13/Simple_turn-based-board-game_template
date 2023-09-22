using Assets._Project.Asset_Loading;
using Assets.Package.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Assets._Project.Game.Characters
{
    public class CharactersBase : TokensBase<CharacterData, Character>
    {
        public IReadOnlyCollection<CharacterData> Datas => _datas.AsReadOnly();

        public override async Task LoadDataAsync()
        {
            _datas = new(await Addressables.LoadAssetsAsync<CharacterData>("Character Data", null).Task);
        }

        protected override Character CreateByID(string id)
        {
            return new Character(GetDataByID(id), new LocalInstanceLoader());
        }
    }
}
