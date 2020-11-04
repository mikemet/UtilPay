using System;

namespace UtilPay
{
    class Light
    {
        const decimal LightTariff = 3.63M;
        public decimal TotalLightCost;
        public uint SpentKilowatt;

        public Light()
        { }

        private uint initialLightVal;
        public uint InitialLightVal
        {
            get { return initialLightVal; }
            set { initialLightVal = value; }
        }

        private uint finalLightVal;
        public uint FinalLightVal
        {
            get { return finalLightVal; }
            set { finalLightVal = value; }
        }

        public decimal CalcLightCost()
        {
            SpentKilowatt = finalLightVal - initialLightVal;
            TotalLightCost = Math.Round(SpentKilowatt * LightTariff, 2);
            return TotalLightCost;
        }
    }
}
