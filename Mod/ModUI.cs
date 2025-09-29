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

            //Patch.TreeBackup();//아직 로드 안됨
            //Settings.TreeSetup();//아직 로드 안됨
            settings = GetSettings<Settings>();// StaticConstructorOnStartup 보다 먼저 실행됨         

            MyLog.Message($"ED");
        }

        Vector2 scrollPosition;
        string tmp;

        // 매 프레임마다 호출됨
        public override void DoSettingsWindowContents(Rect inRect)
        {
            //MyLog.Message($"ST {Settings.treeSetup.Count}");
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

            MyLog.Message($"ED");
        }

        public override string SettingsCategory()
        {
            return "Plants Patch".Translate();
        }

        public void TextFieldNumeric(Listing_Standard listing,  KeyValuePair<string, MyPlant> num)
        {
            // 한 줄 높이의 Rect 확보
            //Rect rowRect = listing.GetRect(30f);
            //// 열 너비 계산 (3등분)
            //float colWidth = rowRect.width / 3f;

            //Widgets.Label(new Rect(rowRect.x, rowRect.y, colWidth, rowRect.height), num.Key.Translate());

            //// 두 번째 열: growDays 입력 필드
            //string growStr = num.Value.growDays.ToString();
            //Widgets.TextFieldNumeric(
            //    new Rect(rowRect.x + colWidth, rowRect.y, colWidth, rowRect.height),
            //    ref num.Value.growDays,
            //    ref growStr,
            //    1f, 100f
            //);

            //// 세 번째 열: harvestYield 입력 필드
            //string yieldStr = num.Value.harvestYield.ToString();
            //Widgets.TextFieldNumeric(
            //    new Rect(rowRect.x + colWidth * 2, rowRect.y, colWidth, rowRect.height),
            //    ref num.Value.harvestYield,
            //    ref yieldStr,
            //    1f, 100f
            //);

            //Widgets.Label(listing.GetRect(30f), num.Key.Translate());
            //Widgets.TextFieldNumericLabeled(listing.GetRect(30f),"성장일", ref num.Value.growDays, ref tmp);
            //Widgets.TextFieldNumericLabeled(listing.GetRect(30f),"수확량", ref num.Value.harvestYield, ref tmp);

            listing.Label(num.Key.Translate());
            tmp = num.Value.growDays.ToString();
            listing.TextFieldNumeric(ref num.Value.growDays, ref tmp);
            listing.TextFieldNumericLabeled("성장일", ref num.Value.growDays, ref tmp);
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
