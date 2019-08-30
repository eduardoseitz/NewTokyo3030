using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro library

namespace NewIndieDev.TextAdventureSystem
{
    // This scripts controlls all the Text Adventure System UI
    public class UIController : MonoBehaviour
    {
        #region Declarations
        [Header("Dialog UI Elements")]
        [SerializeField] GameObject panelDialog;
        [SerializeField] TextMeshProUGUI txtProDialogTitle;
        [SerializeField] TextMeshProUGUI txtProDialog;
        [Header("Dialog UI Animation Setup")]
        [Range(1f,5f)]
        [SerializeField] float animationSpeedDialog = 2f;
        [Header("Choice UI Elements")]
        [SerializeField] GameObject panelChoice;
        [SerializeField] TextMeshProUGUI txtProChoicesTitle;
        [SerializeField] TextMeshProUGUI[] _txtProChoices;
        [SerializeField] Image[] _imgIconChoices;
        [SerializeField] GameObject[] buttonChoices;
        [Header("Background UI Elements")]
        [SerializeField] Image imgBackground;
        
        string currentDialogText;
        float currentDialogSpeed;
        bool isTextAnimationOver;

        GameObject _activePanel;
        #endregion

        #region Main Methods
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update() { }
        #endregion

        #region Helper Methods

            #region Panel UI
            // Switch visible panel
            void TogglePanelUI(GameObject panel)
            {
                if (_activePanel)
                {
                    _activePanel.SetActive(false);
                }
                _activePanel = panel;
                _activePanel.SetActive(true);
            }

            //
            public void ToggleDialogUI()
            {
                TogglePanelUI(panelDialog);
            }

            //
            public void ToggleChoiceUI()
            {
                TogglePanelUI(panelChoice);
            }
        #endregion

            #region Dialog UI
            // Updates the title from dialog UI
            public void UpdateDialogTitleUI(string dialogTitle)
            {
                txtProDialogTitle.text = dialogTitle;
            }

            // Updates UI dialog text and animation
            public void UpdateDialogUI(string dialogPhrase)
            {
                // Starts animation
                isTextAnimationOver = false;

                // Update text
                currentDialogText = dialogPhrase;

                // Fancy dialog text animation
                StartCoroutine("AnimateDialogText");

                // Return true when animation is over
                isTextAnimationOver = true;
            }

            IEnumerator AnimateDialogText()
            {
                txtProDialog.text = null;
                currentDialogSpeed = 0.5f / animationSpeedDialog;
                for (int character = 0; character < currentDialogText.Length; character++)
                {
                    txtProDialog.text += currentDialogText[character];
                    yield return new WaitForSeconds(currentDialogSpeed);
                }
            }

            public bool IsTextAnimationOver()
            {
                return (isTextAnimationOver) ? true : false;
            }

            // Speed up dialog text animation
            public void SpeedUpDialogUI() {
                // if animation is not already speed up
                if (currentDialogSpeed == 0.5f / animationSpeedDialog)
                    currentDialogSpeed /= 2;
            }

            //
            public void SkipDialogUI() { }
            #endregion

            #region Choice UI
            // Update and display the player story choice.options to choose from
            public void UpdateChoiceUI(Choice choice)
            {
                txtProChoicesTitle.text = choice.questionTitle;
                for (int button = 0; button < buttonChoices.Length; button++)
                {
                    if (button < choice.options.Length)
                    {
                        _txtProChoices[button].text = choice.options[button].phrase;
                        _imgIconChoices[button].sprite = choice.options[button].icon;
                        buttonChoices[button].SetActive(true);
                    }
                    else
                    {
                        buttonChoices[button].SetActive(false);
                    }
                }

                if (choice.options.Length > buttonChoices.Length)
                {
                    Debug.LogWarning("There are more choice.options than choice buttons set!");
                }
            }
            #endregion

            #region Background UI
            // Swaps current background with a new one
            public void SwapBackgorund(Background background)
            {
                if (background != null)
                {
                    imgBackground.sprite = background.sprite;
                    imgBackground.color = new Color32(241, 241, 241, 255);
                }
                else
                {
                    imgBackground.sprite = null;
                    imgBackground.color = new Color32(51, 51, 51, 255);
                }
            }
            #endregion

        #endregion

        #region Unit Test Methods
        void UnitTestAnimateDialogText()
        {
            // Update text
            currentDialogText = "Test Phrase!";

            // Fancy dialog text animation
            StartCoroutine("AnimateDialogText");
        }
        #endregion
    }
}
