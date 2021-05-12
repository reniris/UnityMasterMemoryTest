using System.Linq;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI MessageText;

    void Start()
    {
        var db = MasterDataDB.DB;
        var msg = db.GameMessageTable.FindByID(1);
        SetMessage(msg.corpse1 + msg.corpse2 + msg.corpse3);
    }

    private void SetMessage(string msg)
    {
        MessageText.text += msg + "\n";
        Debug.Log(msg);
    }
}
