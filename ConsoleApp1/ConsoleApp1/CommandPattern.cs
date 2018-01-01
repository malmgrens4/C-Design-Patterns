using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPattern
{

    interface IncrementableVolume
    {
        void IncreaseVolume();
        void DecreaseVolume();
    }

    interface Command {
        void Execute();
        String Name();
    }

    class VolumeUp: Command
    {

        private TV tv;
        public VolumeUp(TV tv)
        {
            this.tv = tv;
        }

        public string Name()
        {
            return "Volume Up";
        }

        void Command.Execute()
        {
            this.tv.IncreaseVolume();
        }
    }

    class VolumeDown: Command
    {
        private TV tv;
        public VolumeDown(TV tv)
        {
            this.tv = tv;
        }

        public String Name()
        {
            return "Volume Down";
        }

        void Command.Execute()
        {
            this.tv.DecreaseVolume();
        }

    }

    
    class Remote
    {
        
        List<Command> commandHistory;
        public Remote()
        {
            this.commandHistory = new List<Command>();
        }
        public void StoreAndExecute(Command cmd)
        {
            this.commandHistory.Add(cmd);
            cmd.Execute();
            Console.WriteLine("Command Executes");
        }

        public void PrintHistory()
        {
            StringBuilder historyString = new StringBuilder();
            int i = 0;
            foreach (Command cmd in commandHistory)
            {
                historyString.Append("| ").Append(i).Append(" ").Append(cmd.Name()).Append(" |");
                i++;
            }
            Console.WriteLine(historyString);
        }

    }
    
    class TV: IncrementableVolume
    {
        int volume;
        public void IncreaseVolume()
        {
            volume++;
            Console.WriteLine("Volume Increases {0}", volume);
        }

        public void DecreaseVolume()
        {
            volume--;
            Console.WriteLine("Volume Decreases {0}", volume);

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            TV tv = new TV();
            Remote remote = new Remote();

            VolumeUp increaseVolume = new VolumeUp(tv);
            VolumeDown decreaseVolume = new VolumeDown(tv);

            remote.StoreAndExecute(increaseVolume);
            remote.StoreAndExecute(decreaseVolume);
            remote.StoreAndExecute(increaseVolume);
            remote.StoreAndExecute(increaseVolume);
            remote.PrintHistory();
            Console.ReadKey();
        }
    }
}
