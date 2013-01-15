namespace LispToText.Chassis.Test.TransformTest
{
    public class TestValues
    {
        #region Fields
        
        public const string BasicSingleElementText = "{book}";
        public const string BasicSingleElementWithValueText = "{book This is the title.}";
        public const string SingleElementSingleChild = "{book{title hi there}}";
        public const string SingleElementTwoChildrenWithoutBookText = "{book {title hi there.}{tagline hi thar.}}";
        public const string SingleElementTwoChildrenWithBookText = "{book This is the title. {title hi there.}{tagline hi thar.}}";
        public const string TwoElementNoChildNoTitle = "{book}{chapter}";
        public const string TwoElementNoChildWithTitle = "{book This is the book.}{chapter This is the chapter.}";
        public const string TwoElementSingleChild = "{book{title chapter 1}}{chapter{title chapter 1}}";
        public const string TwoElementWithChildrenAndTitles = "{book This is the book.{title hi there.}{tagline hi thar.}}{chapter This is the chapter.{title2 hi there2.}{tagline2 hi thar2.}}";

        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion

        #region Properties

        #endregion
    }
}