using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Lilly.PlantsPatch

{

    public static class Patch
    {
        public static HarmonyX harmony = null;
        public static string harmonyId = "Lilly.";
/*
        public static void OnPatch(bool repatch = false)
        {
            if (repatch)
            {
                Unpatch();
            }
            if (harmony != null || !Settings.onPatch) return;
            harmony = new HarmonyX(harmonyId);
            try
            {
                harmony.PatchAll();
                MyLog.Message($"Patch <color=#00FF00FF>Succ</color>");
            }
            catch (System.Exception e)
            {
                MyLog.Error($"Patch Fail");
                MyLog.Error(e.ToString());
                MyLog.Error($"Patch Fail");
            }
            
        }

        public static void Unpatch()
        {
            MyLog.Message($"UnPatch");
            if (harmony == null) return;
            harmony.UnpatchSelf();
            harmony = null;
        }
*/
        // PlayDataLoader.DoPlayLoad() 메서드가 def 로딩 후 호출되므로 효과 없음
        //[HarmonyPatch(typeof(PlayDataLoader), "DoPlayLoad")]
        //[HarmonyPostfix]

        public static Dictionary<string, MyPlant> treeBackup = new Dictionary<string, MyPlant>();

        public static void TreeBackup()
        {
            foreach (var def in DefDatabase<ThingDef>.AllDefs)
            {
                if (def.plant != null && def.plant.IsTree)
                {
                    // def.plant.growDays 
                    // def.plant.harvestYield
                    MyPlant value = new MyPlant(def.plant);
                    treeBackup.Add(def.defName, value);                   
                    MyLog.Message($"TreeBackup {def.defName} {value}");
                }
            }

        }

        public static void TreePatch()
        {
            foreach (var def in DefDatabase<ThingDef>.AllDefs)
            {
                if (def.plant != null && def.plant.IsTree)
                {
                    // def.plant.growDays 
                    // def.plant.harvestYield
                    MyLog.Message($"TreeBackup {def.defName} {def.plant.growDays}");
                    if (treeBackup.TryGetValue(def.defName, out MyPlant myPlant))
                    {
                        myPlant.Apply(def.plant);
                        MyLog.Message($"TreePatch {def.defName} {def.plant}");
                    }
                    //else
                    //{
                    //    MyLog.Message($"TreePatch {def.defName} not found");
                    //}
                }
            }
        }

    }
}
