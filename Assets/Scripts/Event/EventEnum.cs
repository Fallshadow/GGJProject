
    /// <summary>
    /// 提供給不同模塊之間交互訊息，以降低偶合度。
    /// 事件分類以發送者的物件為依據，主要是各個Manager系統。
    /// </summary>
    public enum EventGroup : short
    {
        NONE = 0,
        UI,
        GAME,
    }
    public enum UiEvent : short
    {
        NONE = 0,
        CURSORENDFRAG,

        //ControlFunc
        ControlFuncSelect,
        ControlFuncUnSelect,
        UseGroove,
    }
    
    public enum GameEvent : short
    {
        NONE = 0,
        RestartGame,

    }

