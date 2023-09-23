using Assets._Project.Asset_Loading;

namespace Assets._Project
{
    public class GameConfigLoader : LocalAssetLoader<GameConfig>
    {
        public override object Key => "Game Config";
    }
}
