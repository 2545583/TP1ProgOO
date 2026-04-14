namespace TP1
{
     class DoctorManager
    {
        public DoctorManager()
        {
          
        }

        public void Add()
        {
            Doctor doctor = new();
            _doctors.Enqueue(doctor);
        }

        public void Print()
        {

        }

        public void Save()
        {

        }

        private Queue<Doctor> _doctors = new();
    }
}
