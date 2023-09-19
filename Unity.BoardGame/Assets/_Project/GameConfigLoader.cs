using Assets._Project.Asset_Loading;

namespace Assets._Project
{
    public class GameConfigLoader : LocalAssetLoader<Config>
    {
        public override object Key => "Game Config";
    }
}
