namespace SynWebCRM.Data.Mock
{
    public class IntAutoincrementor
    {
        public IntAutoincrementor(int seedValue = 0)
        {
            Seed(seedValue);
        }

        public void Seed(int seedValue)
        {
            _value = seedValue;
        }

        private int _value;
        public int NextValue => ++_value;
    }
}
