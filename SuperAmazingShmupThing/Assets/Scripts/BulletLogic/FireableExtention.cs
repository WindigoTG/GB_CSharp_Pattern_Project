namespace ShmupProject
{
    public static class FireableExtention
    {
        public static Fireable Of(this Fireable fireable, IFireable subFireable)
        {
            var lowest = GetLowest(fireable);

            lowest.SubFireable = subFireable;
            return fireable;
        }

        private static Fireable GetLowest(Fireable fireable)
        {
            var lowest = fireable;
            while (fireable != null)
            {
                lowest = fireable;
                fireable = fireable.SubFireable as Fireable;
            }

            return lowest;
        }

        public static Fireable FiredInLine(this Fireable fireable)
        {
            return new Line().Of(fireable);
        }

        public static Fireable FiredInArc(this Fireable fireable)
        {
            return new Arc().Of(fireable);
        }
    }
}