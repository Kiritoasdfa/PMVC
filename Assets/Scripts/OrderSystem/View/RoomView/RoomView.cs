using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace OrderSystem
{
    public class RoomView : MonoBehaviour
    {

        public UnityAction<Order> Order = null;
        public UnityAction Pay = null;
        public UnityAction CallCook = null;
        public UnityAction<RoomItem> Leave = null;

        private ObjectPool<RoomItemView> objectPool = null;
        private List<RoomItemView> rooms = new List<RoomItemView>();
        private Transform parent = null;
        private void Awake()
        {
            parent = this.transform.Find("Content");

            var prefab = Resources.Load<GameObject>("Prefabs/UI/RoomItem");
            objectPool = new ObjectPool<RoomItemView>(prefab , "RoomPool");
        }
        public void UpdateRoom(IList<RoomItem> rooms)
        {

            for (int i = 0; i < this.rooms.Count; i++)
                objectPool.Push(this.rooms[i]);

            this.rooms.AddRange(objectPool.Pop(rooms.Count));
            Move(rooms);
        }

        public void Move(IList<RoomItem> rooms)
        {
            for (int i = 0; i < this.rooms.Count; i++)
            {
                this.rooms[i].transform.SetParent(parent);
                var item = rooms[i];
                this.rooms[i].transform.Find("Id").GetComponent<Text>().text = item.ToString();
                Color color = Color.white;
                if (item.state.Equals(0))
                    color = Color.green;
                else if (item.state.Equals(1))
                {
                    color = Color.yellow;
                    StartCoroutine(RoomServing(item));
                }
                else if (item.state.Equals(2))
                    color = Color.red;

                this.rooms[i].GetComponent<Image>().color = color;
            }
        }

        IEnumerator RoomServing(RoomItem item, float time = 2)
        {
            yield return new WaitForSeconds(time);
            Leave.Invoke(item);
        }
    }
}