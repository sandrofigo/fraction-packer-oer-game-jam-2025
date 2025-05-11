using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using System.Linq;

public class ProblemDataGenerator : MonoBehaviour
{
    private int currentDataIdx = 0;
    private List<string> data = new List<string>();
    private int countProblemsPerToken = 10;

    public TextAsset txtFile = null;
    public bool useTxtFile = false;

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
        if(useTxtFile && txtFile != null)
        {
            ParseTxtFile();
        }
        else
        {
            ChaosGeneration();
        }
    }



    public string GetNextProblem()
    {
        string next = data[currentDataIdx % data.Count];
        currentDataIdx++;
        return next;
    }

    private void ParseTxtFile()
    {
        data = txtFile.text.Split(Environment.NewLine).ToList();
    }

    private void ChaosGeneration()
    {
        for (int i = 1; i < 9; i++)
        {
            for (int j = 1; j < 9; j++)
            {
                if (i == j)
                    continue;
                exampleFractions.Add(new Tuple<int, int>(i, j));
            }
        }

        foreach (var token in ProblemFactory.Instance.ProblemTypeTokens)
        {
            Debug.Log("Start with token <" + token.ToString() + ">");

            for (int i = 0; i < countProblemsPerToken; i++)
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
                    if (UnityEngine.Random.Range(1, 3) % 2 == 0)
                    {
                        randFractAmount--;
                    }
                    else
                    {
                        randSinglesAmount--;
                    }
                }

                // user fractions
                for (int j = 0; j < randFractAmount; j++)
                {
                    Tuple<int, int> pair = exampleFractions[UnityEngine.Random.Range(0, exampleFractions.Count)];
                    userInputs.Add(pair.Item1 + "," + pair.Item2 + ";");
                }

                // user single values
                for (int j = 0; j < randSinglesAmount; j++)
                {
                    Tuple<int, int> pair = exampleFractions[UnityEngine.Random.Range(0, exampleFractions.Count)];
                    int selectedItem = UnityEngine.Random.Range(1, 3) % 2 == 0 ? pair.Item1 : pair.Item2;
                    userInputs.Add(selectedItem + ",n;");
                }

                while (userInputs.Count > 0)
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

        // write data to txt
        int filePrefix = UnityEngine.Random.Range(0, 100);
        string fileName = filePrefix + "_data.txt";

        using (TextWriter tw = new StreamWriter(Application.persistentDataPath + "/" + fileName))
        {
            foreach (var line in data)
                tw.WriteLine(line);
        }
    }
}
