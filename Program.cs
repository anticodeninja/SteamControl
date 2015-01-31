namespace SteamControl
{
    using System;
    using NDesk.Options;

    class Program
    {
        private static void Main(string[] args)
        {
            var p = new OptionSet
            {
                {"s|status|IsBigPictureRunning", v => Console.WriteLine(SteamControl.IsBigPictureRunning() ? 0 : 1)},
                {"r|run|StartBigPicture", v => SteamControl.StartBigPicture()},
                {"h|halt|CloseBigPicture", v => SteamControl.CloseBigPicture()},
                {"w|wait|WaitBigPictureState=", (bool s) => SteamControl.WaitBigPictureState(s)},
                {"p|path|GetSteamPath", v => Console.WriteLine(SteamControl.GetSteamPath())},
            };
            p.Add("?|help", v => p.WriteOptionDescriptions(Console.Out));
            p.Parse(args);
        }
    }
}
