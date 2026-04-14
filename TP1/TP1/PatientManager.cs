namespace TP1
{
     class PatientManager
    {
        public PatientManager(DoctorManager doctorManager)
        {
            
        }

        public void Add()
        {
            Patient patient = new();
            _patients.Enqueue(patient);
        }

        public void Print()
        {
           
        }

        public void Save()
        {

        }

        public void MakeAppointment()
        {

        }


        private Queue<Patient> _patients = new();
    }
}
