using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecondAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            //Question 1
            Console.WriteLine("Intersection - Q1");
            int[] nums1 = { 1, 2, 3, 8, 9, 10 };
            int[] nums2 = { 2, 3, 4, 5, 6, 7, 3 };
            Console.WriteLine("Elements of FirstArray:{ 1, 2, 3,8,9,10 }");
            Console.WriteLine("Elements of SecondArray:{ 2, 3, 4,5,6,7,3 }");
            Console.WriteLine("Intersect Result");
            var res = Program.Intersection(nums1, nums2);
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            //Question 2 :
            Console.WriteLine("Target Value - Q2");
            int[] nums = { 1, 2, 3, 4, 5 };
            int target = 2;
            Console.WriteLine("Elements of Array: {1, 2, 3, 4, 5}");
            Console.WriteLine("Target value to be searched : 2");
            int index = Program.SearchInsert(nums, target);
            Console.WriteLine("The index of the target value is " + index);
            Console.WriteLine();

            //Question 3 :
            Console.WriteLine("Lucky Integer - Q3");
            int[] lucky = { 1, 2, 2, 3, 3, 3 };
            Console.WriteLine("Elements in the array : {1, 2, 2, 3, 3, 3}");
            Console.WriteLine("Lucky Integer is : ");
            int largestLucky = Program.FindLucky(lucky);
            Console.WriteLine(largestLucky);
            Console.WriteLine();

            //Question 4 :
            Console.WriteLine("Max Number - Q4");
            Console.WriteLine("The Maximum number generated in the sequence is ");
            int max = Program.GetMaximumGenerated(5);
            Console.WriteLine(max);
            Console.WriteLine();

            //Question 5:
            Console.WriteLine("Destination - Q5");
            IList<IList<string>> paths = new List<IList<string>>();
            List<string> city = new List<string>() { "London", "New York", "New York", "Lima", "Lima", "Sao Paulo" };
            paths.Add(city);
            string destinationCity = Program.DestCity(paths);
            Console.WriteLine(destinationCity);
            Console.WriteLine();

            //Question 6:
            Console.WriteLine("Two Sum - Q6");
            int[] arr = new int[] { 2, 7, 11, 15 };
            var items = Program.TwoSum(arr, 9);
            Console.WriteLine("The element in the array is");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            //Question 7:
            Console.WriteLine("Student Scores - Q7");
            int[][] studentScores =
                {
                    new int[] { 1,91 },new int[] { 1,92 }, new int[] { 2, 93 },
                    new int[] { 2,97 },new int[] { 1,60 }, new int[] { 2,77 },
                    new int[] { 1,65 },new int[] { 1,87 }, new int[] { 1,100 },
                    new int[] { 2,100 },new int[] { 2,76 }

                };

            var topFive = Program.HighFive(studentScores);
            //Display the result
            for (int i = 0; i < topFive.Length; i++)
            {
                System.Console.Write("Element({0}): ", i);

                for (int j = 0; j < topFive[i].Length; j++)
                {
                    System.Console.Write("{0}{1}", topFive[i][j], j == (topFive[i].Length - 1) ? "" : " ");
                }
                System.Console.WriteLine();
            }

            //Question 8 :

            Console.WriteLine("Top Five Average - Q8");
            int[] values = { 1, 2, 3, 4, 5, 6, 7 };
            var result = Program.Rotate(values, 3);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

        }


        static int[]  Intersection(int[] nums1, int[] nums2)
        {
            var set1 = new HashSet<int>(nums1);
            return nums2.Where(n => set1.Remove(n)).ToArray();
        }

        //Question 2

        static int SearchInsert(int[] nums, int target)
        {
            int i = nums.ToList().BinarySearch(target);
            return i >= 0 ? i : ~i;
        }

        //Question 3

        static int FindLucky(int[] arr)
        {
            var freq = new int[501];
            foreach (var num in arr)
                freq[num]++;

            for (int i = 500; i >= 1; i--)
                if (freq[i] == i)
                    return i;

            return -1;
        }

        //Question 4

        static int GetMaximumGenerated(int n)
        {
            if (n <= 0)
                return 0;

            var num = new int[n + 1];
            num[0] = 0;
            num[1] = 1;
            var max = 1;
            for (var i = 2; i < n + 1; i++)
            {
                if (i % 2 == 0)
                    num[2 * (i / 2)] = num[i / 2];
                else
                    num[(2 * (i / 2)) + 1] = num[i / 2] + num[i / 2 + 1];

                max = Math.Max(max, num[i]);
            }

            return max;
        }

        // Question 5


        static string DestCity(IList<IList<string>> paths)
        {
            ISet<string> fromSet = new HashSet<string>();
            foreach (var p in paths)
            {
                fromSet.Add(p[0]);
            }

            foreach (var p in paths)
            {
                if (!fromSet.Contains(p[1]))
                {
                    return p[1];
                }
            }

            return "";
        }

        //Question 6

        static int[] TwoSum(int[] numbers, int target)
        {
            var i = 0;
            var j = numbers.Length - 1;

            while (i < j)
            {
                if (numbers[i] + numbers[j] == target)
                    return new[] { i + 1, j + 1 };

                if (numbers[i] + numbers[j] < target) i++;
                else j--;
            }

            return new[] { i + 1, j + 1 };
        }

        //Question 7

        static int[][] HighFive(int[][] items)
        {
            return (from item in items
                    group item by item[0] into itemGroup
                    orderby itemGroup.Key
                    select new[]{ itemGroup.Key,
                      (from groupItem in itemGroup
                       orderby groupItem[1] descending
                       select groupItem[1]).Take(5).Sum() / 5
                      }).ToArray();
        }

        //Question 8

        static int[] Rotate(int[] nums, int k)
        {
            // Solution 1 -- Time: O(n), Space: O(n)
            int[] output = new int[nums.Length];
            int length = nums.Length;
            for (int i = 0; i < nums.Length; i++)
            {
                output[(i + k) % length] = nums[i];
            }
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = output[i];
            }
            return nums;
        }




    }



        }
    

