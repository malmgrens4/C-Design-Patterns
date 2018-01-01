using System;

namespace FactoryPattern
{
    enum EmojiTypes
    {
        CLOWN,
        SMILE
    }
    interface Emoji
    {
        void SayLine();
    }
    class EmojiFactory
    {
        public static Emoji CreateEmoji(EmojiTypes emojiType)
        {
            switch (emojiType)
            {
                case EmojiTypes.CLOWN:
                    return new ClownEmoji();

                case EmojiTypes.SMILE:
                    return new SmileEmoji();

                default:
                    Console.WriteLine("No matching emoji found, construction default!");
                    return new SmileEmoji();
            }
        }
    }

    class SmileEmoji : Emoji
    {

        public void SayLine()
        {
            Console.WriteLine("I'm smiling!");
        }
    }

    class ClownEmoji : Emoji
    {
        public void SayLine()
        {
            Console.WriteLine("I am a clown hahaha!");
        }
    }

    class Program
    {
        //the client in this scenario
        static void Main(string[] args)
        {
            //instead of having to instantiate objects here with if(desiredEmoji == EmojiType.CLOWN){emoji = new ClownEmoji();} else if ... etc.
            //we can just pass in the associated type enum of the object we want to create
            EmojiTypes desiredEmoji = EmojiTypes.CLOWN;
            Emoji emoji = EmojiFactory.CreateEmoji(desiredEmoji);
            emoji.SayLine();
            Console.WriteLine("Okay, their part is done, let us reuse this object and replace it with our next desired emoji.");
            desiredEmoji = EmojiTypes.SMILE;
            emoji = EmojiFactory.CreateEmoji(desiredEmoji);
            emoji.SayLine();
            Console.ReadKey();
        }
    }
}
