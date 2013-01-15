using System.Collections.Generic;
using System.Linq;
using Base.Framework.Functional;
namespace LispToText.Chassis.Transform
{
    public class TransformFromText  
    {
        #region Support Methods

        private static string RemoveEndingBraceAndTrim(string rawText)
        {
            return rawText.Replace("}", "").Trim();
        }

        private static string RetrieveTheElementName(string textToCheck, int firstSpace)
        {
            return RemoveEndingBraceAndTrim(textToCheck.Substring(0, firstSpace));
        }

        private static string RetrieveTheElementValue(string textToCheck, int firstSpace)
        {
            return RemoveEndingBraceAndTrim(new string(textToCheck.Skip(firstSpace).ToArray()));
        }

        private static int RetrieveTheIndexOfTheFirstSpaceOrTheLengthOfTheString(string toCheck)
        {
            var firstSpace = toCheck.IndexOf(' ');
            return firstSpace == -1 ? toCheck.Length : firstSpace;
        }

        private static IList<TextElement> CreateChildrenListFromTheRemainTextAndExistingList(string remainingText, IEnumerable<TextElement> existingList )
        {
            var createdChildrenFromRemainingText = CreateValueHierarchy(CreateAnOpenAndCloseListFromText(remainingText), remainingText);
            return existingList.Union(createdChildrenFromRemainingText).ToList();
        }

        #endregion

        #region Methods
        
        public static IList<OpenAndCloseItem> CreateAnOpenAndCloseListFromText(string rawText)
        {
            return
                When<IList<OpenAndCloseItem>>
                    .True(rawText.Length > 0)
                    .Then(() =>
                              {
                                  var openAndCloseList = new List<OpenAndCloseItem>();
                                  for (var i = 0; i < rawText.Length; i++)
                                  {
                                      if (rawText[i] == '{' || rawText[i] == '}')
                                      {
                                          openAndCloseList.Add(new OpenAndCloseItem(rawText[i] == '{', i));
                                      }
                                  }

                                  return openAndCloseList;
                              })
                    .Else(() => new List<OpenAndCloseItem>());
        }

        public static IList<TextElement> CreateValueHierarchy(IList<OpenAndCloseItem> openAndCloseList, string rawText)
        {
            var score = 0;
            var start = 0;

            IList<TextElement> elements = new List<TextElement>();

            for (var i = 0; i < openAndCloseList.Count(); i++)
            {
                if (score == 0)
                {
                    start = i;
                }

                score = openAndCloseList[i].IsOpen ? score + 1 : score - 1;

                if (score == 0)
                {
                    var currentChunk = rawText.Substring(openAndCloseList[start].Index, openAndCloseList[i].Index - openAndCloseList[start].Index);
                    
                    var split = currentChunk.Split('{');

                    var text = split.First(x => !string.IsNullOrEmpty(x));
                    var firstSpace = RetrieveTheIndexOfTheFirstSpaceOrTheLengthOfTheString(text);

                    currentChunk = currentChunk.Remove(0, text.Length + 1);

                    var createdElementFromText = new TextElement(RetrieveTheElementName(text, firstSpace), RetrieveTheElementValue(text, firstSpace));
                    elements.Add(createdElementFromText);

                    createdElementFromText.Children = CreateChildrenListFromTheRemainTextAndExistingList(currentChunk, createdElementFromText.Children);
                }
            }

            return elements;
        } 

        #endregion
    }
}