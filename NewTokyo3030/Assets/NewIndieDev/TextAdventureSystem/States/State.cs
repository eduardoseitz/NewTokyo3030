using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewIndieDev.TextAdventureSystem
{
    // This script controlls every story state object
    [CreateAssetMenu(fileName = "NewState", menuName = "New Indie Dev/Text Adventure State")]
    public class State : ScriptableObject
    {
        [Header("Dialogs")]
        public bool dialogEnabled = true;
        public Dialog dialog;

        [Header("Choices")]
        public bool choicesEnabled = false;
        public Choice choice;

        [Header("Background")]
        public bool backgroundEnabled = false;
        public Background background;
    }

    [System.Serializable]
    public class Dialog
    {
        public string nameTitle;
        [TextArea(1, 5)] public string[] phrases;
        public State gotoState;
    }

    [System.Serializable]
    public class Choice
    {
        public string questionTitle;
        public Option[] options;
    }

    [System.Serializable]
    public class Option
    {
        [TextArea(1, 2)] public string phrase;
        public Sprite icon;
        public State gotoState;
    }

    [System.Serializable]
    public class Background
    {
        public Sprite sprite;
    }
}
