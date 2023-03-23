namespace SupremacyKingdom
{
    /// <summary>
    /// State of the Sling.
    /// </summary>
    public enum SlingshotState
    {
        Idle,
        UserPulling,
        BirdFlying
    }

    /// <summary>
    /// State of the stage.
    /// </summary>
    public enum GameState
    {
        Idle,
        Start,
        BirdMovingToSlingshot,
        Playing,
        Won,
        Lost
    }

    /// <summary>
    /// State of the Bird.
    /// </summary>
    public enum BirdState
    {
        BeforeThrown,
        Thrown
    }
    
}
