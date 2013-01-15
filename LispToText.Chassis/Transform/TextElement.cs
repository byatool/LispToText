using System.Collections.Generic;

namespace LispToText.Chassis.Transform
{
    public class TextElement
    {
        public TextElement(string name, string value)
        {
            Name = name;
            Value = value;
            Children = new List<TextElement>();
        }

        public string Name { get; private set; }

        public string Value { get; private set; }

        public IList<TextElement> Children { get; set; }
    }
}