using RimWorld;
using Verse;

namespace Lilly.PlantsPatch

{
    public class TreeSetupEntry
    {
        public string defName="";
        public MyPlant plant = new MyPlant();

        public void ExposeData()
        {
            Scribe_Values.Look(ref defName, "defName");
            Scribe_Deep.Look(ref plant, "plant");
        }
    }


    public class MyPlant
    {
        public float growDays=1f;// 나무 성장 시간
        public float harvestYield=10f;// 수확량

        public MyPlant() { }

        public void ExposeData()
        {
            Scribe_Values.Look(ref growDays, "growDays", 1f);
            Scribe_Values.Look(ref harvestYield, "harvestYield", 25f);
        }

        public MyPlant(MyPlant plant)
        {
            growDays=plant.growDays;
            harvestYield = plant.harvestYield;
        }

        public MyPlant(PlantProperties plant)
        {
            growDays=plant.growDays;
            harvestYield = plant.harvestYield;
        }

        public void Apply(PlantProperties plant)
        {
            plant.growDays = growDays;
            plant.harvestYield = harvestYield;
        }

        public override string ToString()
        {
            return $"growDays:{growDays}, harvestYield:{harvestYield}";
        }

        public void SetFrom(MyPlant other)
        {
            this.growDays = other.growDays;
            this.harvestYield = other.harvestYield;
        }

        public MyPlant Copy()
        {
            return new MyPlant(this);
        }
    }
}
