using System.Collections.Generic;
using Verse;

namespace Lilly.PlantsPatch
{
    public class Settings : ModSettings
    {
        public static bool onDebug = true;
        //public static bool onPatch = true;
        //public static int maxFishPopulation = 1000000;

        public static Dictionary<string, MyPlant> treeSetup = new Dictionary<string, MyPlant>();

        public override void ExposeData()
        {
            if (Scribe.mode != LoadSaveMode.LoadingVars && Scribe.mode != LoadSaveMode.Saving) return;

            MyLog.Message($"<color=#00FF00FF>{Scribe.mode}</color>");
            base.ExposeData();
            Scribe_Values.Look(ref onDebug, "onDebug", false);
            //Scribe_Values.Look(ref onPatch, "onPatch", true);
            
            Scribe_Values.Look<Dictionary<string, MyPlant>>(ref treeSetup, "treeSetup");


            Patch.TreePatch();
        }

        public static void TreeSetup()
        {
            foreach (var kv in Patch.treeBackup)
            {
                // def.plant.growDays 
                // def.plant.harvestYield
                treeSetup.Add(kv.Key, kv.Value.Copy());
                MyLog.Message($"TreeSetup {kv.Key} {kv.Value}");                
            }

        }
    }
}
