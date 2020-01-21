﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Obtains the ( audio / picture / translation ) that corresponds to a given term
public static class TermData
{

    public class Terms
    {
        public List<string> groups;


        // < group, [word, url] >
        public Dictionary<string, string[]> terms;

        public Terms()
        {
            groups = new List<string>();
            terms = new Dictionary<string, string[]>();
        }
    }
}