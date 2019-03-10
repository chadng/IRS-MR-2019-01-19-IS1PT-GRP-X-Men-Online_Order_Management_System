using doremi.DAL;
using doremi.Models;
using doremi.Services;
using NRules.Fluent.Dsl;

namespace doremi.Rules
{
    [Priority(70)]
    public class AutoApproveRule : Rule
    {
        public override void Define()
        {
            SalesOrder salesOrder = null;

            When()
                .Match<SalesOrder>(() => salesOrder, s => s.OrderProgressTypeId == OrderProgressStatus.BALANCE_VERIFIED && s.OrderProgressTypeId != 1 && s.Total < 1000);//for bulk order, print on demand, no need approve

            Then()
                .Do(ctx => salesOrder.SetOrderProgressTypeId(OrderProgressStatus.APPROVED, "AutoApproveRule"))
                .Do(ctx => salesOrder.DeductBalance())
                 .Do(ctx => new LogHelper().Log(salesOrder.SalesOrderName + " has been processed by SalesOrderAutoApproveRule"))
                //.Do(ctx => EmailUtil.GetInstance().SendEmailToGroup(Groups.WAREHOUST, "Order " + salesOrder.SalesOrderName + " is Approved", "Order " + salesOrder.SalesOrderName + " has been Approved"))
                .Do(ctx => ctx.Update(salesOrder))
                ;
        }
    }
}