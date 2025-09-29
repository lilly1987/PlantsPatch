using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Lilly.PlantsPatch
{

    public class ModUI : Mod
    {
        public static ModUI self;
        public static Settings settings;

        public ModUI(ModContentPack content) : base(content)
        {
            self = this;
            MyLog.Message($"ST");

            //Patch.OnPatch();//이미 defs가 로드된 후라 너무 늦음

            Patch.TreeBackup();//
            Settings.TreeSetup();//
            settings = GetSettings<Settings>();// StaticConstructorOnStartup 보다 먼저 실행됨         

            MyLog.Message($"ED");
        }

        Vector2 scrollPosition;
        string tmp;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);

            var rect = new Rect(0, 0, inRect.width - 16, 1000);

            Widgets.BeginScrollView(inRect, ref scrollPosition, rect);

            Listing_Standard listing = new Listing_Standard();

            listing.Begin(rect);

            listing.GapLine();

            // ---------

            listing.CheckboxLabeled($"Debug", ref Settings.onDebug);
            //listing.CheckboxLabeled($"on Patch", ref Settings.onPatch);

            foreach (KeyValuePair<string, MyPlant> item in Settings.treeSetup)
            {
                TextFieldNumeric(listing, item);
            }

            // ---------

            listing.GapLine();

            listing.End();

            Widgets.EndScrollView();
        }

        public override string SettingsCategory()
        {
            return "Max Fish Population".Translate();
        }

        public void TextFieldNumeric(Listing_Standard listing,  KeyValuePair<string, MyPlant> num) 
        {
            listing.Label(num.Key.Translate());
            tmp = num.Value.growDays.ToString();
            listing.TextFieldNumeric(ref num.Value.growDays, ref tmp);
            tmp = num.Value.harvestYield.ToString();
            listing.TextFieldNumeric(ref num.Value.harvestYield, ref tmp);
        }

        public void TextFieldNumeric<T>(Listing_Standard listing, ref T num, string label = "", string tipSignal = "") where T : struct
        {
            listing.Label(label.Translate(), tipSignal: tipSignal.Translate());
            tmp = num.ToString();
            listing.TextFieldNumeric<T>(ref num, ref tmp);
        }
    }
}
