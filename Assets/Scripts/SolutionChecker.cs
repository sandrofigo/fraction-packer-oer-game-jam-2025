using System;
using System.Collections.Generic;
using UnityEngine;

public class SolutionChecker : MonoBehaviour
{
    float[] inputs = new float[10];
    int inputSlots = 6;
    int possibleSolutions = 0;
    int validSolutions = 0;
    int[] currentComb;
    void CheckSolutions()
    {
        bool lastCombination = false;
        currentComb = MakeComb(inputs.Length, inputSlots);
        checkPermutations(currentComb);
        while (!lastCombination)
        {
            currentComb = Successor(currentComb, inputs.Length, inputSlots);
            checkPermutations(currentComb);
            lastCombination = IsLast(currentComb, inputs.Length, inputSlots);
        }
    }

    void checkPermutations(int[] comb)
    {
        int[] currentPerm = MakePerm(comb.Length);
        SolutionCheck(currentPerm);
        bool lastPerm = false;
        while (!lastPerm)
        {
            currentPerm = SuccessorPerm(currentPerm);
            SolutionCheck(currentPerm);
            lastPerm = IsLastPerm(currentPerm);
        }
    }

    void SolutionCheck(int[] perm)
    {
        possibleSolutions++;
        float[] solutionToCheck = new float[perm.Length];
        for (int i = 0; i < solutionToCheck.Length; i++)
        {
            solutionToCheck[i] = inputs[currentComb[perm[i]]];
        }
        /*if () 
        {
            validSolutions++;
        }*/
    }





    static int[] MakeComb(int n, int k)
    {
        int[] result = new int[k];
        for (int i = 0; i < k; i++)
            result[i] = i;
        return result;
    }

    static bool IsLast(int[] comb, int n, int k)
    {
        // is comb(8,3) like [5,6,7] ?
        if (comb[0] == n - k)
            return true;
        else
            return false;
    }
    static int[] Successor(int[] comb, int n, int k)
    {
        //int i;
        int[] result = new int[k];  // make copy
        for (int i = 0; i < k; ++i)
            result[i] = comb[i];

        int idx = k - 1;
        while (idx > 0 && result[idx] == n - k + idx)
            --idx;

        ++result[idx];

        for (int j = idx; j < k - 1; ++j)
            result[j + 1] = result[j] + 1;

        return result;
    }

    static int[] MakePerm(int order)
    {
        int[] result = new int[order];
        for (int i = 0; i < order; i++)
            result[i] = i;
        return result;
    }

    static bool IsLastPerm(int[] perm)
    {
        // is perm like [5,4,3,2,1,0] ?
        int order = perm.Length;
        if (perm[0] != order - 1) return false;
        if (perm[order - 1] != 0) return false;
        for (int i = 0; i < order - 1; ++i)
        {
            if (perm[i] < perm[i + 1])
                return false;
        }
        return true;
    }

    static int[] SuccessorPerm(int[] perm)
    {
        int order = perm.Length;  // [0,1,2,3,4] is order 5

        int[] result = new int[order];
        for (int k = 0; k < order; ++k)  // copy curr data to result
            result[k] = perm[k];

        int left, right;

        left = order - 2;  // Find left value 
        while ((result[left] > result[left + 1]) && (left >= 1))
            --left;

        right = order - 1;  // find right; first value > left
        while (result[left] > result[right])
            --right;

        int tmp = result[left];  // swap [left] and [right]
        result[left] = result[right];
        result[right] = tmp;

        int i = left + 1;  // order the tail
        int j = order - 1;
        while (i < j)
        {
            tmp = result[i];
            result[i++] = result[j];
            result[j--] = tmp;
        }

        return result;
    }
}
