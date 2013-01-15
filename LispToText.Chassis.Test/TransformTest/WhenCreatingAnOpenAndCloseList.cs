using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using LispToText.Chassis.Transform;

namespace LispToText.Chassis.Test.TransformTest
{
    public class WhenCreatingAnOpenAndCloseList
    {
        #region Fields

        #endregion

        #region Support Methods

        private void AssertThatTheItemIs(bool shouldBe, IEnumerable<OpenAndCloseItem> listToCheck, int numberToSkip)
        {
            listToCheck.Skip(numberToSkip).First().IsOpen.Should().Be(shouldBe, "Item of index " + numberToSkip + " was not " + shouldBe);
        }

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
        public void AndThereAreNoElementsAnEmptyListIsReturned()
        {
            TransformFromText.CreateAnOpenAndCloseListFromText(string.Empty).Count().Should().Be(0);
        }

        [Test]
        public void AndThereIsOneElementSoCountIsCorrect()
        {
            TransformFromText.CreateAnOpenAndCloseListFromText(TestValues.BasicSingleElementText).Count().Should().Be(2);
        }

        [Test]
        public void AndThereIsOneElementSoThereIsOneOpenAndOneCloseItem()
        {
            TransformFromText.CreateAnOpenAndCloseListFromText(TestValues.BasicSingleElementText).Count(item => item.IsOpen || !item.IsOpen).Should().Be(2);
        }

        [Test]
        public void AndThereIsAnInnerElementSoThereIsAOpenOpenCloseClosePattern()
        {

            var openAndClosedList = TransformFromText.CreateAnOpenAndCloseListFromText(TestValues.SingleElementSingleChild);

            AssertThatTheItemIs(true, openAndClosedList, 0);
            AssertThatTheItemIs(true, openAndClosedList, 1);
            AssertThatTheItemIs(false, openAndClosedList, 2);
            AssertThatTheItemIs(false, openAndClosedList, 3);
        }

        [Test]
        public void AndThereAreTwoParentElementsSoThereIsAnOpenOpenCloseCloseOpenOpenCloseClosePattern()
        {
            var openAndClosedList = TransformFromText.CreateAnOpenAndCloseListFromText(TestValues.TwoElementSingleChild);

            AssertThatTheItemIs(true, openAndClosedList, 0);
            AssertThatTheItemIs(true, openAndClosedList, 1);
            AssertThatTheItemIs(false, openAndClosedList, 2);
            AssertThatTheItemIs(false, openAndClosedList, 3);

            AssertThatTheItemIs(true, openAndClosedList, 4);
            AssertThatTheItemIs(true, openAndClosedList, 5);
            AssertThatTheItemIs(false, openAndClosedList, 6);
            AssertThatTheItemIs(false, openAndClosedList, 7);
        }

        #endregion
    }
}