using doremi.Models;
using NRules.Fluent.Dsl;
using System;

namespace doremi.Rules
{
    [Priority(90)]
    public class R02ConfirmedSoAutoSetDiscountRule : Rule
    {/*
        when
            OrderProgressStatus.CONFIRMED
        then
            set the best discount in this period
        */

        public override void Define()
        {
            SalesOrder salesOrder = null;
            When()
                .Match<SalesOrder>(() => salesOrder, s => false && s.OrderProgressTypeId == OrderProgressStatus.CONFIRMED);

            Then()
                .Do(ctx => salesOrder.SetBestDiscount())//cancelled
                .Do(ctx => ctx.Update(salesOrder))
                ;
        }
    }
}