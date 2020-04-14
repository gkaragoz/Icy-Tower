public enum GameState {
    Loading,
    MainMenu,
    GamePaused,
    GameplayCountdown,
    Gameplay,
    GameOver,
    Wardrobe
}

public enum CameraStateEnums {
    MainMenu_to_Gameplay,
    Gameplay_to_MainMenu,
    MainMenu_to_Wardrobe,
    Wardrobe_to_MainMenu,
    MainMenu_to_MainMenu,
    None
}

public enum GoldHolderTypes {
    ShortLineGoldHolder,
    LongLineGoldHolder,
    TriangleGoldHolder,
    RectangleGoldHolder,
    SquareGoldHolder
}

public enum GoldTypes {
    YellowGold,
    GreenGold,
    RedGold
}

public enum Collectables {
    CoinMagnet,
    PlatformSaver,
    SpeedUp,
    StickyPlunger,
    TimeSlower,
    Umbrella,
    SuperCoin,
    Key
}

public enum VFXTypes {
    VFXCollectGold,
    VFXComboJump,
    VFXJump,
    VFXGhost,
    VFXMagnet,
    VFXUmbrella,
    VFXPumpWalking,
    VFXConffetti
}

public enum TowerProp{
    Skull,
    Torch,
}

public enum AnimationState {
    Walk,
    LeftRun,
    RightRun,
    Idle,
    Jump,
    ComboJump,
    Flip,
    Rotate,
    Fall
}

// Every sound enums has their sound files same named as enum.
public enum SoundFXTypes {
    UI_General_Button_Open_or_Click,// EVERY BUTTON HAS THIS SOUND BUT WANT TO OVERRIDES ARE DIFFERENT.
    UI_General_Button_Close,
    UI_Buy_General,                    // Slot power up, Costume, Booster, Music, Particle
    UI_Buy_Upgrade,                    // Slot power up and passive power up level,
    UI_Try_Costume,                    // Try costume in wardrobe screen.
    UI_Add_Gold,                    // When user gets gold rewards on UI screen.
    UI_Add_Key,                     // When user gets key rewards on UI screen.
    UI_Add_Gem,                        // When user gets gem rewards on UI screen.
    UI_Change_Screen,               // Change main screen sound fx over social, market, main screen, wardrobe.
    UI_Open_Close_Wardrobe_Screen,    // Open close wardrobe on UI screen.
    UI_Change_Wardrobe_Page,        // Change page on wardrobe UI.
    UI_Spin_Wheel_Reward,            // Reward on spin wheel stopped.
    UI_Spin_Wheel_Tick_Loop,        // Will loop until get internet response.
    UI_Spin_Wheel_Tick_End_Slow,    // Starts after internet response.
    UI_Open_Map_Popup,
    UI_Break_My_Best_Score,
    UI_Stats_Show_UP,
    InGame_Player_Jump,
    InGame_Player_Jump_Combo,
    InGame_Collect_Gold,            // Starts from 1 and 0.05 pitch increase until 1.75.
    InGame_Collect_Key,
    InGame_Collect_SuperGold,
    InGame_Collect_Slot_Powerup,    // Umbrella, Pump, Saver block, slow timer power ups.
    InGame_PowerUp_Pump_Step,
    InGame_PowerUp_Pump_Jump,
    InGame_PowerUp_BlockSaver,
    InGame_PowerUp_Rocket,
    InGame_100_Confetti,
    InGame_Break_My_Best_Score_Yaaay,
    InGame_Use_Revive_Key,
    InGame_Ready
}


