using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DissolveController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Material dissolveMaterial; // Material with dissolve shader
    [SerializeField] private Button dissolveButton;     // Button to start dissolve
    [SerializeField] private Button undoDissolveButton; // Button to reverse dissolve

    [Header("Tween Settings")]
    [SerializeField] private float tweenDuration = 1.0f; // Duration of the dissolve effect

    private float dissolveAmount = 0f; // Current dissolve amount (0 = fully visible, 1 = fully dissolved)
    private Tween dissolveTween;

    private void Start()
    {
        // Initialize material's dissolve amount
        SetDissolveAmount(dissolveAmount);

        // Assign button click listeners
        dissolveButton.onClick.AddListener(StartDissolve);
        undoDissolveButton.onClick.AddListener(UndoDissolve);
    }

    private void SetDissolveAmount(float amount)
    {
        if (dissolveMaterial != null)
        {
            dissolveMaterial.SetFloat("_DissolveAmount", amount);
        }
    }

    public void StartDissolve()
    {
        // Stop any ongoing tween to avoid conflicts
        dissolveTween?.Kill();

        // Tween _DissolveAmount from 0 to 1
        dissolveTween = DOTween.To(
            () => dissolveAmount,
            x => {
                dissolveAmount = x;
                SetDissolveAmount(dissolveAmount);
            },
            1f,
            tweenDuration
        );
    }

    public void UndoDissolve()
    {
        // Stop any ongoing tween to avoid conflicts
        dissolveTween?.Kill();

        // Tween _DissolveAmount from 1 to 0
        dissolveTween = DOTween.To(
            () => dissolveAmount,
            x => {
                dissolveAmount = x;
                SetDissolveAmount(dissolveAmount);
            },
            0f,
            tweenDuration
        );
    }
}
