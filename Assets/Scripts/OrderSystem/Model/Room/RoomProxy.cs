using System.Collections.Generic;
using PureMVC.Patterns;
using UnityEngine;

namespace OrderSystem
{
    public class RoomProxy : Proxy
    {
        public new const string NAME = "RoomProxy";

        public IList<RoomItem> Rooms
        {
            get { return (IList<RoomItem>)base.Data; }
        }
        public RoomProxy() : base(NAME, new List<RoomItem>())
        {
            AddClient(new RoomItem(1,  0));
            AddClient(new RoomItem(2,  0));
            AddClient(new RoomItem(3, 0));
            AddClient(new RoomItem(4, 0));
            //AddClient(new RoomItem(5,  0));
        }
        public void AddClient(RoomItem item)
        {
            Rooms.Add(item);
        }
        public void RemoveRooms(RoomItem item)
        {

            for (int i = 0; i < Rooms.Count; i++)
            {
                if (item.id == Rooms[i].id)
                {
                    Rooms[i].state = 0;
                    SendNotification(OrderSystemEvent.ResfrshRoom);
                    return;
                }
            }
        }

        public void ChangeRoom()
        {

            //UnityEngine.Debug.LogWarning(Waiters[0].name+""+ Waiters[0].state);
            //UnityEngine.Debug.LogWarning(Waiters[1].name + "" + Waiters[1].state);
            //UnityEngine.Debug.LogWarning(Waiters[2].name + "" + Waiters[2].state);
            RoomItem item = GetIdleRoom();
            if (item != null)
            {
                item.state = 1;
                SendNotification(OrderSystemEvent.ResfrshRoom);
               
            }
        }
        private RoomItem GetIdleRoom()
        {
            foreach (RoomItem roomitem in Rooms)
                if (roomitem.state.Equals((int)E_RoomtState.EmptyRoom))
                    return roomitem;
            UnityEngine.Debug.LogWarning("暂无空闲房间请稍等..");
            return null;
        }
    }
}
