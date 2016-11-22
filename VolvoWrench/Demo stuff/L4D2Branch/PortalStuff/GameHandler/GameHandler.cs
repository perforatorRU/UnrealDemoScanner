using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VolvoWrench.Demo_stuff.L4D2Branch.PortalStuff.Result;

namespace VolvoWrench.Demo_stuff.L4D2Branch.PortalStuff.GameHandler
{
    public abstract class GameHandler
    {
        protected int CurrentTick
        {
            get;
            set;
        }

        public abstract DemoProtocolVersion DemoVersion
        {
            get;
            protected set;
        }

        public string FileName
        {
            get;
            set;
        }

        public string GameDir
        {
            get;
            set;
        }

        public string Map
        {
            get;
            set;
        }

        protected string MapEndAdjustType
        {
            get;
            private set;
        }

        protected List<string> Maps
        {
            get;
            private set;
        }

        protected string MapStartAdjustType
        {
            get;
            private set;
        }

        public int NetworkProtocol
        {
            get;
            set;
        }

        public string PlayerName
        {
            get;
            set;
        }

        public List<KeyValuePair<string, int>> Flags
        {
            get;
            set;
        }

        public int SignOnLen
        {
            get;
            set;
        }

        protected GameHandler()
        {
            this.MapStartAdjustType = "Map Start";
            this.MapEndAdjustType = "Map End";
            this.Maps = new List<string>();
            this.Flags = new List<KeyValuePair<string, int>>();
                        
        }

        public static GameHandler getGameHandler(string gameDir, string map)
        {
            if (gameDir == "portal")
            {
                return new PortalGameHandler();
            }
            if (gameDir == "portal2") {
                
                if (Category.Portal2Sp.Maps.Contains(map))
                {
                    return new Portal2SpGameHandler();    
                }
                if (Category.Portal2Coop.Maps.Contains(map))
                {
                    return new Portal2CoopGameHandler();
                }
                if (Category.Portal2CoopCourse6.Maps.Contains(map))
                {
                    return new Portal2CoopCourse6GameHandler();
                }
            }
            throw new Exception("Unknown game");
        }

        public abstract DemoParseResult GetResult();

        public abstract long HandleCommand(byte command, int tick, BinaryReader br);

        public abstract bool IsStop(byte command);

        protected abstract ConsoleCmdResult ProcessConsoleCmd(BinaryReader br);

        protected abstract long ProcessCustomData(BinaryReader br);

        protected abstract PacketResult ProcessPacket(BinaryReader br);

        protected int ProcessSignOn(BinaryReader br)
        {
            br.BaseStream.Seek((long)this.SignOnLen, SeekOrigin.Current);
            return this.SignOnLen;
        }

        protected abstract long ProcessStringTables(BinaryReader br);

        protected abstract long ProcessUserCmd(BinaryReader br);
    }

}