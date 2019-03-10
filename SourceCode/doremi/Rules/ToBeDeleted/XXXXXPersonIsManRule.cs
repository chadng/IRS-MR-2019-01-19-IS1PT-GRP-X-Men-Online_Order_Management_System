using doremi.Entities;
using NRules.Fluent.Dsl;

namespace doremi.Rules
{
    public class PersonIsManRule : Rule
    {
        public override void Define()
        {
            Person p = null;

            When()
                .Match<Person>(() => p, person => false && person.IsMan && person.Title.Equals("Unknown"));

            Then()
                .Do(ctx => p.SetTitle("Mr.")).Do(ctx => System.Console.WriteLine(p.Title)).Do(ctx => ctx.Update(p));
        }
    }
}