﻿namespace IndustrialPark
{
    public enum RandomizerFlags
    {
        Warps                   = 1 << 0,
        Pickup_Positions        = 1 << 1,
        Tiki_Types              = 1 << 2,
        Tiki_Models             = 1 << 3,
        Tiki_Allow_Any_Type     = 1 << 4,
        Enemy_Types             = 1 << 5,
        MovePoint_Radius        = 1 << 6,
        Platform_Speeds         = 1 << 7,
        Boulder_Settings        = 1 << 8,
        Marker_Positions        = 1 << 9,
        Pointer_Positions       = 1 << 10,
        Player_Start            = 1 << 11,
        Timers                  = 1 << 12,
        Music                   = 1 << 13,
        DiscoFloors             = 1 << 14,
        Textures                = 1 << 15,
        Sounds                  = 1 << 16,
        Cameras                 = 1 << 17,
        DoubleBootHipLODT       = 1 << 18
    }

    public enum RandomizerFlagsP2
    {
        Level_Files              = 1 << 0,
        Reduce_Warps_To_HB01     = 1 << 1,
        Teleport_Box_Positions   = 1 << 2,
        Taxi_Positions           = 1 << 3,
        Bus_Stop_Positions       = 1 << 4,
        Mix_SND_SNDS             = 1 << 5,
        SIMP_Positions           = 1 << 6,
        Enemy_Models             = 1 << 7,
        Enemies_Allow_Any_Type   = 1 << 8,
        Models                   = 1 << 9
    }

    public enum RandomizerFlagsP3
    {
        BootToHB01            = 1 << 0,
        RandomBootLevel       = 1 << 1,
        DontShowMenuOnBoot    = 1 << 2,
        AllMenuWarpsHB01      = 1 << 3,
        Cheat_Invincible      = 1 << 4,
        BobCheat_BubbleBowl   = 1 << 5,
        BobCheat_CruiseBubble = 1 << 6,
        ScoobyCheat_Spring    = 1 << 7,
        ScoobyCheat_Helmet    = 1 << 8,
        ScoobyCheat_Smash     = 1 << 9,
        ScoobyCheat_Umbrella  = 1 << 10,
    }
}