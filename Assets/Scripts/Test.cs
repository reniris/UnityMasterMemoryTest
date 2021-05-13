using System.Linq;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI MessageText;
<<<<<<< HEAD

    void Start()
    {
        var db = MasterDataDB.DB;

        var saveDataManager = SaveDataManager.Instance;
        saveDataManager.Load("save-file-0");
        var skillLevels = saveDataManager.SkillLevels;

        foreach (var skill in db.SkillTable.All)
        {
            SetMessage($"Skill Name: {skill.SkillName}");

            var lv = skillLevels[skill.SkillID];
            SetMessage($"Skill Lv: {lv}");

            var parameter = db.SkillParameterTable
                .FindBySkillIDAndSkillLv((skill.SkillID, lv));
            SetMessage($"Skill Damage: {parameter.Damage}");
        }

        var newLevels = skillLevels.Select(lv =>
        {
            if (lv >= 9) return lv;
            return lv + 1;
        }).ToList();
        saveDataManager.SaveSkillLevels("save-file-0", newLevels);

        
    }

=======

    void Start()
    {
        var db = MasterDataDB.DB;
        var msg = db.GameMessageTable.FindByID(1);
        SetMessage(msg.corpse1 + msg.corpse2 + msg.corpse3);
    }

>>>>>>> develop_reniris
    private void SetMessage(string msg)
    {
        MessageText.text += msg + "\n";
        Debug.Log(msg);
    }
}
