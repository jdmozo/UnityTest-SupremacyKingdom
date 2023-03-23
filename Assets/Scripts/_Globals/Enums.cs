using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupremacyKingdom
{
    public enum SlingshotState
    {
        Idle,
        UserPulling,
        BirdFlying
    }

    public enum GameState
    {
        Idle,
        Start,
        BirdMovingToSlingshot,
        Playing,
        Won,
        Lost
    }

    public enum BirdState
    {
        BeforeThrown,
        Thrown
    }
    
}
