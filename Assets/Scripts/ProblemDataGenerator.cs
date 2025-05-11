using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text;

public class ProblemDataGenerator : MonoBehaviour
{
    private int currentDataIdx = 0;
    private List<string> data = new List<string>();
    private int countProblemsPerToken = 10;
    ProblemFactory problemFactory;

    private List<Tuple<int, int>> exampleFractions = new List<Tuple<int, int>>();

    public static ProblemDataGenerator Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for(int i = 1; i < 9; i++)
        {
            for(int j = 1; j < 9; j++)
            {
                if (i == j)
                    continue;
                exampleFractions.Add(new Tuple<int, int>(i, j));
            }
        }

        foreach(var token in ProblemFactory.Instance.ProblemTypeTokens)
        {
            if (token == 'E' || token == 'P')
                continue;

            Debug.Log("Start with token <" + token.ToString() + ">");

            for(int i = 0; i < countProblemsPerToken; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(token.ToString());

                for (int j = 0; j < 3; j++)
                {
                    if (j == 2)
                        sb.Append("n,n-");
                    else
                        sb.Append("n,n;");
                }

                // An,n;n,n;n,n-
                //  1 3 5 7 9 11
                int countPresetValues = UnityEngine.Random.Range(0, 3);

                for (int j = 0; j < countPresetValues; j++)
                {
                    int idx;
                    do
                    {
                        idx = UnityEngine.Random.Range(1, 11);
                        if (idx % 2 == 0)
                            idx++;
                    } while (sb[idx] != 'n');

                    sb.Remove(idx, 1);
                    sb.Insert(idx, UnityEngine.Random.Range(1, 10));
                }

                // data for user
                int maxCount = 7;
                int randFractAmount = UnityEngine.Random.Range(2, 5);
                int randSinglesAmount = UnityEngine.Random.Range(2, 5);
                List<string> userInputs = new List<string>();

                if (randFractAmount + randSinglesAmount > maxCount)
                {
                    if(UnityEngine.Random.Range(1, 3) % 2 == 0)
                    {
                        randFractAmount--;
                    }
                    else
                    {
                        randSinglesAmount--;
                    }
                }

                // user fractions
                for(int j = 0; j < randFractAmount; j++)
                {
                    Tuple<int, int> pair = exampleFractions[UnityEngine.Random.Range(0, exampleFractions.Count)];
                    sb.Append(pair.Item1 + "," + pair.Item2 + ";");
                    userInputs.Add(pair.Item1 + "," + pair.Item2 + ";");
                }

                // user single values
                for (int j = 0; j < randSinglesAmount; j++)
                {
                    Tuple<int, int> pair = exampleFractions[UnityEngine.Random.Range(0, exampleFractions.Count)];
                    int selectedItem = UnityEngine.Random.Range(1, 3) % 2 == 0 ? pair.Item1 : pair.Item2;
                    userInputs.Add(selectedItem + ",n;");
                }

                for(int j = 0; j < userInputs.Count; j++)
                {
                    int removeIdx = UnityEngine.Random.Range(0, userInputs.Count);
                    string removed = userInputs[removeIdx];
                    sb.Append(removed);
                    userInputs.RemoveAt(removeIdx);
                }

                sb.Remove(sb.Length - 1, 1);
                data.Add(sb.ToString());
            }
        }
    }

    public string GetNextProblem()
    {
        string next = data[currentDataIdx % data.Count];
        currentDataIdx++;
        return next;
    }
}
