using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataClasses : MonoBehaviour
{
}

namespace Data
{
    // примеры классов
    public class Checkpoint : DataClasses
    {
        public int level = 2;
        public int checkpoint = 3;
    }

    public class PlayerInfo : DataClasses
    {
        public float health;
        public int bullet;
    }
}
