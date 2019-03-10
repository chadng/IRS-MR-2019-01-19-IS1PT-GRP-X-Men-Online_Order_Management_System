using doremi.DAL;
using doremi.Models;
using doremi.Services;
using NRules.Fluent.Dsl;

namespace doremi.Rules
{
    [Priority(100)]
    public class BulkOrderRule : Rule
    {
        public override void Define()
        {
            SalesOrder salesOrder = null;
            //Customer customer = null;

            When()
                .Match<SalesOrder>(() => salesOrder, so => so.OrderProgressTypeId == OrderProgressStatus.CONFIRMED && so.IsBulkOrder);
            Then()
                .Do(ctx => salesOrder.SetOrderProgressTypeId(OrderProgressStatus.PRINTING, "BulkOrderRule"))
                .Do(ctx => EmailUtil.GetInstance().SendEmailToGroup(Groups.OPERATION, "Printing", "Printing"))
                 .Do(ctx => new LogHelper().Log(salesOrder.SalesOrderName + " has been processed by BulkOrderRule"))
                .Do(ctx => ctx.Update(salesOrder));
        }
    }
}