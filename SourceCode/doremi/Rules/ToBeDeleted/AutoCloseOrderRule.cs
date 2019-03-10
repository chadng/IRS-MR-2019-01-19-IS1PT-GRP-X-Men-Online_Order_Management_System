using doremi.DAL;
using doremi.Models;
using NRules.Fluent.Dsl;

namespace doremi.Rules
{
    public class AutoCloseOrderRule : Rule
    {/*
        when
            shipment is delivered
        then
            close the order of this shipment
        */

        public override void Define()
        {
            Shipment shipment = null;
            SalesOrder salesOrder = null;
            //Customer customer = null;

            When()
                .Match<Shipment>(() => shipment, sh => false && sh.IsFullShipment)//shipment is delivered
                .Query<SalesOrder>(() => salesOrder, so => so.Match<SalesOrder>(s2 => s2.SalesOrderId == shipment.SalesOrderId && s2.OrderProgressTypeId == 4));//orderprogres==shipping

            Then()
                .Do(ctx => salesOrder.SetOrderProgressTypeId(5, "xxx"))//close order
                .Do(ctx => ctx.Update(salesOrder))
                .Do(ctx => System.Console.WriteLine("AutoCloseOrderRule"))
                 .Do(ctx => new LogHelper().Log("AutoCloseOrderRule"))
                ;
        }
    }
}