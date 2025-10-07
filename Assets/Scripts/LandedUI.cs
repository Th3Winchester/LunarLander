using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LandedUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI titleTextMesh;
    [SerializeField] private TextMeshProUGUI statsTextMesh;

    private void Start()
    {
        Lander.Instance.OnLanded += Instance_OnLanded;

        Hide();
    }

    private void Instance_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        if (e.landingType == Lander.LandingType.Success)
        {
            titleTextMesh.text = "Successful Landing";
        } else
        {
            titleTextMesh.text = "<color=#ff00ff>Lander Crashed!</color>";
        }

        statsTextMesh.text =
            Mathf.Round(e.landingSpeed * 2f) + "\n" +
            Mathf.Round(e.dotVector * 100f) + "\n" +
            "x" + e.scoreMultiplayer + "\n" +
            e.score;

        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
