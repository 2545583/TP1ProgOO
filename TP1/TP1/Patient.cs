namespace TP1
{
     class Patient : Person
    {
        public Patient()
        {
            AskInformations(_firstName, _lastName);
        }

        private string _firstName;
        private string _lastName;


    }
}
