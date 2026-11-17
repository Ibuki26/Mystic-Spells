using TMPro;
using UnityEngine;

public class RewardLogManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _logTexts;

    // ログを追加
    public void AddLog(string message)
    {
        // 上のログを一つずつ移動
        for (int i = 0; i < _logTexts.Length - 1; i++)
        {
            _logTexts[i].text = _logTexts[i + 1].text;
        }

        // 一番下に新しいログを追加
        _logTexts[^1].SetText(message);
    }

    //エピソードが終わったことを通知
    public void ResetLog()
    {
        AddLog("-----start episode-----");
    }
}