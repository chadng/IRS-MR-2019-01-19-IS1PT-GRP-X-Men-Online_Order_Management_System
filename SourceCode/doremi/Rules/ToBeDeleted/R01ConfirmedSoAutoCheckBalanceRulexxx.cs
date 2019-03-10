using doremi.DAL;
using doremi.Entities;
using doremi.Models;
using NRules.Fluent.Dsl;
using System.Collections.Generic;

namespace doremi.Rules
{
    [Priority(100)]
    public class R01ConfirmedSoAutoCheckBalanceRulexxx : Rule
    {
        /*
         WHEN
            OrderProgressStatus.CONFIRMED
         THEN
            Check Balance, see if need to cancel the order

         */

        public override void Define()
        {
            SalesOrder salesOrder = null;
            When()
                .Match<SalesOrder>(() => salesOrder, s => false && s.OrderProgressTypeId == OrderProgressStatus.CONFIRMED);

            Then()
                //.Do(ctx => salesOrder.CheckBalance())//cancelled
                //.Do(ctx => ctx.Update(salesOrder))
                .Do(ctx => new LogHelper().Log("testing"));
            ;
        }
    }
}