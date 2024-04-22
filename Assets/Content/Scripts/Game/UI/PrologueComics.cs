using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Content.Scripts.Game.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PrologueComics : MonoBehaviour
    {
        public event Action OnPrologueEnd;
        public event Action PreEndEvent;
        [SerializeField] private TMP_Text textField;
        [SerializeField] private Button nextBTN;
        [SerializeField] private float fadeTime;
        [SerializeField] private CanvasGroup content;
        [Space(10)]
        [Header("Prologue text")]
        [SerializeField] private List<String> Dialogs = new List<string>();
        private CanvasGroup wrapper;
        private int dialogIndex;

        private void Awake()
        {
            wrapper = GetComponent<CanvasGroup>();
            content.DOFade(0, 0);
            textField.DOFade(0, 0);
            nextBTN.interactable = false;
        }

        public void Show()
        {
            nextBTN.onClick.AddListener(MoveNext);
            dialogIndex = 0;
            textField.text = Dialogs[dialogIndex];
            content.DOFade(1, fadeTime).OnComplete(() =>
            {
                nextBTN.interactable = true;
                textField.DOFade(1, fadeTime / 2);
            });
        }

        private void MoveNext()
        {
            dialogIndex++;
            if (dialogIndex < Dialogs.Count)
            {
                textField.DOFade(0, fadeTime / 2).OnComplete(() =>
                {
                    nextBTN.interactable = false;
                    textField.text = Dialogs[dialogIndex];
                    textField.DOFade(1, fadeTime / 2).OnComplete(() =>
                    {
                        nextBTN.interactable = true;
                    });

                });
            }
                
            else
                Hide();
        }

        public void Hide()
        {
            PreEndEvent?.Invoke();
            nextBTN.onClick.RemoveListener(MoveNext);
            nextBTN.gameObject.SetActive(false);
            content.DOFade(0, fadeTime).OnComplete(() =>
            {
                wrapper.DOFade(0, fadeTime);
                OnPrologueEnd?.Invoke();
            });
        }
    }
}