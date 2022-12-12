using System;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

namespace OrderSystem
{
    public class RoomMediator : Mediator
    {
        private RoomProxy roomProxy = null;
        public new const string NAME = "RoomMediator";
        public RoomView RoomView
        {
            get { return (RoomView)base.ViewComponent; }
        }
        public RoomMediator(RoomView view) : base(NAME, view)
        {
            RoomView.Leave += item =>{SendNotification(OrderCommandEvent.Clent_EnterRoom, item, "null");};
        }
        public override void OnRegister()
        {
            base.OnRegister();
            roomProxy = Facade.RetrieveProxy(RoomProxy.NAME) as RoomProxy;
            if (null == roomProxy)
                throw new Exception(WaiterProxy.NAME + "is null,please check it!");

            RoomView.UpdateRoom(roomProxy.Rooms);
        }
        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            //notifications.Add(OrderCommandEvent.Clent_EnterRoom);
            notifications.Add(OrderSystemEvent.ResfrshRoom);
            return notifications;
        }

        public override void HandleNotification(INotification notification)
        {
            Debug.Log(notification.Name);
            switch (notification.Name)
            {
                case OrderSystemEvent.ResfrshRoom:
                    roomProxy = Facade.RetrieveProxy(RoomProxy.NAME) as RoomProxy;
                    RoomView.Move(roomProxy.Rooms);//刷新一下房子的状态
                    break;
            }
        }

    }
}
