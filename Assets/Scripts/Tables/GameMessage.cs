
using MasterMemory;
using MessagePack;
using System;
using System.Collections.Generic;

[MemoryTable("gameMessage"), MessagePackObject(true)]
public class GameMessage
{
    [PrimaryKey]
    public int ID { get; set; }

    public string corpse1 { get; set; }
    public string corpse2 { get; set; }
    public string corpse3 { get; set; }

    public bool Special { get; set; }

    public GameMessage() { }

    /// <summary>MessagePackSerializer.Deserialize<dynamic>(MessagePack.MessagePackSerializer.FromJson());
    /// ‚©‚ç‚Ì•ÏŠ·
    /// <see cref="GameMessage" /> class.</summary>
    /// <param name="dynamic_json">src</param>
    public GameMessage(Dictionary<object, object> dynamic_json)
    {
        ID = Convert.ToInt32(dynamic_json["ID"]);
        corpse1 = Convert.ToString(dynamic_json["corpse1"]);
        corpse2 = Convert.ToString(dynamic_json["corpse2"]);
        corpse3 = Convert.ToString(dynamic_json["corpse3"]);

        Special = false;
        object sp;
        if (dynamic_json.TryGetValue("Special", out sp) == true)
        {
            Special = Convert.ToBoolean(sp);
        }
    }
}