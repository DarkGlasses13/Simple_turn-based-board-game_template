using Assets._Project.Asset_Loading;

namespace Assets._Project
{
    public class GameConfigLoader : LocalSingleAssetLoader<GameConfig>
    {
        public override object Key => "Game Config";
    }
}
