using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Obtains the ( audio / picture / translation ) that corresponds to a given term
public static class TermData
{

    public class Terms
    {
        // < language, index of language's term within a value of termTranslations >
        public Dictionary<string, int> languageIndicies;

        // < group, [word, url] >
        public Dictionary<string, string[]> termEasy;

        public Dictionary<string, string[]> termMedium;
        public Dictionary<string, string[]> termHard;

        public Terms()
        {
            languageIndicies = new Dictionary<string, int>();
            termEasy = new Dictionary<string, string[]>();
            termMedium = new Dictionary<string, string[]>();
            termEasy = new Dictionary<string, string[]>();
        }
    }
}