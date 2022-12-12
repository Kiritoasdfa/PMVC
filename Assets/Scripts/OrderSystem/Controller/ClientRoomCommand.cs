using System.Collections;
using System.Collections.Generic;
using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class ClientRoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        RoomProxy roomproxy = Facade.RetrieveProxy(RoomProxy.NAME) as RoomProxy;
        if (notification.Type == "Full")
        {
            Debug.Log("入房");
            roomproxy.ChangeRoom( );
        }
        else if (notification.Type == "null")
        {
            Debug.Log("空房");
            roomproxy.RemoveRooms(notification.Body as RoomItem);
        }
    }
}
