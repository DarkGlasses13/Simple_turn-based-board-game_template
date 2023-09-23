using Assets._Project.Asset_Loading;
using Assets.Package.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Assets._Project.Game.Characters
{
    public class CharactersBase : TokensBase<CharacterData, Character>
    {
        private readonly GameConfigLoader _configLoader;
        private GameConfig _config;

        public CharactersBase(GameConfigLoader configLoader)
        {
            _configLoader = configLoader;
        }

        public IReadOnlyCollection<CharacterData> Datas => _datas.AsReadOnly();

        public override async Task LoadDataAsync()
        {
            _datas = new(await Addressables.LoadAssetsAsync<CharacterData>("Character Data", null).Task);
            _config = await _configLoader.LoadAsync();
        }

        protected override Character CreateByID(string id)
        {
            return new Character(GetDataByID(id), new LocalInstanceLoader(), _config);
        }
    }
}
