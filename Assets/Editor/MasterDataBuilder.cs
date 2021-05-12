using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MasterData;
using MessagePack;
using MessagePack.Resolvers;
using UnityEditor;
using UnityEngine;

public static class MasterDataBuilder
{

    [MenuItem("MasterMemory/BuildMasterData")]
    private static void BuildMasterData()
    {
        try
        {
            CompositeResolver.RegisterAndSetAsDefault(new[] {
                MasterMemoryResolver.Instance,
                    GeneratedResolver.Instance,
                    StandardResolver.Instance
            });
        }
        catch { }

        //マスターデータをビルドする
        var builder = new DatabaseBuilder();
        BuildMastarData(builder);

        byte[] data = builder.Build();

        var resourcesDir = $"{Application.dataPath}/Resources";
        Directory.CreateDirectory(resourcesDir);
        var filename = "/master-data.bytes";

        using (var fs = new FileStream(resourcesDir + filename, FileMode.Create))
        {
            fs.Write(data, 0, data.Length);
        }

        Debug.Log($"Write byte[] to: {resourcesDir + filename}");

        AssetDatabase.Refresh();
    }

    private static void BuildMastarData(DatabaseBuilder builder)
    {
        string filename = "MasterData.json";
        var masterDataDir = $"{Application.dataPath}/MasterData";
        var filePath = $"{masterDataDir}/{filename}";

        try
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var reader = new StreamReader(fs, Encoding.GetEncoding("Shift_JIS"));
                var dynamicjson = MessagePackSerializer.Deserialize<dynamic>(MessagePack.MessagePackSerializer.FromJson(reader));
          
                IEnumerable<object> jsondata = dynamicjson;
                var msg_list = jsondata.Select(i => new GameMessage((Dictionary<object, object>)i)).ToArray();

                builder.Append(msg_list);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            //CreateNewSavedata();
        }
    }
}
