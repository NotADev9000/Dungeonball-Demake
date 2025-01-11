using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Transitioner : MonoBehaviour
{
    // SINGLETON SETUP ////////////////////////////
    // Prefab Singleton Snippet From: https://discussions.unity.com/t/self-creating-singleton-from-a-prefab/590233/4
    private static UI_Transitioner _instance;
    public static UI_Transitioner Instance
    {
        get
        {
            if (!_instance)
            {
                // load transitioner prefab which includes all UI elements
                var transitionerPrefab = Resources.Load<GameObject>("UI/Transitioner");
                // create the prefab in scene
                var inScene = Instantiate(transitionerPrefab);
                _instance = inScene.GetComponentInChildren<UI_Transitioner>();
                if (!_instance) _instance = inScene.AddComponent<UI_Transitioner>();
                DontDestroyOnLoad(_instance.transform.root.gameObject);
            }
            return _instance;
        }
    }
    ////////////////////////////////////////////////

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _overlay;
    [SerializeField] private float _transitionTime = 0.1f;

    private bool _isTransitionIn;

    // public event Action OnTransitionStart;
    public event Action OnTransitionEnd;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void BeginTransition()
    {
        _canvas.gameObject.SetActive(true);
        AnimateOverlay();
    }

    private void AnimateOverlay()
    {
        if (_isTransitionIn)
        {
            _isTransitionIn = false;
            StartCoroutine(ScaleOutOverlay());
        }
        else
        {
            _isTransitionIn = true;
            StartCoroutine(ScaleInOverlay());
        }
    }

    private IEnumerator ScaleInOverlay()
    {
        float yScale;
        float time = 0f;
        while (time < _transitionTime)
        {
            time += Time.unscaledDeltaTime;
            yScale = Mathf.Lerp(0f, 1f, time / _transitionTime);
            _overlay.rectTransform.localScale = new Vector3(1f, yScale, 1f);
            yield return null;
        }

        OnTransitionEnd?.Invoke();
    }

    private IEnumerator ScaleOutOverlay()
    {
        float yScale;
        float time = 0f;
        while (time < _transitionTime)
        {
            yScale = Mathf.Lerp(1f, 0f, time / _transitionTime);
            _overlay.rectTransform.localScale = new Vector3(1f, yScale, 1f);
            time += Time.deltaTime;
            yield return null;
        }

        _canvas.gameObject.SetActive(false);
        OnTransitionEnd?.Invoke();
    }
}