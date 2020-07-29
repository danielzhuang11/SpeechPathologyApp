using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Loader : MonoBehaviour
{

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

        WordBase.termData = new TermData.Terms();

        // Line level
        int currLineIndex = 0;
        bool inQuote = false;
        int linesSinceUpdate = 0;
        int kLinesBetweenUpdate = 15;

        // Entry level
        string currEntry = "";
        int currCharIndex = 0;
      
        List<string> currLineEntries = new List<string>();

        // "\r\n" means end of line and should be only occurence of '\r' (unless on macOS/iOS in which case lines ends with just \n)
        char lineEnding = IsIOS() ? '\n' : '\r';
        int lineEndingLength = IsIOS() ? 1 : 2;
        data += "\r\n";
        while (currCharIndex < data.Length)
        {
            if ((data[currCharIndex] == lineEnding))
            {
                // Skip the line ending
                currCharIndex += lineEndingLength;

                // Wrap up the last entry
                // If we were in a quote, trim bordering quotation marks
               

                currLineEntries.Add(currEntry);
                currEntry = "";

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
                            currLineEntries.Add(currEntry);
                            currEntry = "";
                           
                        }
                    }
                    else
                    {
                        currEntry += data[currCharIndex];
                    }
                }
                currCharIndex++;
            }
        }

        DropdownFill.groups = WordBase.termData.groups;
        DropdownFill.groups.Add("Any");
        onCompleted(null);
    }

    private static void ProcessLineFromCSV(List<string> currLineElements, int currLineIndex)
    {

        // This line contains the column headers, telling us which languages are in which column
       
        // This is a normal node
        if (currLineElements.Count > 1)
        {
            string word = null;
            string[] terms = new string[2];
            string currentGroup ="";
            for (int columnIndex = 0; columnIndex < currLineElements.Count; columnIndex++)
            {
               
                string currentTerm = currLineElements[columnIndex];
                if (columnIndex == 0)
                {
                    WordBase.termData.groups.Add(currentTerm);
                    currentGroup = currentTerm;
                    continue;
                }
                else
                {   word = currentTerm;
                    terms[0] = currentGroup;
                   

                        if (columnIndex+1 < currLineElements.Count &&  currLineElements[columnIndex + 1].IndexOf("http") > -1)
                        {

                            terms[1] = currLineElements[columnIndex + 1];
                            columnIndex += 1;

                        }
                        else 
                        { terms[1] = null; }
                  
                }

                WordBase.termData.terms[word] = new string[] {terms[0], terms[1] };
                globalScore.lo += 1;

            }
            if (WordBase.termData.groupScore.Count < WordBase.termData.groups.Count)
            {
                   for(int pos = 0; pos < WordBase.termData.groups.Count; pos++)
                   {
                       if(pos>=WordBase.termData.groupScore.Count)
                       WordBase.termData.groupScore.Add(WordBase.termData.groups[pos], PlayerPrefs.GetInt(WordBase.termData.groups[pos],0));
                    
                   }
                   
            }
            //print( "englishSpelling: >" + englishSpelling + "<" );
        }
        else
        {
            Debug.LogError("Database line did not fall into one of the expected categories.");
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