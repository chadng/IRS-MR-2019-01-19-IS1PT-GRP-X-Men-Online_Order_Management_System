using doremi.DAL;
using doremi.Models;
using doremi.Services;
using NRules.Fluent.Dsl;

namespace doremi.Rules
{
    [Priority(80)]
    public class BestDiscountRule : Rule
    {
        public override void Define()
        {
            SalesOrder salesOrder = null;
            When()
                .Match<SalesOrder>(() => salesOrder, so => so.OrderProgressTypeId == OrderProgressStatus.BALANCE_VERIFIED && so.SalesTypeId == 1);//1 is null
            Then()
                .Do(ctx => salesOrder.SetBestDiscount())
                .Do(ctx => ctx.Update(salesOrder))
                .Do(ctx => new LogHelper().Log(salesOrder.SalesOrderName + " has been processed by BestDiscountRule"));
        }
    }
}