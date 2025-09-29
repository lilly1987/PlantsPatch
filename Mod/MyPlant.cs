using RimWorld;

namespace Lilly.PlantsPatch

{
    public class MyPlant
    {
        public float growDays;// 나무 성장 시간
        public float harvestYield;// 수확량

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
