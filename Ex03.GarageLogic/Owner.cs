namespace Ex03.GarageLogic
{
    public class Owner
    {
        private readonly string r_OwnerName;
        private readonly string r_PhoneNumber;

        public string OwnerName
        {
            get { return r_OwnerName; }
        }

        public string PhoneNumber
        {
            get { return r_PhoneNumber; }
        }

        public Owner(string i_OwnerName, string i_PhoneNumber)
        {
            r_OwnerName = i_OwnerName;
            r_PhoneNumber = i_PhoneNumber;
        }

        public override string ToString()
        {
            return string.Format("Owner: {0}, Phone: {1}", r_OwnerName, r_PhoneNumber);
        }
    }
}