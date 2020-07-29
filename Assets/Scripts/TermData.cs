using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Obtains the ( audio / picture / translation ) that corresponds to a given term
public static class TermData
{

    public class Terms
    {
        public List<string> groups;
        public Dictionary<string, int> groupScore;
        // < word, [group, url] >
        public Dictionary<string, string[]> terms;

        public Terms()
        {
            groups = new List<string>();
            groupScore = new Dictionary<string, int>();
            terms = new Dictionary<string, string[]>();
        }
    }
}