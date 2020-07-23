namespace UtilPay
{
    class Trash
    {
        const decimal TrashTariff = 75.22M;
        public decimal totalTrashCoast;

        public Trash()
        { }

        private uint numberofpersons;
        public uint numberOfPersons
        {
            get { return numberofpersons; }
            set { numberofpersons = value; }
        }

        public decimal CalcTrashCoast ()
        {
            totalTrashCoast = TrashTariff * numberOfPersons;
            return totalTrashCoast;
        } 
    }
}
