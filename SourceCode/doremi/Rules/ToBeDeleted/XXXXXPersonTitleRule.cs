using doremi.Entities;
using NRules.Fluent.Dsl;

namespace doremi.Rules
{
    public class PersonTitleRule : Rule
    {
        public override void Define()
        {
            Person p = null;

            When()
                .Match<Person>(() => p, person => false && person.Title.Equals("Mr."));

            Then()
                .Do(ctx => System.Console.WriteLine("Hi Mr."))
                 .Do(ctx => p.SetTitle("Hi Mr."))
                 .Do(ctx => ctx.Update(p));
            ;
        }
    }
}