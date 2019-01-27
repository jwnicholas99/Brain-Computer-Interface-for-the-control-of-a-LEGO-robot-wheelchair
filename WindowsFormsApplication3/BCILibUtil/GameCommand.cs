using System;
using System.Collections.Generic;
using System.Text;

namespace BCILib.App
{
    public class GameCommand
    {
        public const int StartGame = 1;
        public const int SetBCIScore = 2;
        public const int StopGame = 3;
        public const int CloseGame = 4;
        public const int SetPlayerName = 5;

        public const int SetPlayerName2 = 6;
        public const int SetBCIScore2 = 7;

        // Added 2010.07.26
        public const int Pause = 8;
        public const int Resume = 9;

        public const int SetGameData = 10;

        public const int Timer_Start = 30;
        public const int Timer_Pause = 31;
        public const int Timer_Resume = 32;
        public const int Timer_Stop = 33;
        public const int Timer_Elased = 34;

        public const int CMD_SENDMESSAGE = 61;
        public const int CMD_SENDGAMEDAT = 62;

        // GameLevel -- 20120614
        public const ushort Game_Level = 70;

        public const ushort SendCmdString = ushort.MaxValue;
    }
}
