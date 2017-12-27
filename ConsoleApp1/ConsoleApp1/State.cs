using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteveCorpBanking
{

    class Machine
    {
       
        static void Main(string[] args)
        {
            ATM mainATM = new ATM();
            Console.WriteLine("Hello, welcome to Steve Corp. banking system");
            Boolean running = true;
            string uInput = "";
            Console.WriteLine();
            while (running)
            {
                uInput = Console.ReadLine();
                if(uInput == "d")
                {
                    Console.WriteLine("Enter Amount: ");
                    long amount = long.Parse(Console.ReadLine());
                    mainATM.Deposit(amount);
                }
                
                if(uInput == "w")
                {
                    Console.WriteLine("Enter Amount: ");
                    long amount = long.Parse(Console.ReadLine());
                    mainATM.Withdrawal(amount);
                    
                }
            }
            Console.ReadKey();

        }
    }

    class ATM
    {
        ArrayList accounts;
        State state;
        ArrayList users;
        Log logger;
        public ATM()
        {
            this.accounts = new ArrayList();
            this.users = new ArrayList();
            this.logger = new Log();
            Account newAccount = CreateAccount();
            this.state = new EmptyState(newAccount, this);
        }

        public void SetState(State value)
        {
            Console.WriteLine("Changing States");
            state = value;
        }

        public State GetState()
        {
            return state;
        }

        public void Deposit(long amount)
        {
            state.Deposit(amount);
            Console.WriteLine("{0} Deposited. Account balance now {1}", amount, state.Account.Balance);
            logger.LogAction(DateTime.Now, state.Account.GetUser, "deposit", true);
        }

        public void Withdrawal(long amount)
        {
            Boolean result = this.state.Withdrawal(amount);
            Console.WriteLine("{0} Account balance now {1}", result, this.state.Account.Balance);
            logger.LogAction(DateTime.Now, this.state.Account.GetUser, "withdrawal", result);
        }
        
        /**
         * Starts a console input sequence to get new account details and adds it to the ATMs Registry 
         * Returns success or failure status
         */
        public Account CreateAccount()
        {
            string uID = "";
            string PIN = "";
            Boolean inputValid = false;
            Console.Write("Input uID: ");
            while (!inputValid)
            {
                uID = Console.ReadLine();
             
                if (users.Contains(uID))
                {
                    Console.WriteLine("Name taken, please input another.");
                }
                else
                {
                    inputValid = true;
                }
            }

            inputValid = false;

            Console.Write("Input 4 digit PIN: ");
            string PINFormat = "\\d{4}";
            Regex r = new Regex(PINFormat);

            while (!inputValid)
            { 
                PIN = Console.ReadLine();
                
                if (!r.Match(PIN).Success)
                {
                    Console.WriteLine("PIN invalid format, please try again.");
                }
                else
                {
                    inputValid = true;
                }
            }
            Account newAccount = new Account(uID, 0, PIN);
            logger.LogAction(DateTime.Now, newAccount.GetUser, "Create Account", true);
            return newAccount;
        }



    }

    class LogItem
    {
        DateTime date;
        string user;
        String action;
        Boolean status;

        public LogItem(DateTime date, string user, string action, Boolean status)
        {
            this.date = date;
            this.user = user;
            this.action = action;
            this.status = status;
        }
        public override string ToString()
        {
            StringBuilder logString = new StringBuilder();
            logString.Append("Date: ")
                .Append(date.ToString())
                .Append(" ");
            logString.Append("User: ")
                .Append(user.ToString())
                .Append(" ");
            logString.Append("Action: ")
                .Append(action)
                .Append(" ");
            logString.Append("Status: ")
                .Append(status.ToString())
                .Append(" ");
            return logString.ToString(); 
        }

    }

    /**
     * For expansion of the project, not pertinent for the project
     **/
    class Log
    {
        private IList<LogItem> logs;
        public Log()
        {
            logs = new List<LogItem>();
        }
        public void LogAction(DateTime date, string user, string action, Boolean status)
        {
            LogItem logItem = new LogItem(date, user, action, status);
            logs.Add(logItem);
        }

        public void DisplayLog()
        {
            foreach (LogItem log in logs)
            {
                Console.WriteLine(log.ToString());
            }
            
        }
    }

    /**
     * Account Details 
     * */
    class Account
    {
        string user;
        string PIN;
        long balance;

        public Account(string user, long balance, string PIN)
        {
            this.user = user;
            this.balance = balance;
            this.PIN = PIN;
        }

        public string GetPIN => PIN; //only a getter for the PIN
        public string GetUser => user; //only getter for the accounts associated user
        public long Balance => balance; //only set the balance, we have deposit and withdrawal for manipulating 
        public Boolean Withdrawal(long amount)
        {
            
                balance -= amount;
                return true;
        
        }

        public long Deposit(long amount)
        {
            balance += amount;
            return balance;
        }

        public Boolean Login(string PIN)
        {
            if (PIN == this.PIN){ return true; }
            else { return false; }
        }
    }

    /**
     * This is the state. Different bank states will extend it and implement its members.
     * */
    abstract class State
    {
        protected Account account;
        protected ATM atm;
        public Account Account
        {
             get { return account; }
             set { account = value; }
        }
        public ATM ATM
        {
            get { return atm; }
            set { atm = value; }
        }
        public abstract Boolean Deposit(long amount);
        public abstract Boolean Withdrawal(long amount);

    }   

    class EmptyState: State { 
   
        public EmptyState(State state): this(state.Account, state.ATM)
        {
        }

        public EmptyState(Account account, ATM ATM)
        {
            this.account = account;
            this.ATM = ATM;
        }
        public override Boolean Deposit(long amount)
        {
            account.Deposit(amount);
            CheckStateChange();
            return true;
        }

        public override Boolean Withdrawal(long amount)
        {

            Console.WriteLine("Insufficient funds.");
            return false;
        }
        /**
         * Check if we have a positive balance to switch to the regular state
         * */
        public void CheckStateChange()
        {
            if(account.Balance > 0)
            {
                Console.WriteLine("State change to Regular");
                ATM.SetState(new RegularState(this));
            }
        }
    }

    class RegularState : State
    {
        
        public RegularState(State state)
        {
            this.account = state.Account;
            this.ATM = state.ATM;
        }
        public override bool Deposit(long amount)
        {
            account.Deposit(amount);
            return true;
        }

        public override bool Withdrawal(long amount)
        {
            account.Withdrawal(amount);
            CheckStateChange();
            return true;
        }

        /**
         * Check if we are below or equal to zero funds and switch to the empty state.
         * */
        void CheckStateChange()
        {
            if(account.Balance <= 0)
            {
                Console.WriteLine("State change to Empty");
                ATM.SetState(new EmptyState(this));
            }
        }
    }


}
