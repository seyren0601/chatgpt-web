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
            if(linkedlist_type.Contains("circular"))
            {
                var circularLinkedList = new
                {
                    linkedlist_type = "Circular Linked List",
                    link = "https://youtu.be/gPxL11bX-RY?list=PLrcfrbKhqmAWrIoc47jVx1eEzS9gJUdg_&t=1284"
                };
                return JsonConvert.SerializeObject(circularLinkedList);
            }
            return "";
        }
        public static string get_link_about_stack(string stack_type)
        {
            stack_type = stack_type.ToLower();
            if (stack_type.Contains("stack"))
            {
                var Stack = new
                {
                    stack_type = "Stack",
                    link = "https://www.youtube.com/watch?v=SmUYblJjpfE&list=PLrcfrbKhqmAWrIoc47jVx1eEzS9gJUdg_&index=11"
                };
                return JsonConvert.SerializeObject(Stack);
            }
            return "";
        }
        public static string get_link_about_queue(string queue_type)
        {
            queue_type = queue_type.ToLower();
            if (queue_type.Contains("priority"))
            {
                var PriorityQueue = new
                {
                    queue_type = "Priority Queue",
                    link = "https://www.youtube.com/watch?v=KGukb-Z1ebA&pp=ygUOcHJpb3JpdHkgcXVldWU%3D"
                };
                return JsonConvert.SerializeObject(PriorityQueue);
            }
            if (!queue_type.Contains("priority"))
            {
                var Queue = new
                {
                    queue_type = "Queue",
                    link = "https://www.youtube.com/watch?v=VgSIu0uiMO4&list=PLrcfrbKhqmAWrIoc47jVx1eEzS9gJUdg_&index=12"
                };
                return JsonConvert.SerializeObject(Queue);
            }
            return "";
        }
        public static string get_link_about_hash(string hash_process)
        {
            hash_process = hash_process.ToLower();
            if(hash_process.Contains("hash"))
            {
                var HashTable = new
                {
                    hash_process = "Hash Table",
                    link = "https://www.youtube.com/watch?v=uNeQ_k6qwgM&list=PLrcfrbKhqmAWrIoc47jVx1eEzS9gJUdg_&index=16"
                };
                return JsonConvert.SerializeObject(HashTable);
            }
            return "";
        }
        public static string get_link_about_DFS(string DFS_process)
        {
            DFS_process = DFS_process.ToLower();
            if(DFS_process.Contains("depth first search"))
            {
                var DepthFirstSearch = new
                {
                    DFS_process = "Depth First Search",
                    link = "https://www.youtube.com/watch?v=JAlNXyfe-p4&pp=ygUDREZT"
                };
                return JsonConvert.SerializeObject(DepthFirstSearch);
            }
            return "";
        }
        public static string get_link_about_BFS(string BFS_process)
        {
            BFS_process = BFS_process.ToLower();
            if(BFS_process.Contains("breadth first search"))
            {
                var BreadthFirstSearch = new
                {
                    BFS_process = "Breadth First Search",
                    link = "https://www.youtube.com/watch?v=bhB-GIP3tZM&t=931s&pp=ygUDQkZT"
                };
                return JsonConvert.SerializeObject(BreadthFirstSearch);
            }
            return "";
        }
        public static string get_link_about_binary_tree(string BinaryTree_process)
        {
            BinaryTree_process = BinaryTree_process.ToLower();
            if(BinaryTree_process.Contains("binary tree"))
            {
                var BinaryTree = new
                {
                    BinaryTree_process = "Binary tree",
                    link = "https://www.youtube.com/watch?v=yL7v8iOjIr0&list=PLrcfrbKhqmAWrIoc47jVx1eEzS9gJUdg_&index=14&pp=iAQB"
                };
                return JsonConvert.SerializeObject(BinaryTree);
            }
            return "";
        }
    }
}
