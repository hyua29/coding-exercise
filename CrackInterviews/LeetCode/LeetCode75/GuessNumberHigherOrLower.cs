// /** 
//  * Forward declaration of guess API.
//  * @param  num   your guess
//  * @return 	     -1 if num is higher than the picked number
//  *			      1 if num is lower than the picked number
//  *               otherwise return 0
//  * int guess(int num);
//  */
//
// public class Solution : GuessGame {
//     public int GuessNumber(int n) {
//
//         var left = 1;
//         var right = n;
//
//         var mid = left;
//
//         var result = 0;
//         do
//         {
//             
//             mid = left + (right - left) / 2;
//            
//             result = guess(mid);
//
//             if (result > 0)
//             {
//                 left = mid + 1;
//             }
//             else if (result < 0)
//             {
//                 right = mid - 1;
//             }
//         }   
//         while (result != 0);
//
//         return mid;
//     }
// }