using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDialogueBox : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueBox;

    private AudioSource _audioSource;
    // private bool _triggered = false;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        _dialogueBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<CharacterController2D>()) {
            _dialogueBox.SetActive(true);
            _audioSource.Play();
            // _triggered = true;
            Invoke("HideDialogueBox", 6f);
        }
    }

    private void HideDialogueBox() {
        _dialogueBox.SetActive(false);
    }
}
