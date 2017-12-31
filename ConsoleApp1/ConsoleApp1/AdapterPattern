using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    /**
     * A giant interface.
     * */
    interface IGiant
    {
        void Smash();
        void Stomp();
        void Toss();
    }
    /**
     * A catapult that can load and launch
     * */
    interface ICatapult
    {
        void Load();
        void Launch();
    }
    /**
     * our plain old giant, who the old design only had smashing and grabbing
     * */ 
    class Giant : IGiant
    {
        bool thingInHand;
        public Giant()
        {
            thingInHand = false;
        }
        public bool ThingInHand
        {
            get { return thingInHand; }
        }
        public void Smash()
        {
            Console.WriteLine("I smash thing!");
        }

        public void Grab()
        {
            thingInHand = true;
            Console.WriteLine("I have grabbed thing!");
        }

        public void Stomp()
        {
            Console.WriteLine("I stomp!");
        }

        public void Toss()
        {
            thingInHand = false;
            Console.WriteLine("I toss puny thing!");
        }
    }

    class Catapult : ICatapult
    {
        bool loaded;
        public Catapult()
        {
            loaded = false;
        }
        public void Launch()
        {
            if (loaded)
            {
                Console.WriteLine("Catapult Launching!");
                loaded = false;
            }
            else
            {
                Console.WriteLine("Nothing to Fire!");
            }
        }

        public void Load()
        {
            Console.WriteLine("Catapult Loading!");
            loaded = true;
        }
    }

    class CatapultBrigade
    {
        List<ICatapult> lineUp = new List<ICatapult>();
        public CatapultBrigade()
        {
        }
        public void AddCatapult(ICatapult catapult)
        {
            lineUp.Add(catapult);
        }
        public void LaunchCommand()
        {
            foreach(ICatapult catapult in lineUp)
            {
                catapult.Launch();
            }
        }

        public void LoadCommand()
        {
            foreach(ICatapult catapult in lineUp)
            {
                catapult.Load();
            }
        }



    }
    /**
     * Our Giant adapter that will allow us to turn that lame our giant into an effective catapult if he is with the catapult brigade.
     * */ 
    class GiantAdapter : ICatapult
    {
        Giant giant;
        public GiantAdapter(Giant giant)
        {
            this.giant = giant;
        }
        public void Launch()
        {
            if (giant.ThingInHand)
            {
                Console.WriteLine("The giant throws a thing!");
                giant.Toss();
            }
            else
            {
                Console.WriteLine("Nothing to throw!");
            }
        }

        public void Load()
        {
            giant.Grab(); //mapping the Load functionality with the old giants functionality
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Giant greenGiant = new Giant();
            GiantAdapter catapultGiant = new GiantAdapter(greenGiant);
            Giant hulk = new Giant();
            Catapult stoneLauncher = new Catapult();
            CatapultBrigade catapultBrigade = new CatapultBrigade();
            catapultBrigade.AddCatapult(catapultGiant);
            catapultBrigade.AddCatapult(stoneLauncher);
            catapultBrigade.LoadCommand();
            catapultBrigade.LaunchCommand();
            hulk.Smash();
            Console.ReadKey();
        }
    }
}
