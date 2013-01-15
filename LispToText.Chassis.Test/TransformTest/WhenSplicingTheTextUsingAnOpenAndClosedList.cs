using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LispToText.Chassis.Transform;
using NUnit.Framework;

namespace LispToText.Chassis.Test.TransformTest
{
    public class WhenSplicingTheTextUsingAnOpenAndClosedList
    {

        #region Fields

        #endregion

        #region Support Methods

        #endregion

        #region Test Hooks

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        #endregion

        #region Test Methods

        [Test]
        public void ItWillConvertASingleElementToAList()
        {
            TransformFromText
                .CreateValueHierarchy(TransformFromText.CreateAnOpenAndCloseListFromText(TestValues.BasicSingleElementText).ToList(), TestValues.BasicSingleElementText)
                .Should()
                .Match(x => x.Any(y => y.Name == "book"));
        }

        [Test]
        public void ItWillSetTheElementValueToTheRemainingText()
        {
            TransformFromText
                .CreateValueHierarchy(TransformFromText.CreateAnOpenAndCloseListFromText(TestValues.BasicSingleElementWithValueText).ToList(), TestValues.BasicSingleElementWithValueText)
                .Should()
                .Match(x => x.Any(y => y.Name == "book" && y.Value == "This is the title."));
        }   

        [Test]
        public void ItHasNoValueButHasTwoChildren()
        {
            var createValueHierarchy = TransformFromText.CreateValueHierarchy(TransformFromText.CreateAnOpenAndCloseListFromText(TestValues.SingleElementTwoChildrenWithoutBookText).ToList(), TestValues.SingleElementTwoChildrenWithoutBookText);

            createValueHierarchy[0].Name.Should().Be("book");
            createValueHierarchy[0].Value.Should().Be("");
            createValueHierarchy[0].Children[0].Name.Should().Be("title");
            createValueHierarchy[0].Children[0].Value.Should().Be("hi there.");
            createValueHierarchy[0].Children[1].Name.Should().Be("tagline");
            createValueHierarchy[0].Children[1].Value.Should().Be("hi thar.");
        }

        [Test]
        public void ItHasAValueAndItHasTwoChildren()
        {
            var createValueHierarchy = TransformFromText.CreateValueHierarchy(TransformFromText.CreateAnOpenAndCloseListFromText(TestValues.SingleElementTwoChildrenWithBookText).ToList(), TestValues.SingleElementTwoChildrenWithBookText);

            createValueHierarchy[0].Name.Should().Be("book");
            createValueHierarchy[0].Value.Should().Be("This is the title.");
            createValueHierarchy[0].Children[0].Name.Should().Be("title");
            createValueHierarchy[0].Children[0].Value.Should().Be("hi there.");
            createValueHierarchy[0].Children[1].Name.Should().Be("tagline");
            createValueHierarchy[0].Children[1].Value.Should().Be("hi thar.");
        }

        [Test]
        public void ThereAreTwoParentsAndNoChildrenNoTitles()
        {
            var createValueHierarchy = TransformFromText.CreateValueHierarchy(TransformFromText.CreateAnOpenAndCloseListFromText(TestValues.TwoElementNoChildNoTitle).ToList(), TestValues.TwoElementNoChildNoTitle);

            createValueHierarchy[0].Name.Should().Be("book");
            createValueHierarchy[0].Value.Should().Be("");
            createValueHierarchy[1].Name.Should().Be("chapter");
            createValueHierarchy[1].Value.Should().Be("");
        }

        [Test]
        public void ThereAreTwoParentsAndNoChildrenWithTitles()
        {
            var createValueHierarchy = TransformFromText.CreateValueHierarchy(TransformFromText.CreateAnOpenAndCloseListFromText(TestValues.TwoElementNoChildWithTitle).ToList(), TestValues.TwoElementNoChildWithTitle);

            createValueHierarchy[0].Name.Should().Be("book");
            createValueHierarchy[0].Value.Should().Be("This is the book.");
            createValueHierarchy[1].Name.Should().Be("chapter");
            createValueHierarchy[1].Value.Should().Be("This is the chapter.");
        }

        [Test]
        public void ThereAreTwoParenttsWithChildrenAndTitles()
        {
            var createValueHierarchy = TransformFromText.CreateValueHierarchy(TransformFromText.CreateAnOpenAndCloseListFromText(TestValues.TwoElementWithChildrenAndTitles).ToList(), TestValues.TwoElementWithChildrenAndTitles);

            createValueHierarchy[0].Name.Should().Be("book");
            createValueHierarchy[0].Value.Should().Be("This is the book.");
            createValueHierarchy[0].Children.Count.Should().Be(2);
            createValueHierarchy[0].Children[0].Name.Should().Be("title");
            createValueHierarchy[0].Children[0].Value.Should().Be("hi there.");
            createValueHierarchy[0].Children[1].Name.Should().Be("tagline");
            createValueHierarchy[0].Children[1].Value.Should().Be("hi thar.");

            createValueHierarchy[1].Name.Should().Be("chapter");
            createValueHierarchy[1].Value.Should().Be("This is the chapter.");
            createValueHierarchy[1].Children.Count.Should().Be(2);
            createValueHierarchy[1].Children[0].Name.Should().Be("title2");
            createValueHierarchy[1].Children[0].Value.Should().Be("hi there2.");
            createValueHierarchy[1].Children[1].Name.Should().Be("tagline2");
            createValueHierarchy[1].Children[1].Value.Should().Be("hi thar2.");
        }

        #endregion
    }
}