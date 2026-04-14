namespace TP1
{
     class Doctor : Person
    {
        public Doctor()
        {
            AskInformations(_firstName, _lastName);
        }

        public Doctor(string filename)
        {
            
        }

        
        private bool IsAvailable()
        {
            if (_patients.Count < 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void AddPatient(Patient patient)
        {
            _patients.Append(patient);
        }

        private  Queue<Patient> _patients = new();

        private string _firstName;
        private string _lastName;

    }
}
