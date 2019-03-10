using doremi.DAL;
using doremi.Models;
using doremi.Services;
using NRules.Fluent.Dsl;

namespace doremi.Rules
{
    [Priority(90)]
    public class BalanceCheckingRule : Rule
    {
        public override void Define()
        {
            SalesOrder salesOrder = null;
            When()
                .Match<SalesOrder>(() => salesOrder, so => so.OrderProgressTypeId == OrderProgressStatus.CONFIRMED && !so.IsBulkOrder);
            Then()
                .Do(ctx => salesOrder.CheckBalance())
                .Do(ctx => ctx.Update(salesOrder))
                .Do(ctx => new LogHelper().Log(salesOrder.SalesOrderName + " has been processed by BalanceCheckingRule"));
        }
    }
}