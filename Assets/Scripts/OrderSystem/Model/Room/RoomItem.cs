using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OrderSystem
{
    public enum E_RoomtState
    {
        EmptyRoom=0,
        ChectRoom=1,

    }
    public class RoomItem 
    {
        public int id { get; set; }

        public int state;
        public RoomItem(int id, int state = 0)
        {
            this.id = id;
            this.state = state;
        }
        public override string ToString()
        {
            return id + "号房间\n"  + "\n" + resultState();
        }
        private string resultState()
        {
            if (state.Equals(0))
                return "空的";
            return "满了";
        }

    }
}
