using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("盤面のボタン")]
    [SerializeField] private Button[] cells;

    [Header("ターン表示用のテキスト")]
    [SerializeField] private TextMeshProUGUI turnText;

    //現在のプレイヤー
    private string currentMark = "〇";

    //各マスの状態の保存
    private string[] board = new string[9];

    private void Start()
    {
        //Buttonのクリックイベントの登録
        for (int i = 0; i < cells.Length; i++)
        {
            int index = i;

            cells[i].onClick.AddListener(() => OnCellClicked(index));
        }
        //初期ターンの表示
        UpdateTurnText();
    }

    //Buttonがクリックされた時の処理
    private void OnCellClicked(int index)
    {
        //既に入力されている場合は処理しない
        if ((!string.IsNullOrEmpty(board[index])))
        {
            return;
        }

        //盤面の状態更新
        board[index] = currentMark;

        //Buttonの子オブジェクトからTextMeshProUGUIを取得
        TextMeshProUGUI text = cells[index].GetComponentInChildren<TextMeshProUGUI>();

        //〇か×を表示
        if (text != null)
        {
            text.text = currentMark;
        } 
        //次のプレイヤーに交代
        currentMark = currentMark == "〇" ? "×" : "〇";

        //ターン表示の更新
        UpdateTurnText();
    }

    //ターン表示の更新
    private void UpdateTurnText()
    {
        turnText.text = "現在のプレイヤー:" + currentMark;
    }

    //ゲームのリセット
    public void ResetGame()
    {
        //盤面の状態を初期化
        for (int i = 0; i < board.Length; i++)
        {
            board[i] = null;

            //TextMeshProの文字を消す
            TextMeshProUGUI text = cells[i].GetComponentInChildren<TextMeshProUGUI>();

            if (text != null)
            {
                text.text = "";
            }

        }

        //選考に〇を戻す
        currentMark = "〇";

        //ターン表示の更新
        UpdateTurnText();
    }
}
