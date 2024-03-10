using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ChatGPTCaller.DAL
{
    public class Functions
    {
        public static string get_link_about_sort(string sort_type)
        {
            sort_type = sort_type.ToLower();
            if (sort_type.Contains("quick sort"))
            {
                var quickSort = new
                {
                    sort_type = "Quick sort",
                    link = "https://youtu.be/Gyj8fd4DBpc?list=PLrcfrbKhqmAWrIoc47jVx1eEzS9gJUdg_"
                };
                return JsonConvert.SerializeObject(quickSort);
            }
            if (sort_type.Contains("bubble sort"))
            {
                var bubbleSort = new
                {
                    sort_type = "Bubble sort",
                    link = "https://www.youtube.com/watch?v=xli_FI7CuzA&pp=ygULYnViYmxlIHNvcnQ%3D"
                };
                return JsonConvert.SerializeObject(bubbleSort);
            }
            if (sort_type.Contains("insertion sort"))
            {
                var insertionSort = new
                {
                    sort_type = "Insertion sort",
                    link = "https://www.youtube.com/watch?v=JU767SDMDvA&pp=ygUOaW5zZXJ0aW9uIHNvcnQ%3D"
                };
                return JsonConvert.SerializeObject(insertionSort);
            }
            if (sort_type.Contains("merge sort"))
            {
                var mergeSort = new
                {
                    sort_type = "Merge sort",
                    link = "https://www.youtube.com/watch?v=4VqmGXwpLqc&pp=ygUKbWVyZ2Ugc29ydA%3D%3D"
                };
                return JsonConvert.SerializeObject(mergeSort);
            }
            return "";
        }

        public static string get_link_about_search(string search_type)
        {
            search_type = search_type.ToLower();
            if (search_type.Contains("linear search"))
            {
                var linearSearch = new
                {
                    search_type = "Linear search",
                    link = "https://www.youtube.com/watch?v=YvAosi_pZ8w&list=PLrcfrbKhqmAWrIoc47jVx1eEzS9gJUdg_&index=3&pp=iAQB"
                };
                return JsonConvert.SerializeObject(linearSearch);
            }
            if (search_type.Contains("binary search"))
            {
                var binarySearch = new
                {
                    search_type = "Binary search",
                    link = "https://youtu.be/YvAosi_pZ8w?list=PLrcfrbKhqmAWrIoc47jVx1eEzS9gJUdg_&t=2365"
                };
                return JsonConvert.SerializeObject(binarySearch);
            }
            return "";
        }

        public static string get_link_about_linkedlist(string  linkedlist_type)
        {
            linkedlist_type = linkedlist_type.ToLower();
            if(linkedlist_type.Contains("single"))
            {
                var singleLinkedList = new
                {
                    linkedlist_type = "Single Linked List",
                    link = "https://youtu.be/4imz17FNr9k?list=PLrcfrbKhqmAWrIoc47jVx1eEzS9gJUdg_"
                };
                return JsonConvert.SerializeObject(singleLinkedList);
            }
            if (linkedlist_type.Contains("double"))
            {
                var doubleLinkedList = new
                {
                    linkedlist_type = "Double Linked List",
                    link = "https://youtu.be/gPxL11bX-RY?list=PLrcfrbKhqmAWrIoc47jVx1eEzS9gJUdg_"
                };
                return JsonConvert.SerializeObject(doubleLinkedList);
            }
            return "";
        }
    }
}
