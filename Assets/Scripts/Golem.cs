using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    [SerializeField] private AudioClip _bossAudioMusic;
    [SerializeField] private GameObject _dustVFX;

    private Transform _childGolemSprite;
    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _childGolemSprite = this.transform.GetChild(0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<CharacterController2D>()) {
            _animator.SetTrigger("Erupt");
            GameObject dustVFX = Instantiate(_dustVFX, new Vector3(transform.position.x, transform.position.y + 3f, 0), Quaternion.identity);
            dustVFX.GetComponent<AudioSource>().Play();
            Camera.main.gameObject.GetComponent<AudioSource>().clip = _bossAudioMusic;
            Camera.main.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
