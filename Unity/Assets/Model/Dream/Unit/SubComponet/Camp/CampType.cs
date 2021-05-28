namespace ET
{
    public enum CampType
    {
        CampNone = 0,
        CampRed = 1,
        CampGreen = 1 << 1,
        CampBlue = 1 << 2,
        CampYellow = 1 << 3,
        CampPink = 1 << 4,
        CampPurple = 1 << 5,
        CampNeutral = ~0
    }
}