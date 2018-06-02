using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour {

    public enum SourceType {
        Image,
        SpriteRenderer
    }

    [Header("Settings")]
    public SourceType sourceType;
    public Image image;
    public SpriteRenderer spriteRenderer;
    public float frameRate = 0.2f;

    private Sprite[] _currentAnimation;

    [Header("Debug Infos")]
    [SerializeField]
    private int _currentFrameIndex;

    private float _nextFrameTime;

    IEnumerator Play() {
        while (_currentAnimation != null && Time.time >= _nextFrameTime) {
            _nextFrameTime = Time.time + frameRate;

            switch (sourceType) {
                case SourceType.Image:
                if (_currentFrameIndex < _currentAnimation.Length) {
                    image.sprite = _currentAnimation[_currentFrameIndex++];
                    if (_currentFrameIndex >= _currentAnimation.Length)
                        _currentFrameIndex = 0;
                }
                break;
                case SourceType.SpriteRenderer:
                if (_currentFrameIndex < _currentAnimation.Length) {
                    spriteRenderer.sprite = _currentAnimation[_currentFrameIndex++];
                    if (_currentFrameIndex >= _currentAnimation.Length)
                        _currentFrameIndex = 0;
                }
                break;
            }
            yield return new WaitForSeconds(frameRate);
        }
    }

    private void PlayOneShot() {
        Stop();

        switch (sourceType) {
            case SourceType.Image:
            image.sprite = _currentAnimation[0];
            break;
            case SourceType.SpriteRenderer:
            spriteRenderer.sprite = _currentAnimation[0];
            break;
        }
    }

    public void Play(Sprite[] anim) {
        this._currentAnimation = anim;

        if (this._currentAnimation.Length > 1)
            StartCoroutine(Play());
        else
            PlayOneShot();
    }

    public void Stop() {
        StopAllCoroutines();
    }

    public Sprite[] GetCurrentAnimation() {
        return _currentAnimation;
    }
}