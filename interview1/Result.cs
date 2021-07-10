using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace interview1
{
    public class Result
    {
        public int Id { get; set; }
        public int values { get; set; }

        private List<int> myList = new List<int>();
        ////23
        //public static string text1 =
        //    "suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto";
        ////24
        //public static string text =
        //    "quia  suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto";
        ////21
        //public static string text2 =
        //    "quia et dfd suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto";

        public int GetSortedValues(string text)
        {

                var punctuation = text.Where(Char.IsWhiteSpace).Distinct().ToArray();
                var words = text.ToString().Split();
                var sorted = words.OrderBy(n => n.Length).Count();
              
                return sorted;

        }
        List<Posts> sortedList = postsList.OrderByDescending(o => o.wordcount).ToList();

        //need to print the values in ascending order
        public void GetValues(List<int> myNewList)
        {
            myNewList.Sort(myList);
         
        }



    }
}
