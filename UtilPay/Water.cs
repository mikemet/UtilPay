namespace UtilPay
{
    class Water
    {
        const decimal WaterTariff = 26.34M;
        public uint SpentCubes;
        public decimal TotalWaterCost;

        public Water()
        { }

        private uint initialWaterVal;
        public uint InitialWaterVal
        {
            get { return initialWaterVal; }
            set { initialWaterVal = value; }
        }

        private uint finalWaterVal;
        public uint FinalWaterVal
        {
            get { return finalWaterVal; }
            set { finalWaterVal = value; }
        }

        public decimal CalcWaterCost ()
        {
            SpentCubes = finalWaterVal - initialWaterVal;
            TotalWaterCost = SpentCubes * WaterTariff;
            return TotalWaterCost;
        }
    }
}
