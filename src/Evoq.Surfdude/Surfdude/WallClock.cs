using System;

namespace Evoq.Surfdude
{
    internal class WallClock : IWallClock
    {
        private Func<DateTimeOffset> f;

        public WallClock(Func<DateTimeOffset> getNow)
        {
            this.f = getNow;
        }

        public DateTimeOffset Now()
        {
            return f();
        }
    }
}