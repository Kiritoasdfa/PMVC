
/*=========================================
* Author: Administrator
* DateTime:2017/6/21 18:17:11
* Description:$safeprojectname$
==========================================*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using PureMVC.Patterns;

namespace OrderSystem
{
    public class CookProxy : Proxy
    {
        public new const string NAME = "CookProxy";
        public Queue<Order> WaitforCookOrder = new Queue<Order>();
        public IList<CookItem> Cooks
        {
            get { return (IList<CookItem>) base.Data; }
        }

        
       
        public CookProxy( ) : base(NAME , new List<CookItem>())
        {
            AddCook(new CookItem(1 , "强尼" , 0));
            AddCook(new CookItem(2 , "托尼"));
            AddCook(new CookItem(3 , "鲍比" , 0));
            //AddCook(new CookItem(4 , "缇米"));
            //AddCook(new CookItem(5 , "Bob"));
        }
        public void AddCook( CookItem item )
        {
            Cooks.Add(item);
        }
        public void RemoveCook( CookItem item )
        {
            Cooks.Remove(item);
        }
        public void ChangeCookState(CookItem cookitem)
        {
            GetCookItem(cookitem.id).state = 0;
            if (WaitforCookOrder.Count != 0)
            {
                CookCooking(WaitforCookOrder.Dequeue());
                return;
            }
            SendNotification(OrderSystemEvent.ResfrshCook);
        }
        public CookItem GetCookItem(int id)
        {
            for (int i = 0; i < Cooks.Count; i++)
            {
                if (Cooks[i].id == id)
                {
                    return Cooks[i];
                }
            }
            return null;
        }
        public void CookCooking(Order order)
        {
            for (int i = 0; i < Cooks.Count; i++)
            {
                if (Cooks[i].state == 0)//找到非忙碌厨师改变其状态
                {
                    Cooks[i].state=1;
                    Cooks[i].cooking = order.names;//厨师抄的什么菜
                    Cooks[i].cookOrder = order;// 厨师炒菜的菜单
                    UnityEngine.Debug.Log(order.names);
                    SendNotification(OrderSystemEvent.ResfrshCook);//找到空闲厨师去刷新一下厨师显示的状态
                    return;
                }
            }
            WaitforCookOrder.Enqueue(order);
        }
    }
}