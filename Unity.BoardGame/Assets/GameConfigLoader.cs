using Assets._Project;
using Assets._Project.Asset_Loading;

namespace Assets
{
    public class GameConfigLoader : LocalAssetLoader<GameConfig>
    {
        public override object Key => "Game Config";
    }
}
