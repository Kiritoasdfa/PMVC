using System.Collections;
using System.Collections.Generic;
using OrderSystem;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

public class CookCommand : SimpleCommand
{

    public override void Execute(INotification notification)
    {
        CookProxy cookProxy = Facade.RetrieveProxy(CookProxy.NAME) as CookProxy; //厨师的代理
        
        if (notification.Type == "Busy")
        {
            Order order = notification.Body as Order;
            cookProxy.CookCooking(order);
        }
        if (notification.Type == "Rester")
        {
            CookItem cookitem = notification.Body as CookItem;
            cookProxy.ChangeCookState(cookitem);
        }
        //cookProxy.CookCooking(order);
    }
}
