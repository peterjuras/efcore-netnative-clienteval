using System;
using System.Collections.Generic;

namespace EmptyQueryNativeTest
{
    public class Model
    {
        public Model()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Children = new List<Child>();
        }

        public void AddChild(Child child)
        {
            this.Children.Add(child);
            child.Parent = this;
        }

        public string Id { get; set; }
        public string SomeString { get; set; }

        public List<Child> Children { get; set; }
    }

    public class Child
    {
        public Child()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Model Parent { get; set; }
        public string Id { get; set; }
        public string SomeString { get; set; }
    }

}
