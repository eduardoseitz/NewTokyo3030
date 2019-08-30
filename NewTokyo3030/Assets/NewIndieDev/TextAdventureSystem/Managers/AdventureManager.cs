using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewIndieDev.TextAdventureSystem
{
    // This scripts controlls all the Text Advanture logic
    [RequireComponent(typeof(UIController))]
    [AddComponentMenu(menuName: "Text Advanture Manager")]
    public class AdventureManager : MonoBehaviour
    {
        #region Declarations
        // Editor declarations
        [Header("Adventure Setup")]
        [SerializeField] State startState;

        // Local declarations
        int _currentPhrase;
        State _currentState;

        // Script reference declarations
        UIController uiController;

        #endregion

        #region Main Methods
        void Awake()
        {
            uiController = GetComponent<UIController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            StartGame();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UpdateDialog();
            }
        }
        #endregion

        #region Helper Methods
        //
        void StartGame()
        {
            if (startState)
            {
                _currentState = startState;
                LoadNextState();
            }
            else
            {
                Debug.LogWarning("[TextAdvantureManager] First State must be set!");
            }
        }

        // Loads next block of story
        void LoadNextState()
        {
            if (_currentState.backgroundEnabled)
            {
                uiController.SwapBackgorund(_currentState.background);
            }

            // Check current state type
            if (_currentState.dialogEnabled)
            {
                uiController.UpdateDialogTitleUI(_currentState.dialog.nameTitle);
                uiController.ToggleDialogUI();
                UpdateDialog();
            }
            else if (_currentState.choicesEnabled)
            {
                UpdateChoice();
                uiController.ToggleChoiceUI();
            }
        }

        // What happens when players chooses to see the next dialog
        public void UpdateDialog()
        {
            if (_currentPhrase < _currentState.dialog.phrases.Length)
            {
                // If current text animation is over
                if (uiController.IsTextAnimationOver())
                {
                    uiController.UpdateDialogUI(_currentState.dialog.phrases[_currentPhrase]);
                    _currentPhrase++;
                }
                // If animation is not over than speed it up
                else
                {
                    uiController.SpeedUpDialogUI();
                }
            }
            else
            {
                _currentPhrase = 0;
                if (_currentState.dialog.gotoState)
                {
                    _currentState = _currentState.dialog.gotoState;
                    LoadNextState();
                }
                else
                {
                    Debug.LogWarning("Next State must be set in " + _currentState.name + "Current State property.");
                }
            }
        }

        //
        void UpdateChoice()
        {
            uiController.UpdateChoiceUI(_currentState.choice);
        }

        public void SelectChoice(int choiceNumber)
        {
            if (_currentState.choice.options[choiceNumber].gotoState)
            {
                _currentState = _currentState.choice.options[choiceNumber].gotoState;
                LoadNextState();
            }
            else
            {
                Debug.LogWarning(_currentState.name + " has a choice go to state not set!");
            }
        }
        #endregion
    }
}
