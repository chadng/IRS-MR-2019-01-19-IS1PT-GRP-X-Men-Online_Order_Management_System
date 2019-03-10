using System;

namespace doremi.Entities
{
    public class Person
    {
        public bool IsMan { get; set; }
        public String Title { get; set; }

        public Person(bool IsMan, String Title) {
            this.IsMan = IsMan;
            this.Title = Title;
        }

        public void SetTitle(String Title) {
            this.Title = Title;
        }
    }
}