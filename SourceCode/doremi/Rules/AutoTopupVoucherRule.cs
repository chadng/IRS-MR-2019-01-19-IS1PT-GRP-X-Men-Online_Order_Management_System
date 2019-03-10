using doremi.DAL;
using doremi.Models;
using NRules.Fluent.Dsl;
using System;

namespace doremi.Rules
{
    [Priority(60)]
    public class AutoTopupVoucherRule : Rule
    {
        /*
        when
            order is closed
        then
            topup the voucher to user account and set the order status to redeemed
        */

        public override void Define()
        {
            SalesOrder salesOrder = null;
            When()
                .Match<SalesOrder>(() => salesOrder, s => s.OrderProgressTypeId == OrderProgressStatus.CLOSED);

            ;

            Then()
                .Do(ctx => salesOrder.AutoTopupCreditPoints())
                .Do(ctx => new LogHelper().Log(salesOrder.SalesOrderName + "has been processed by AutoTopupVoucherRule"));
        }

        //private void Apply(SalesOrder so, Shipment sh)
        //{
        //    so.SetOrderProgressTypeId(5);//5 closed
        //    sh.SalesOrderId = so.SalesOrderId;
        //    sh.IsFullShipment = true;
        //}
    }
}