namespace UI
{
    public class StageProgress
    {
        private const int MinimumStageForPrestige = 50;

        public (int minLevel, int maxLevel) CalculatePrestigeLevels(int prestigeCount)
        {
            if (prestigeCount <= 0)
            {
                return (minLevel: 1, maxLevel: MinimumStageForPrestige);
            }

            var minimumLevel = 50 * prestigeCount;
            var maximumLevel = minimumLevel += MinimumStageForPrestige;

            return (minLevel: minimumLevel, maxLevel: maximumLevel);
        }
    }
}