using System;

namespace UtilPay
{
    class Gaz
    {
        public uint NumberOfCubes;
        public decimal TotaGazlCost;
        const decimal GazTariff = 6.102M;

        public Gaz()
        {}

        private uint initialGazVal;
        public uint InitialGazVal
        {
            get { return initialGazVal; }
            set { initialGazVal = value; }
        }

        private uint finalGazVal;
        public uint FinalGazVal
        {
            get { return finalGazVal; }
            set { finalGazVal = value; }
        }
        
        public decimal CalcGazCost()
        {
            NumberOfCubes = finalGazVal - InitialGazVal;
            TotaGazlCost = Math.Round(NumberOfCubes * GazTariff, 2);
            return TotaGazlCost;
        }
    }
}
