using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShmupProject
{
    public static class FireableExtention
    {
        public static Fireable Of(this Fireable fireable, IFireable SubFireable)
        {
            fireable.SubFireable = SubFireable;
            return fireable;
        }
    }
}