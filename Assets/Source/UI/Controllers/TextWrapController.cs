#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Zetta.UI.Controllers
{
    public class TextWrapController : MonoBehaviour
    {
        [SerializeField]
        private Text text;

        public int separateAt;

        public string Text
        {
            set
            {
                text.text = AddNewlinesAt(value);
            }
        }

        private string AddNewlinesAt(string input)
        {
            var words = input.Split(' ');
            var sb = new StringBuilder();
            var currentSentenceLength = 0;
            var wordAmount = words.Length;
            for (int i = 0; i < wordAmount; i++)
            {
                var word = words[i];
                var wordLength = word.Length;
                sb.Append(word);
                if (currentSentenceLength + wordLength > separateAt)
                {
                    sb.Append("\n");
                    currentSentenceLength = 0;
                }
                else
                {
                    if (i != wordAmount - 1)
                    {
                        sb.Append(" ");
                    }
                    currentSentenceLength += wordLength;
                }
            }
            sb.Append('\n');
            return sb.ToString();
        }
    }
}
