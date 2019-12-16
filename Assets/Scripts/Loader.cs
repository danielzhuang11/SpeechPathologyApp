using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Loader : MonoBehaviour
{

    private int progress = 0;
    List<string> languages = new List<string>();

    void Initialize()
    {

    }

    public void Load()
    {
        StartCoroutine(CSVDownloader.DownloadData(AfterDownload));
    }

    public void AfterDownload(string data)
    {
        if (null == data)
        {
            Debug.LogError("Was not able to download data or retrieve stale data.");
            // TODO: Display a notification that this is likely due to poor internet connectivity
            //       Maybe ask them about if they want to report a bug over this, though if there's no internet I guess they can't
        }
        else
        {
            StartCoroutine(ProcessData(data, AfterProcessData));
        }
    }

    private void AfterProcessData(string errorMessage)
    {
        if (null != errorMessage)
        {
            Debug.LogError("Was not able to process data: " + errorMessage);
            // TODO: 
        }
        else
        {

        }
    }

    public IEnumerator ProcessData(string data, System.Action<string> onCompleted)
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        GetWords.termData = new TermData.Terms();

        // Line level
        int currLineIndex = 0;
        bool inQuote = false;
        int linesSinceUpdate = 0;
        int kLinesBetweenUpdate = 15;

        // Entry level
        string currEntry = "";
        int currCharIndex = 0;
        bool currEntryContainedQuote = false;
        List<string> currLineEntries = new List<string>();

        // "\r\n" means end of line and should be only occurence of '\r' (unless on macOS/iOS in which case lines ends with just \n)
        char lineEnding = IsIOS() ? '\n' : '\r';
        int lineEndingLength = IsIOS() ? 1 : 2;

        while (currCharIndex < data.Length)
        {
            if (!inQuote && (data[currCharIndex] == lineEnding))
            {
                // Skip the line ending
                currCharIndex += lineEndingLength;

                // Wrap up the last entry
                // If we were in a quote, trim bordering quotation marks
                if (currEntryContainedQuote)
                {
                    currEntry = currEntry.Substring(1, currEntry.Length - 2);
                }

                currLineEntries.Add(currEntry);
                currEntry = "";
                currEntryContainedQuote = false;

                // Line ended
                ProcessLineFromCSV(currLineEntries, currLineIndex);
                currLineIndex++;
                currLineEntries = new List<string>();

                linesSinceUpdate++;
                if (linesSinceUpdate > kLinesBetweenUpdate)
                {
                    linesSinceUpdate = 0;
                    yield return new WaitForEndOfFrame();
                }
            }
            else
            {
                if (data[currCharIndex] == '"')
                {
                    inQuote = !inQuote;
                    currEntryContainedQuote = true;
                }

                // Entry level stuff
                {
                    if (data[currCharIndex] == ',')
                    {
                        if (inQuote)
                        {
                            currEntry += data[currCharIndex];
                        }
                        else
                        {
                            // If we were in a quote, trim bordering quotation marks
                            if (currEntryContainedQuote)
                            {
                                currEntry = currEntry.Substring(1, currEntry.Length - 2);
                            }

                            currLineEntries.Add(currEntry);
                            currEntry = "";
                            currEntryContainedQuote = false;
                        }
                    }
                    else
                    {
                        currEntry += data[currCharIndex];
                    }
                }
                currCharIndex++;
            }

            progress = (int)((float)currCharIndex / data.Length * 100.0f);
        }

        onCompleted(null);
    }

    private void ProcessLineFromCSV(List<string> currLineElements, int currLineIndex)
    {


        // This line contains the column headers, telling us which languages are in which column
        if (currLineIndex == 0)
        {
            //languages = new List<string>();
            //for (int columnIndex = 0; columnIndex < currLineElements.Count; columnIndex++)
            //{
            //    string currLanguage = currLineElements[columnIndex];
            //    Assert.IsTrue((columnIndex != 0 || currLanguage == "English"), "First column first row was:" + currLanguage);
            //    Assert.IsFalse(Manager.instance.translator.termData.languageIndicies.ContainsKey(currLanguage));
            //    Manager.instance.translator.termData.languageIndicies.Add(currLanguage, currLineIndex);
            //    languages.Add(currLanguage);
            //}
            //UnityEngine.Assertions.Assert.IsFalse(languages.Count == 0);
        }
        // This is a normal node
        else if (currLineElements.Count > 1)
        {
            string word = null;
            string[] terms = new string[3];

            for (int columnIndex = 0; columnIndex < currLineElements.Count; columnIndex++)
            {
                string currentTerm = currLineElements[columnIndex];
                if (columnIndex == 0)
                {
                  //  Assert.IsFalse(GetWords.termData.terms.ContainsKey(currentTerm), "Saw this term twice: " + currentTerm);
                    word = currentTerm;
                }
                else
                {
                    terms[columnIndex - 1] = currentTerm;
                }
            }
            GetWords.termData.terms[word] = terms;
          
            //print( "englishSpelling: >" + englishSpelling + "<" );
        }
        else
        {
            Debug.LogError("Database line did not fall into one of the expected categories.");
        }

        foreach (KeyValuePair<string, string[]> kvp in GetWords.termData.terms)
        {

            Debug.Log(kvp.Key);
            for(int i=0; i<3; i++)
            {
                Debug.Log(kvp.Value[i]);
            }
        }
    }

    public static bool IsIOS()
    {
#if UNITY_IOS
            return true;
#endif
        return false;
    }
}